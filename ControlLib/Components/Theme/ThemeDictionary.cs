using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ControlLib.Components.Theme {
    public class ThemeDictionary : ResourceDictionary, INotifyPropertyChanged {
        private string? _themeName;

        public string ThemeName {
            get => _themeName!;
            set {
                _themeName = value;
                OnPropertyChanged();
            }
        }

        public new Uri Source {
            get => base.Source;
            set {
                base.Source = value;
                OnPropertyChanged();
            }
        }

        #region Event

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null!) {
            var eventHandler = PropertyChanged;
            eventHandler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}