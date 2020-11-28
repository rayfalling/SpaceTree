using System;

namespace SpaceTree.Libs.Cache {
    internal class FileCache {
        /// <summary>
        /// 文件路径
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Length { get; set; }

        /// <summary>
        /// 上次统计日期
        /// </summary>
        public DateTime LastSeen { get; set; }

        public FileCache(string uri, long length, DateTime lastSeen) {
            Uri = uri;
            Length = length;
            LastSeen = lastSeen;
        }
    }
}