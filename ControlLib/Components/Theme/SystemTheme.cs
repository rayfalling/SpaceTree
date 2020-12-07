using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ControlLib.Components.Theme {
    public class SystemTheme : ThemeHandler {
        #region Const

        private const int WmWinIniChange = 0x001A;
        private const int WmSettingChange = WmWinIniChange;

        #endregion

        #region Construct

        static SystemTheme() {
            Instance = new SystemTheme();
            AppTheme = GetAppTheme();
            WindowsTheme = GetWindowsTheme();
        }

        #endregion

        #region Override

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            if (msg != WmSettingChange) return IntPtr.Zero;
            var systemParameter = Marshal.PtrToStringAuto(lParam);
            if (systemParameter != "ImmersiveColorSet") return IntPtr.Zero;
            AppTheme = GetAppTheme();
            WindowsTheme = GetWindowsTheme();
            ThemeChanged?.Invoke(null, null!);
            handled = true;
            return IntPtr.Zero;
        }

        #endregion

        #region Private Methods

        private static ApplicationTheme GetAppTheme() {
            var registryKey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);
            if (registryKey == null) return ApplicationTheme.Light;
            var intValue = (int) (registryKey.GetValue("AppsUseLightTheme", 1) ?? 1);
            return intValue == 0 ? ApplicationTheme.Dark : ApplicationTheme.Light;
        }

        private static WindowsTheme GetWindowsTheme() {
            var registryKey =
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey(
                    @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize", false);
            if (registryKey == null) return WindowsTheme.Light;
            var intValue = (int) (registryKey.GetValue("SystemUsesLightTheme", 1) ?? 1);
            return intValue == 0 ? WindowsTheme.Dark : WindowsTheme.Light;
        }

        #endregion

        #region Properties

        private static ApplicationTheme _appTheme;

        public static ApplicationTheme AppTheme {
            get => _appTheme;
            private set {
                if (Equals(_appTheme, value)) return;
                _appTheme = value;
                OnStaticPropertyChanged();
            }
        }

        private static WindowsTheme _windowsTheme;

        public static WindowsTheme WindowsTheme {
            get => _windowsTheme;
            private set {
                if (Equals(_windowsTheme, value)) return;
                _windowsTheme = value;
                OnStaticPropertyChanged();
            }
        }

        #endregion

        #region Events

        public static event EventHandler<PropertyChangedEventArgs>? StaticPropertyChanged;

        protected static void OnStaticPropertyChanged([CallerMemberName] string propertyName = null!) {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        public static event EventHandler? ThemeChanged;

        #endregion
    }
}