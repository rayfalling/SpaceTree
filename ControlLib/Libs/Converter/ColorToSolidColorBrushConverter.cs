using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlLib.Libs.Converter {
    public class ColorToSolidColorBrushConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            try {
                var col = (System.Windows.Media.Color) value;
                return new SolidColorBrush(col);
            } catch {
                return Brushes.Transparent;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}