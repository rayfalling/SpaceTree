﻿using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace ControlLib.Libs.Converter {
    internal class ClipInnerRectConverter : IMultiValueConverter {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            if (values.Any(o => o == DependencyProperty.UnsetValue)) return new object();

            var width = (double) values[0];
            var height = (double) values[1];
            var outerMargin = (double) values[2];

            var region = new RectangleGeometry(new Rect(-outerMargin, -outerMargin, width + (outerMargin * 2),
                height                                                                    + (outerMargin * 2)));
            var clip = new RectangleGeometry(new Rect(0, 0, width, height));

            var group = new GeometryGroup();
            group.Children.Add(region);
            group.Children.Add(clip);

            return group;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}