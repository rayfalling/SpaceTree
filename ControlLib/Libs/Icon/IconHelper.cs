using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ControlLib.Libs.Icon {
    class IconHelper {
        #region Icon Property

        private static ImageSource? _appIcon;

        public static ImageSource AppIcon {
            get {
                if (_appIcon != null) return _appIcon;
                var path = System.Reflection.Assembly.GetEntryAssembly()?.Location;
                _appIcon = GetIcon(path ?? System.Environment.CurrentDirectory);

                return _appIcon;
            }
            protected set => _appIcon = value;
        }

        public static ImageSource GetIcon(string path) {
            const uint flags = ShgfiIcon | ShgfiUseFileAttributes | ShgfiSmallIcon;

            var ret = SHGetFileInfo(path, FileAttributeNormal, out var fileInfo,
                (uint) Marshal.SizeOf(typeof(ShellFileInfo)), flags);

            if (ret != 0) {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(fileInfo.hIcon, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }

            return null!;
        }

        #endregion

        #region Win32 API DLL Import

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct ShellFileInfo {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, out ShellFileInfo shellFileInfo,
            uint cbFileInfo, uint uFlags);

        private const uint FileAttributeNormal = 0x00000080;

        private const uint ShgfiIcon = 0x000000100;
        private const uint ShgfiLargeIcon = 0x000000000;
        private const uint ShgfiSmallIcon = 0x000000001;
        private const uint ShgfiUseFileAttributes = 0x000000010;

        #endregion
    }
}