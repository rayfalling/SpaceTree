using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlLib.Libs.Converter {
    internal class ClipOuterRectConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Any(o => o == DependencyProperty.UnsetValue)) return new object();

            var width = (double) values[0];
            var height = (double) values[1];
            var margin = (double) values[2];

            return new RectangleGeometry(new Rect(margin, margin, width, height));
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}