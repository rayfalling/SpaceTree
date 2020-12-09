using SpaceTree.Libs.FileSize;

namespace SpaceTree.Libs.Cache {
    public class DiskInfoCache {
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

        private SizeLevel _sizeLevel = SizeLevel.None;

        /// <summary>
        /// Construct
        /// </summary>
        /// <param name="name"></param>
        /// <param name="diskType"></param>
        /// <param name="diskFreeSize"></param>
        /// <param name="diskTotalSize"></param>
        public DiskInfoCache(string name, string diskType, ulong diskFreeSize, ulong diskTotalSize) {
            Name = name;
            DiskType = name == "C:\\" ? "SystemDisk" : diskType;
            DiskFreeSize = diskFreeSize;
            DiskTotalSize = diskTotalSize;
        }

        public string GetFormattedUsage() {
            if (_sizeLevel == SizeLevel.None)
                _sizeLevel = SizeUtils.GetProperSizeLevel(DiskTotalSize - DiskFreeSize);
            return $"Used: {SizeUtils.GetPrettySize(DiskTotalSize - DiskFreeSize, _sizeLevel)}";
        }

        public string GetFormattedTotal() {
            if (_sizeLevel == SizeLevel.None)
                _sizeLevel = SizeUtils.GetProperSizeLevel(DiskTotalSize - DiskFreeSize);
            return $"Total: {SizeUtils.GetPrettySize(DiskTotalSize, _sizeLevel)}";
        }
    }
}