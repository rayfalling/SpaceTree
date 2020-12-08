using System;

namespace SpaceTree.Libs.Cache {
    internal class FileCache {
        /// <summary>
        /// file path
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// file size
        /// </summary>
        public ulong Length { get; set; }

        /// <summary>
        /// last access date
        /// </summary>
        public DateTime LastSeen { get; set; }

        public FileCache(string uri, ulong length, DateTime lastSeen) {
            Uri = uri;
            Length = length;
            LastSeen = lastSeen;
        }
    }
}