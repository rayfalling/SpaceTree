using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ControlLib.Libs.Converter {
    internal class NegativeMarginConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var margin = (double) value;
            return new Thickness(-margin);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}