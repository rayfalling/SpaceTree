using System.Windows;
using System.Windows.Media;

namespace ControlLib.Components.Text {
    internal class AcrylicText {
        #region Property

        #region Header Text

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.RegisterAttached(
            "Header",
            typeof(string),
            typeof(AcrylicText),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits)
        );

        public static void SetHeader(UIElement element, string value) =>
            element.SetValue(HeaderProperty, value);

        public static string GetHeader(UIElement element) =>
            (string) element.GetValue(HeaderProperty);

        #endregion

        #region Header Size

        public static readonly DependencyProperty HeaderSizeProperty = DependencyProperty.RegisterAttached(
            "HeaderSize",
            typeof(double),
            typeof(AcrylicText),
            new FrameworkPropertyMetadata(14.0, FrameworkPropertyMetadataOptions.Inherits)
        );

        public static void SetHeaderSize(UIElement element, double value) =>
            element.SetValue(HeaderSizeProperty, value);

        public static double GetHeaderSize(UIElement element) =>
            (double) element.GetValue(HeaderSizeProperty);

        #endregion

        #region Header ForegroundBrush

        public static readonly DependencyProperty HeaderForegroundBrushProperty = DependencyProperty.RegisterAttached(
            "HeaderForegroundBrush",
            typeof(Brush),
            typeof(AcrylicText),
            new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.Inherits)
        );

        public static void SetHeaderForegroundBrush(UIElement element, Brush value) =>
            element.SetValue(HeaderForegroundBrushProperty, value);

        public static Brush GetHeaderForegroundBrush(UIElement element) =>
            (Brush) element.GetValue(HeaderForegroundBrushProperty);

        #endregion

        #region Placeholder

        public static readonly DependencyProperty PlaceholderTextProperty = DependencyProperty.RegisterAttached(
            "PlaceholderText",
            typeof(string),
            typeof(AcrylicText),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.Inherits)
        );

        public static void SetPlaceholderText(UIElement element, string value) =>
            element.SetValue(PlaceholderTextProperty, value);

        public static string GetPlaceholderText(UIElement element) =>
            (string) element.GetValue(PlaceholderTextProperty);

        #endregion

        #region Placeholder ForegroundBrush

        public static readonly DependencyProperty PlaceholderForegroundBrushProperty =
            DependencyProperty.RegisterAttached(
                "PlaceholderForegroundBrush",
                typeof(Brush),
                typeof(AcrylicText),
                new FrameworkPropertyMetadata(Brushes.DimGray, FrameworkPropertyMetadataOptions.Inherits)
            );

        public static void SetPlaceholderForegroundBrush(UIElement element, Brush value) =>
            element.SetValue(PlaceholderForegroundBrushProperty, value);

        public static Brush GetPlaceholderForegroundBrush(UIElement element) =>
            (Brush) element.GetValue(PlaceholderForegroundBrushProperty);

        #endregion

        #endregion
    }
}