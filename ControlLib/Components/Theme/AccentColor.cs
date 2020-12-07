using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Media;
using static System.Windows.Media.ColorConverter;

namespace ControlLib.Components.Theme {
    public class AccentColors : ThemeHandler {
        #region Native WinAPI Import

        [DllImport("uxtheme.dll", EntryPoint = "#95", CharSet = CharSet.Unicode)]
        internal static extern uint GetImmersiveColorFromColorSetEx(uint dwImmersiveColorSet, uint dwImmersiveColorType,
            bool bIgnoreHighContrast, uint dwHighContrastCacheMode);

        [DllImport("uxtheme.dll", EntryPoint = "#96", CharSet = CharSet.Unicode)]
        internal static extern uint GetImmersiveColorTypeFromName(string name);

        [DllImport("uxtheme.dll", EntryPoint = "#98", CharSet = CharSet.Unicode)]
        internal static extern uint GetImmersiveUserColorSetPreference(bool bForceCheckRegistry, bool bSkipCheckOnFail);

        private const int WmDwmColorizationColorChanged = 0x0320;

        #endregion

        #region Construct

        static AccentColors() {
            Instance = new AccentColors();
            Initialize();
        }

        #endregion

        #region Public Methods

        public static Color GetColorByTypeName(string name) {
            var colorSet = GetImmersiveUserColorSetPreference(false, false);
            var colorType = GetImmersiveColorTypeFromName(name);
            var rawColor = GetImmersiveColorFromColorSetEx(colorSet, colorType, false, 0);

            var bytes = BitConverter.GetBytes(rawColor);
            return Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);
        }

        #endregion

        #region Colors

        private static Color _immersiveSystemAccent;

        public static Color ImmersiveSystemAccent {
            get => _immersiveSystemAccent;
            private set {
                if (Equals(_immersiveSystemAccent, value)) return;
                _immersiveSystemAccent = value;
                OnStaticPropertyChanged();
            }
        }

        private static Color _immersiveSystemAccentDark1;

        public static Color ImmersiveSystemAccentDark1 {
            get => _immersiveSystemAccentDark1;
            private set {
                if (Equals(_immersiveSystemAccentDark1, value)) return;
                _immersiveSystemAccentDark1 = value;
                OnStaticPropertyChanged();
            }
        }

        private static Color _immersiveSystemAccentDark2;

        public static Color ImmersiveSystemAccentDark2 {
            get => _immersiveSystemAccentDark2;
            private set {
                if (Equals(_immersiveSystemAccentDark2, value)) return;
                _immersiveSystemAccentDark2 = value;
                OnStaticPropertyChanged();
            }
        }

        private static Color _immersiveSystemAccentDark3;

        public static Color ImmersiveSystemAccentDark3 {
            get => _immersiveSystemAccentDark3;
            private set {
                if (Equals(_immersiveSystemAccentDark3, value)) return;
                _immersiveSystemAccentDark3 = value;
                OnStaticPropertyChanged();
            }
        }

        private static Color _immersiveSystemAccentLight1;

        public static Color ImmersiveSystemAccentLight1 {
            get => _immersiveSystemAccentLight1;
            private set {
                if (Equals(_immersiveSystemAccentLight1, value)) return;
                _immersiveSystemAccentLight1 = value;
                OnStaticPropertyChanged();
            }
        }

        private static Color _immersiveSystemAccentLight2;

        public static Color ImmersiveSystemAccentLight2 {
            get => _immersiveSystemAccentLight2;
            private set {
                if (Equals(_immersiveSystemAccentLight2, value)) return;
                _immersiveSystemAccentLight2 = value;
                OnStaticPropertyChanged();
            }
        }

        private static Color _immersiveSystemAccentLight3;

        public static Color ImmersiveSystemAccentLight3 {
            get => _immersiveSystemAccentLight3;
            private set {
                if (Equals(_immersiveSystemAccentLight3, value)) return;
                _immersiveSystemAccentLight3 = value;
                OnStaticPropertyChanged();
            }
        }

        #endregion

        #region Brushes

        private static Brush _immersiveSystemAccentBrush = null!;

        public static Brush ImmersiveSystemAccentBrush {
            get => _immersiveSystemAccentBrush;
            private set {
                if (Equals(_immersiveSystemAccentBrush, value)) return;
                _immersiveSystemAccentBrush = value;
                OnStaticPropertyChanged();
            }
        }

        private static Brush _immersiveSystemAccentDark1Brush = null!;

        public static Brush ImmersiveSystemAccentDark1Brush {
            get => _immersiveSystemAccentDark1Brush;
            private set {
                if (Equals(_immersiveSystemAccentDark1Brush, value)) return;
                _immersiveSystemAccentDark1Brush = value;
                OnStaticPropertyChanged();
            }
        }

        private static Brush _immersiveSystemAccentDark2Brush = null!;

        public static Brush ImmersiveSystemAccentDark2Brush {
            get => _immersiveSystemAccentDark2Brush;
            private set {
                if (Equals(_immersiveSystemAccentDark2Brush, value)) return;
                _immersiveSystemAccentDark2Brush = value;
                OnStaticPropertyChanged();
            }
        }

        private static Brush _immersiveSystemAccentDark3Brush = null!;

        public static Brush ImmersiveSystemAccentDark3Brush {
            get => _immersiveSystemAccentDark3Brush;
            private set {
                if (Equals(_immersiveSystemAccentDark3Brush, value)) return;
                _immersiveSystemAccentDark3Brush = value;
                OnStaticPropertyChanged();
            }
        }

        private static Brush _immersiveSystemAccentLight1Brush = null!;

        public static Brush ImmersiveSystemAccentLight1Brush {
            get => _immersiveSystemAccentLight1Brush;
            private set {
                if (Equals(_immersiveSystemAccentLight1Brush, value)) return;
                _immersiveSystemAccentLight1Brush = value;
                OnStaticPropertyChanged();
            }
        }

        private static Brush _immersiveSystemAccentLight2Brush = null!;

        public static Brush ImmersiveSystemAccentLight2Brush {
            get => _immersiveSystemAccentLight2Brush;
            private set {
                if (Equals(_immersiveSystemAccentLight2Brush, value)) return;
                _immersiveSystemAccentLight2Brush = value;
                OnStaticPropertyChanged();
            }
        }

        private static Brush _immersiveSystemAccentLight3Brush = null!;

        public static Brush ImmersiveSystemAccentLight3Brush {
            get => _immersiveSystemAccentLight3Brush;
            private set {
                if (Equals(_immersiveSystemAccentLight3Brush, value)) return;
                _immersiveSystemAccentLight3Brush = value;
                OnStaticPropertyChanged();
            }
        }

        #endregion

        #region Initialize

        internal static void Initialize() {
            if (!Libs.SystemInfo.SystemInfo.IsWin7()) {
                ImmersiveSystemAccent = GetColorByTypeName("ImmersiveSystemAccent");
                ImmersiveSystemAccentDark1 = GetColorByTypeName("ImmersiveSystemAccentDark1");
                ImmersiveSystemAccentDark2 = GetColorByTypeName("ImmersiveSystemAccentDark2");
                ImmersiveSystemAccentDark3 = GetColorByTypeName("ImmersiveSystemAccentDark3");
                ImmersiveSystemAccentLight1 = GetColorByTypeName("ImmersiveSystemAccentLight1");
                ImmersiveSystemAccentLight2 = GetColorByTypeName("ImmersiveSystemAccentLight2");
                ImmersiveSystemAccentLight3 = GetColorByTypeName("ImmersiveSystemAccentLight3");
            } else {
                ImmersiveSystemAccent =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF2990CC");
                ImmersiveSystemAccentDark1 =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF2481B6");
                ImmersiveSystemAccentDark2 =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF2071A1");
                ImmersiveSystemAccentDark3 =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF205B7E");
                ImmersiveSystemAccentLight1 =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF2D9FE1");
                ImmersiveSystemAccentLight2 =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF51A5D6");
                ImmersiveSystemAccentLight3 =
                    // ReSharper disable once PossibleNullReferenceException
                    (Color) ConvertFromString("#FF7BB1D0");
            }

            ImmersiveSystemAccentBrush = CreateBrush(ImmersiveSystemAccent);
            ImmersiveSystemAccentDark1Brush = CreateBrush(ImmersiveSystemAccentDark1);
            ImmersiveSystemAccentDark2Brush = CreateBrush(ImmersiveSystemAccentDark2);
            ImmersiveSystemAccentDark3Brush = CreateBrush(ImmersiveSystemAccentDark3);
            ImmersiveSystemAccentLight1Brush = CreateBrush(ImmersiveSystemAccentLight1);
            ImmersiveSystemAccentLight2Brush = CreateBrush(ImmersiveSystemAccentLight2);
            ImmersiveSystemAccentLight3Brush = CreateBrush(ImmersiveSystemAccentLight3);
        }

        internal static Brush CreateBrush(Color color) {
            var brush = new SolidColorBrush(color);
            brush.Freeze();
            return brush;
        }

        #endregion

        #region Events

        public static event EventHandler<PropertyChangedEventArgs> StaticPropertyChanged = null!;

        protected static void OnStaticPropertyChanged([CallerMemberName] string propertyName = null!) {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled) {
            if (msg == WmDwmColorizationColorChanged) {
                Initialize();
            }

            return IntPtr.Zero;
        }

        #endregion
    }
}