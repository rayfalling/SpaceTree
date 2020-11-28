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
        /// 文件夹路径
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 文件夹大小
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 上次统计日期
        /// </summary>
        public DateTime LastSeen { get; set; }

        /// <summary>
        /// 当前目录文件信息
        /// </summary>
        public List<FileCache> FileCaches;

        /// <summary>
        /// 子目录信息
        /// </summary>
        public List<DirectoryCache> SubDirectoryCaches;

        public DirectoryCache(string uri, long length, DateTime lastSeen) {
            Uri = uri;
            Length = length;
            LastSeen = lastSeen;
            FileCaches = new List<FileCache>();
            SubDirectoryCaches = new List<DirectoryCache>();
        }

        public DirectoryCache() {
            Uri = "";
            Length = 0;
            LastSeen = DateTime.Now;
            FileCaches = new List<FileCache>();
            SubDirectoryCaches = new List<DirectoryCache>();
        }

        /// <summary>
        /// 获取原始大小
        /// </summary>
        /// <returns></returns>
        public long GetRawSize() {
            CheckSize();
            return Length;
        }

        /// <summary>
        /// 获取格式化大小
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