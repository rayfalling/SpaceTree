using System;
using System.Runtime.InteropServices;
using ControlLib.Components.Window.Data;
using ControlLib.Libs.SystemInfo;

namespace ControlLib.Libs.Acrylic {
    internal class AcrylicHelper {
        #region Win32 API DLL Import

        /// <summary>
        /// windows native method
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

        #endregion

        #region Tint Color

        private const uint AccentColor = 0x00FFFFFF;

        #endregion

        #region Static Methods

        /// <summary>
        /// Enable blur effect in windows platform
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="style"></param>
        /// <param name="state"></param>
        /// <param name="accentColor"></param>
        internal static void EnableBlur(IntPtr hwnd, uint accentColor = AccentColor,
            AccentFlagsType style = AccentFlagsType.Window, AccentState state = AccentState.ACCENT_DISABLED) {
            var accent = new AccentPolicy();
            var accentStructSize = Marshal.SizeOf(accent);

            accent.AccentState = state == AccentState.ACCENT_DISABLED ? CheckPlatformSupport(state) : state;
            accent.AccentFlags = style == AccentFlagsType.Window ? 2 : 0x20 | 0x40 | 0x80 | 0x100;
            accent.GradientColor = accentColor;

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = accentStructSize,
                Data = accentPtr
            };

            _ = SetWindowCompositionAttribute(hwnd, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        /// <summary>
        /// check if platform 
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        internal static AccentState CheckPlatformSupport(AccentState state) {
            var currentVersion = SystemInfo.SystemInfo.Version.Value;
            if (currentVersion >= VersionInfo.Windows101903) {
                if (state == AccentState.ACCENT_DISABLED) {
                    return AccentState.ACCENT_ENABLE_BLURBEHIND;
                }

                if (state >= AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND) {
                    return AccentState.ACCENT_ENABLE_BLURBEHIND;
                }
            }

            if (currentVersion == VersionInfo.Windows101809) {
                if (state == AccentState.ACCENT_DISABLED) {
                    return AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND;
                }
            }

            if (currentVersion >= VersionInfo.Windows10) {
                if (state == AccentState.ACCENT_DISABLED) {
                    return AccentState.ACCENT_ENABLE_BLURBEHIND;
                }

                if (state >= AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND) {
                    return AccentState.ACCENT_ENABLE_BLURBEHIND;
                }
            }

            if (currentVersion >= VersionInfo.Windows7) {
                if (state == AccentState.ACCENT_DISABLED) {
                    return AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT;
                }

                if (state >= AccentState.ACCENT_ENABLE_BLURBEHIND) {
                    return AccentState.ACCENT_ENABLE_TRANSPARENTGRADIENT;
                }
            }

            return state;
        }

        #endregion
    }
}