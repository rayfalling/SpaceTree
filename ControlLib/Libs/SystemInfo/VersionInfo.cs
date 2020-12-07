using System;

namespace ControlLib.Libs.SystemInfo {
    internal class VersionInfo : IEquatable<VersionInfo>, IComparable<VersionInfo>, IComparable {
        public readonly int Major;
        public readonly int Minor;
        public readonly int Build;

        public VersionInfo(int major, int minor, int build) {
            Major = major;
            Minor = minor;
            Build = build;
        }

        public bool Equals(VersionInfo? other) {
            return other is not null && Major == other.Major && Minor == other.Minor && Build == other.Build;
        }

        public override bool Equals(object? obj) {
            return (obj is VersionInfo other) && Equals(other);
        }

        public override int GetHashCode() {
            return Major.GetHashCode() ^ Minor.GetHashCode() ^ Build.GetHashCode();
        }

        public static bool operator ==(VersionInfo left, VersionInfo right) {
            return left.Equals(right);
        }

        public static bool operator !=(VersionInfo left, VersionInfo right) {
            return !(left == right);
        }


        public int CompareTo(VersionInfo? other) {
            return other switch {
                not null when Major != other.Major => Major.CompareTo(other.Major),
                not null when Minor != other.Minor => Minor.CompareTo(other.Minor),
                _ => other is not null && Build != other.Build ? Build.CompareTo(other.Build) : 0
            };
        }

        public int CompareTo(object? obj) {
            if (obj is VersionInfo other) {
                return CompareTo(other);
            }

            throw new ArgumentException("param is not VersionInfo");
        }

        public static bool operator <(VersionInfo left, VersionInfo right) {
            return left.CompareTo(right) < 0;
        }

        public static bool operator <=(VersionInfo left, VersionInfo right) {
            return left.CompareTo(right) <= 0;
        }

        public static bool operator >(VersionInfo left, VersionInfo right) {
            return left.CompareTo(right) > 0;
        }

        public static bool operator >=(VersionInfo left, VersionInfo right) {
            return left.CompareTo(right) >= 0;
        }

        public override string ToString() {
            return $"{Major}.{Minor}.{Build}";
        }

        public static VersionInfo Windows7 => new VersionInfo(6, 1, 7600);
        public static VersionInfo Windows7Sp1 => new VersionInfo(6, 1, 7601);

        public static VersionInfo Windows8 => new VersionInfo(6, 2, 9200);
        public static VersionInfo Windows81 => new VersionInfo(6, 3, 9600);

        public static VersionInfo Windows10 => new VersionInfo(10, 0, 10240);
        public static VersionInfo Windows101511 => new VersionInfo(10, 0, 10586);
        public static VersionInfo Windows101607 => new VersionInfo(10, 0, 14393);
        public static VersionInfo Windows101703 => new VersionInfo(10, 0, 15063);
        public static VersionInfo Windows101709 => new VersionInfo(10, 0, 16299);
        public static VersionInfo Windows101803 => new VersionInfo(10, 0, 17134);
        public static VersionInfo Windows101809 => new VersionInfo(10, 0, 17763);
        public static VersionInfo Windows101903 => new VersionInfo(10, 0, 18362);
        public static VersionInfo Windows102004 => new VersionInfo(10, 0, 19041);
        public static VersionInfo Windows1020H2 => new VersionInfo(10, 0, 19042);
    }
}