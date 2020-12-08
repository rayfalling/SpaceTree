using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceTree.Libs.Cache;
using SpaceTree.Libs.Config.Disk;

namespace SpaceTree.Libs.Helper.Disk {
    internal static class DiskInfo {
        public static List<DiskInfoCache> GetSystemDriveInfos() {
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            return allDrives.Where(DiskConfig.GetInstance().TestDiskType)
                            .Select(item => new DiskInfoCache(
                                item.Name,
                                item.DriveType.ToString(),
                                (ulong) item.TotalFreeSpace,
                                (ulong) item.TotalSize))
                            .ToList();
        }
    }
}