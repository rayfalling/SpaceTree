using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ControlLib.Components.Theme {
    public sealed class ThemeCollection : ObservableCollection<ThemeDictionary> {
        private readonly IList<ThemeDictionary> _previousList;

        public ThemeCollection() {
            _previousList = new List<ThemeDictionary>();
            CollectionChanged += ThemeCollectionChanged;
        }

        #region Events

        private void ThemeCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e) {
            if (e.Action == NotifyCollectionChangedAction.Reset) {
                foreach (var item in _previousList) {
                    item.PropertyChanged -= ItemPropertyChanged;
                }

                _previousList.Clear();
            }


            if (e.OldItems != null) {
                foreach (ThemeDictionary item in e.OldItems) {
                    _previousList.Remove(item);
                    item.PropertyChanged -= ItemPropertyChanged;
                }
            }

            if (e.NewItems != null) {
                foreach (ThemeDictionary item in e.NewItems) {
                    _previousList.Add(item);
                    item.PropertyChanged += ItemPropertyChanged;
                }
            }
        }

        private void ItemPropertyChanged(object? sender, PropertyChangedEventArgs e) {
            var args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
            OnCollectionChanged(args);
        }

        #endregion
      
    }
}