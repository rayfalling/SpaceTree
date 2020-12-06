using System;
using System.Runtime.InteropServices;
using ControlLib.DataModel.SystemInfo;
using ControlLib.DataModel.Window;

namespace ControlLib.Libs {
    internal class AcrylicHelper {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        internal static void EnableBlur(IntPtr hwnd, AccentFlagsType style = AccentFlagsType.Window) {
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);
            var currentVersion = SystemInfo.Version.Value;
            if (currentVersion >= VersionInfo.Windows101903) {
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            } else if (currentVersion >= VersionInfo.Windows101809) {
                accent.AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
            } else if (currentVersion >= VersionInfo.Windows10) {
                accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;
            } else {
                accent.AccentState = AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT;
            }

            if (style == AccentFlagsType.Window) {
                accent.AccentFlags = 2;
            } else {
                accent.AccentFlags = 0x20 | 0x40 | 0x80 | 0x100;
            }

            //accent.GradientColor = 0x99FFFFFF;
            accent.GradientColor = 0x00FFFFFF;

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(hwnd, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}