namespace SpaceTree.Libs.Cache {
    internal class DiskInfoCache {
        /// <summary>
        /// Disk name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Disk type
        /// </summary>
        public string DiskType { get; set; }

        /// <summary>
        /// total free size of disk
        /// </summary>
        public ulong DiskFreeSize { get; set; }

        /// <summary>
        /// total used size of disk
        /// </summary>
        public ulong DiskTotalSize { get; set; }

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="name"></param>
        /// <param name="length"></param>
        /// <param name="diskType"></param>
        /// <param name="diskFreeSize"></param>
        /// <param name="diskUsedSize"></param>
        public DiskInfoCache(string name, string diskType, ulong diskFreeSize, ulong diskTotalSize) {
            Name = name;
            DiskType = diskType;
            DiskFreeSize = diskFreeSize;
            DiskTotalSize = diskTotalSize;
        }
    }
}