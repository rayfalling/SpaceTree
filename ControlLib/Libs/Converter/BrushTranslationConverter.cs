using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlLib.Libs.Converter {
    internal class BrushTranslationConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Any(o => o == DependencyProperty.UnsetValue)) return new Point(0, 0);

            var ctrl = values[1] as UIElement;
            // ReSharper disable once InvertIf
            if (values[0] is UIElement parent) {
                var relativePos = parent.TranslatePoint(new Point(0, 0), ctrl);

                return new TranslateTransform(relativePos.X, relativePos.Y);
            }
            return new Point(0, 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}