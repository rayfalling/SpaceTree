using System;
using System.Globalization;
using System.Windows.Data;

namespace ControlLib.Libs.Converter {
    public class IsNullConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}