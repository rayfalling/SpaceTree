﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace ControlLib.Libs.Converter {
    public class RelativePositionConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Any(o => o == DependencyProperty.UnsetValue || o == null)) return new Point(0, 0);
            var parent = values[0] as UIElement;
            var ctrl = values[1] as UIElement;
            var pointerPos = (Point) values[2];
            var relativePos = parent?.TranslatePoint(pointerPos, ctrl);
            return relativePos ?? new Point();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}