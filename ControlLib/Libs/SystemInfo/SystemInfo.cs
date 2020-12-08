using System;

namespace ControlLib.Libs.SystemInfo {
    internal class SystemInfo {
        public static Lazy<VersionInfo> Version { get; } = new Lazy<VersionInfo>(GetVersionInfo);

        #region Static

        internal static VersionInfo GetVersionInfo() {
            var registryKey =
                Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\",
                    false);
            if (registryKey == null) return default!;

            // get windows version
            var majorValue = registryKey.GetValue("CurrentMajorVersionNumber");
            var minorValue = registryKey.GetValue("CurrentMinorVersionNumber");
            var buildValue = (string) (registryKey.GetValue("CurrentBuild") ?? 7600);
            var canReadBuild = int.TryParse(buildValue, out var build);

            // default version for system lower than 10.0.10240
            var defaultVersion = System.Environment.OSVersion.Version;

            if (majorValue is int major && minorValue is int minor && canReadBuild) {
                return new VersionInfo(major, minor, build);
            }

            return new VersionInfo(defaultVersion.Major, defaultVersion.Minor, defaultVersion.Revision);
        }

        internal static bool IsWin10() {
            return Version.Value.Major == 10;
        }

        internal static bool IsWin7() {
            return Version.Value.Major == 6 && Version.Value.Minor == 1;
        }

        internal static bool IsWin8() {
            return Version.Value.Major == 6 && (Version.Value.Minor == 2 || Version.Value.Minor == 3);
        }

        #endregion
    }
}