using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpaceTree.Libs.FileSize;

namespace SpaceTree.Libs.Cache {
    /// <summary>
    /// 目录信息缓存
    /// </summary>
    internal class DirectoryCache {
        /// <summary>
        /// full path
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// size of files and sub directories
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// last access date
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        /// can access with current permission
        /// </summary>
        public bool Accessible { get; set; }

        /// <summary>
        /// files cache info located in current directory 
        /// </summary>
        public List<FileCache> FileCaches;

        /// <summary>
        /// sub directory cache info located in current directory 
        /// </summary>
        public List<DirectoryCache> SubDirectoryCaches;

        public DirectoryCache(string uri, long length, DateTime lastSeen) {
            Uri = uri;
            Length = length;
            LastSeen = lastSeen;
            Accessible = true;
            FileCaches = new List<FileCache>();
            SubDirectoryCaches = new List<DirectoryCache>();
        }

        public DirectoryCache() {
            Uri = "";
            Length = 0;
            LastSeen = DateTime.Now;
            Accessible = true;
            FileCaches = new List<FileCache>();
            SubDirectoryCaches = new List<DirectoryCache>();
        }

        /// <summary>
        /// get raw size in byte
        /// </summary>
        /// <returns></returns>
        public long GetRawSize() {
            CheckSize();
            return Length;
        }

        /// <summary>
        /// get formatted size
        /// </summary>
        /// <returns></returns>
        public string GetFormatSize() {
            CheckSize();
            return SizeUtils.GetPrettySize(Length);
        }

        private void CheckSize() {
            if (SubDirectoryCaches.Count == 0 && FileCaches.Count == 0) return;
            long currentSize = 0;
            Parallel.ForEach(FileCaches, fileCache => Interlocked.Add(ref currentSize, fileCache.Length));
            Parallel.ForEach(SubDirectoryCaches,
                directoryCaches => Interlocked.Add(ref currentSize, directoryCaches.GetRawSize()));
            Length = currentSize;
        }
    }
}