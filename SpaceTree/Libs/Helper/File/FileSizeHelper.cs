using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading.Tasks;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Config.File;
using SpaceTree.Libs.Helper.Cmd;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.Helper.File {
    internal static class FileSizeHelper {
        #region Public Methods

        /// <summary>
        /// Main method to count given path
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static DirectoryCache GetDirectorySizeCache(string path) {
            var directoryCache = LoadDirectory(path);
            var missingList = UnauthorizedAccessCache.GetInstance().GetAllMissingPath();

            Task[] taskList = missingList
                              .Select(directoryPath => new Task(() => {
                                  DiskUsageTask(directoryPath, directoryCache);
                              })).ToArray();
            taskList.AsParallel().ForAll(task => task.Start());
            Task.WaitAll(taskList);

            return directoryCache;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// recursive load directory info
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        internal static DirectoryCache LoadDirectory(string path) {
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
                                        !FileExcludeConfig.GetInstance().TestFileMatch(item.FullName))
                                    .ToList();

            var directoryList = directory.GetDirectories()
                                         .Where(item =>
                                             !FileExcludeConfig.GetInstance().TestDirectionMatch(item.FullName))
                                         .ToList();

            Parallel.ForEach(fileList,
                fileInfo => {
                    lock (directoryCache.FileCaches) {
                        directoryCache.FileCaches.Add(new FileCache(fileInfo.FullName, (ulong) fileInfo.Length,
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
        /// get directory size info form du.exe/du64.exe
        /// we will use size not size on disk to keep same with
        /// C# DirectoryInfo report
        /// du.exe output example:
        /// Files:        130552
        /// Directories:  45942
        /// Size:         16,113,239,255 bytes
        /// Size on disk: 14,142,668,800 bytes 
        /// </summary>
        /// <param name="directoryCache"></param>
        /// <returns></returns>
        internal static DirectoryCache LoadDirectoryInfoByCmd(DirectoryCache directoryCache) {
            var output = CmdHelper.ExecDiskUsage(directoryCache.Uri);

            // check if du.exe return "", means a junction 
            if (output == "") return directoryCache;

            // make windows "\r\n" to "\n" only
            output = output.Replace("\r", "");

            var sizeStr = output.Split("\n")[2].Trim();
            sizeStr = sizeStr.Replace("Size: ", "").Replace("bytes", "");

            if (ulong.TryParse(sizeStr, out var size)) {
                directoryCache.Length = size;
            }

            return directoryCache;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// check application have permission to list directory
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        internal static bool CheckAccess(DirectoryInfo directory) {
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

        /// <summary>
        /// Task for inaccessible path
        /// </summary>
        /// <param name="path"></param>
        /// <param name="rootCache"></param>
        private static void DiskUsageTask(string path, DirectoryCache rootCache) {
            var currentCacheNode = rootCache.GetSubDirectoryCache(path);
            if (currentCacheNode == null || currentCacheNode.Accessible) return;
            LoadDirectoryInfoByCmd(currentCacheNode);
        }

        #endregion
    }
}