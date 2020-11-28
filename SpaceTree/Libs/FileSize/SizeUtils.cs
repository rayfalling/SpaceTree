using System;
using SpaceTree.Libs.Config.Exclude;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.FileSize {
    internal static class SizeUtils {
        private const int Rate = 1024;

        /// <summary>
        /// 获取格式化大小
        /// </summary>
        /// <param name="size">文件/文件夹大小</param>
        /// <returns></returns>
        public static string GetPrettySize(long size) { return GetPrettySize(size, GetProperSizeLevel(size)); }

        /// <summary>
        /// 获取格式化大小
        /// </summary>
        /// <param name="size">文件/文件夹大小</param>
        /// <param name="sizeLevel">大小级别</param>
        /// <returns></returns>
        public static string GetPrettySize(long size, SizeLevel sizeLevel) {
            var rate = (int) sizeLevel;
            while (rate != 0) {
                size /= Rate;
                rate--;
            }

            return $"{size} {PrettySizeLevel(sizeLevel)}";
        }

        /// <summary>
        /// 获取合适进制
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SizeLevel GetProperSizeLevel(long size) {
            var rate = 0;
            while (size / (double) Rate > Rate * 0.8) {
                size /= Rate;
                rate++;
                if (size >= 6)
                    break;
            }
            return (SizeLevel) rate;
        }

        /// <summary>
        /// 翻译文件进制英文
        /// </summary>
        /// <param name="sizeLevel"></param>
        /// <returns></returns>
        private static string PrettySizeLevel(SizeLevel sizeLevel) {
            CheckSizeLevel(sizeLevel);
            return sizeLevel switch {
                SizeLevel.Byte => "B",
                SizeLevel.Kilobyte => "KB",
                SizeLevel.Megabyte => "MB",
                SizeLevel.Gigabyte => "GB",
                SizeLevel.Terabyte => "TB",
                SizeLevel.Petabyte => "PB",
                SizeLevel.Exabyte => "EB",
                _ => throw new ArgumentOutOfRangeException(nameof(sizeLevel), sizeLevel, null)
            };
        }

        /// <summary>
        /// 检查枚举类是否超界
        /// </summary>
        /// <param name="sizeLevel"></param>
        private static void CheckSizeLevel(SizeLevel sizeLevel) {
            if (sizeLevel > SizeLevel.Exabyte)
                Logger.Logger.GetInstance().Log(LogLevel.Error, "Incorrect SizeLevel");
        }
    }
}