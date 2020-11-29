using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Config.Exclude;
using SpaceTree.Libs.Helper.Cmd;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.Helper.File {
    internal static class FileSizeHelper {
        /// <summary>
        /// recursive load directory info
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryCache LoadDirectory(string path) {
            var directoryCache = new DirectoryCache();
            DirectoryInfo directory = new DirectoryInfo(path);

            directoryCache.LastSeen = DateTime.Now;
            directoryCache.Uri = directory.FullName;

            if (!CheckAccess(directory)) {
                directoryCache.Accessible = false;
                UnauthorizedAccessCache.GetInstance().InsertMissPath(directory.Parent?.FullName ?? "", path);
                return directoryCache;
            }

            var fileList = directory.GetFiles()
                                    .Where(item =>
                                        !ExcludeMatch.GetInstance().TestFileMatch(item.FullName))
                                    .ToList();

            var directoryList = directory.GetDirectories()
                                         .Where(item =>
                                             !ExcludeMatch.GetInstance().TestDirectionMatch(item.FullName))
                                         .ToList();

            Parallel.ForEach(fileList,
                fileInfo => {
                    lock (directoryCache.FileCaches) {
                        directoryCache.FileCaches.Add(new FileCache(fileInfo.FullName, fileInfo.Length,
                            DateTime.Now));
                    }
                });

            Parallel.ForEach(directoryList,
                directoryInfo => {
                    var dir = LoadDirectory(directoryInfo.FullName);
                    lock (directoryCache.SubDirectoryCaches) {
                        directoryCache.SubDirectoryCaches.Add(dir);
                    }
                });

            return directoryCache;
        }

        /// <summary>
        /// check application have permission to list directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        public static bool CheckAccess(DirectoryInfo directory) {
            try {
                DirectorySecurity directorySecurity = directory.GetAccessControl();
                var rules = directorySecurity.GetAccessRules(true, true, typeof(NTAccount));

                var currentUser = new WindowsPrincipal(WindowsIdentity.GetCurrent());
                var result = false;
                foreach (FileSystemAccessRule rule in rules) {
                    if (0 == (rule.FileSystemRights & (FileSystemRights.ListDirectory | FileSystemRights.Write))) {
                        continue;
                    }

                    if (rule.IdentityReference.Value.StartsWith("S-1-")) {
                        var sid = new SecurityIdentifier(rule.IdentityReference.Value);
                        if (!currentUser.IsInRole(sid))
                            continue;
                    } else {
                        if (!currentUser.IsInRole(rule.IdentityReference.Value))
                            continue;
                    }

                    switch (rule.AccessControlType) {
                        case AccessControlType.Deny:
                            return false;
                        case AccessControlType.Allow:
                            result = true;
                            break;
                    }
                }

                return result;
            } catch (UnauthorizedAccessException) {
                Logger.Logger.GetInstance().Log(LogLevel.Warning, $"Can't access directory: \"{directory.FullName}\"");
                return false;
            }
        }

        public static DirectoryCache LoadDirectoryInfoByCmd(DirectoryCache directoryCache) {
            var output = CmdHelper.ExeCmd(directoryCache.Uri);
            return directoryCache;
        }
    }
}