using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;

namespace ControlLib.Components.Theme {
    public class ResourceDictionaryExtend : ResourceDictionary {
        public ThemeCollection ThemeDictionaries { get; set; } = new ThemeCollection();

        private ElementTheme? _requestedTheme;

        public ElementTheme? RequestedTheme {
            get => _requestedTheme;
            set {
                _requestedTheme = value;
                ChangeTheme();
            }
        }

        public ResourceDictionaryExtend() {
            SystemTheme.ThemeChanged += SystemThemeChanged;
            ThemeDictionaries.CollectionChanged += ThemeDictionariesCollectionChanged;
            GlobalThemeChanged += ResourceDictionaryExtendGlobalThemeChanged;
        }

        #region Private Events

        private void SystemThemeChanged(object? sender, EventArgs e) {
            ChangeTheme();
        }

        private void ThemeDictionariesCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            ChangeTheme();
        }

        private void ResourceDictionaryExtendGlobalThemeChanged(object? sender, EventArgs e) {
            ChangeTheme();
        }

        private void ChangeTheme() {
            var theme = RequestedTheme ?? GlobalTheme;
            // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
            switch (theme) {
                case ElementTheme.Light:
                    ChangeTheme(ApplicationTheme.Light.ToString());
                    break;
                case ElementTheme.Dark:
                    ChangeTheme(ApplicationTheme.Dark.ToString());
                    break;
                default:
                    ChangeTheme(SystemTheme.AppTheme.ToString());
                    break;
            }
        }

        #endregion

        private void ChangeTheme(string themeName) {
            MergedDictionaries.Clear();
            var theme = ThemeDictionaries.FirstOrDefault(o => o.ThemeName == themeName);
            if (theme != null) {
                MergedDictionaries.Add(theme);
            }
        }

        #region static members

        private static ElementTheme? _globalTheme;

        public static ElementTheme? GlobalTheme {
            get => _globalTheme;
            set {
                _globalTheme = value;
                GlobalThemeChanged?.Invoke(null, null!);
            }
        }

        public static event EventHandler<EventArgs> GlobalThemeChanged = null!;

        #endregion
    }
}