using System;
using System.IO;
using System.Threading.Tasks;
using SpaceTree.Libs.Cache;

namespace SpaceTree.Libs.Helper {
    internal class FileHelper {
        public static DirectoryCache LoadDirectory(string path) {
            var directoryCache = new DirectoryCache();
            DirectoryInfo directory = new DirectoryInfo(path);

            directoryCache.LastSeen = DateTime.Now;
            directoryCache.Uri = directory.FullName;

            Parallel.ForEach(directory.GetFiles("*.*"),
                fileInfo => {
                    lock (directoryCache.FileCaches) {
                        directoryCache.FileCaches.Add(new FileCache(fileInfo.FullName, fileInfo.Length, DateTime.Now));
                    }
                });

            Parallel.ForEach(directory.GetDirectories(),
                directoryInfo => {
                    lock (directoryCache.SubDirectoryCaches) {
                        directoryCache.SubDirectoryCaches.Add(LoadDirectory(directoryInfo.FullName));
                    }
                });

            return directoryCache;
        }
    }
}