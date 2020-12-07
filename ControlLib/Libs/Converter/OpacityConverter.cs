using System;
using System.Globalization;
using System.Windows.Data;

namespace ControlLib.Libs.MouseTracker {
    public class OpacityConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var isEnter = (bool) value;
            var opacity = (double) parameter;
            return isEnter ? opacity : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}