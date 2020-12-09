using System;
using SpaceTree.Libs.Logger;

namespace SpaceTree.Libs.FileSize {
    internal static class SizeUtils {
        private const double Rate = 1024;

        /// <summary>
        /// 获取格式化大小
        /// </summary>
        /// <param name="size">文件/文件夹大小</param>
        /// <returns></returns>
        public static string GetPrettySize(ulong size) {
            return GetPrettySize(size, GetProperSizeLevel(size));
        }

        /// <summary>
        /// 获取格式化大小
        /// </summary>
        /// <param name="size">文件/文件夹大小</param>
        /// <param name="sizeLevel">大小级别</param>
        /// <returns></returns>
        public static string GetPrettySize(ulong size, SizeLevel sizeLevel) {
            var tempSize = (double) size;
            var rate = (int) sizeLevel;
            while (rate != 0) {
                tempSize /= Rate;
                rate--;
            }

            return $"{tempSize:0.##} {PrettySizeLevel(sizeLevel)}";
        }

        /// <summary>
        /// 获取合适进制
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        public static SizeLevel GetProperSizeLevel(ulong size) {
            var tempSize = (double) size;
            var rate = 0;
            while ((tempSize / Rate) > 0.8) {
                tempSize /= Rate;
                rate++;
                if (rate >= 6)
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
                SizeLevel.None => "",
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