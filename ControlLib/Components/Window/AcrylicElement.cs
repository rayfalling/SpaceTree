using System.Windows;
using System.Windows.Media;
using ControlLib.Components.Window.Data;
using WindowStyle = ControlLib.Components.Window.Data.WindowStyle;

namespace ControlLib.Components.Window {
    internal class AcrylicElement {
        #region TintColor

        public static readonly DependencyProperty TintColorProperty = DependencyProperty.RegisterAttached(
            "TintColor",
            typeof(System.Windows.Media.Color),
            typeof(AcrylicElement),
            new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.Inherits)
        );

        public static System.Windows.Media.Color GetTintColor(DependencyObject obj) =>
            (System.Windows.Media.Color) obj.GetValue(TintColorProperty);

        public static void SetTintColor(DependencyObject obj, System.Windows.Media.Color value) =>
            obj.SetValue(TintColorProperty, value);

        #endregion

        #region TintOpacity

        public static readonly DependencyProperty TintOpacityProperty = DependencyProperty.RegisterAttached(
            "TintOpacity",
            typeof(double),
            typeof(AcrylicElement),
            new PropertyMetadata(0.6)
        );

        public static double GetTintOpacity(DependencyObject obj) =>
            (double) obj.GetValue(TintOpacityProperty);

        public static void SetTintOpacity(DependencyObject obj, double value) =>
            obj.SetValue(TintOpacityProperty, value);

        #endregion

        #region NoiseOpacity

        public static readonly DependencyProperty NoiseOpacityProperty = DependencyProperty.RegisterAttached(
            "NoiseOpacity",
            typeof(double),
            typeof(AcrylicElement),
            new PropertyMetadata(0.03)
        );

        public static double GetNoiseOpacity(DependencyObject obj) =>
            (double) obj.GetValue(NoiseOpacityProperty);

        public static void SetNoiseOpacity(DependencyObject obj, double value) =>
            obj.SetValue(NoiseOpacityProperty, value);

        #endregion

        #region FallbackColor

        public static readonly DependencyProperty FallbackColorProperty = DependencyProperty.RegisterAttached(
            "FallbackColor",
            typeof(System.Windows.Media.Color),
            typeof(AcrylicElement),
            new FrameworkPropertyMetadata(Colors.LightGray, FrameworkPropertyMetadataOptions.Inherits)
        );

        public static System.Windows.Media.Color GetFallbackColor(DependencyObject obj) =>
            (System.Windows.Media.Color) obj.GetValue(FallbackColorProperty);

        public static void SetFallbackColor(DependencyObject obj, System.Windows.Media.Color value) =>
            obj.SetValue(FallbackColorProperty, value);

        #endregion

        #region ExtendViewIntoTitleBar

        public static readonly DependencyProperty ExtendViewIntoTitleBarProperty = DependencyProperty.RegisterAttached(
            "ExtendViewIntoTitleBar",
            typeof(bool),
            typeof(AcrylicElement),
            new PropertyMetadata(false)
        );

        public static bool GetExtendViewIntoTitleBar(DependencyObject obj) =>
            (bool) obj.GetValue(ExtendViewIntoTitleBarProperty);

        public static void SetExtendViewIntoTitleBar(DependencyObject obj, bool value) =>
            obj.SetValue(ExtendViewIntoTitleBarProperty, value);

        #endregion

        #region WindowStyle

        public static readonly DependencyProperty AcrylicWindowStyleProperty = DependencyProperty.RegisterAttached(
            "AcrylicWindowStyle",
            typeof(WindowStyle),
            typeof(AcrylicElement),
            new PropertyMetadata(WindowStyle.Normal)
        );

        public static WindowStyle GetWindowStyle(DependencyObject obj) =>
            (WindowStyle) obj.GetValue(AcrylicWindowStyleProperty);

        public static void WindowStyleBar(DependencyObject obj, WindowStyle value) =>
            obj.SetValue(AcrylicWindowStyleProperty, value);

        #endregion

        #region TitleBar

        public static readonly DependencyProperty TitleBarProperty = DependencyProperty.RegisterAttached(
            "TitleBar",
            typeof(FrameworkElement),
            typeof(AcrylicElement),
            new PropertyMetadata(null)
        );

        public static FrameworkElement GetTitleBar(DependencyObject obj) =>
            (FrameworkElement) obj.GetValue(TitleBarProperty);

        public static void SetTitleBar(DependencyObject obj, FrameworkElement value) =>
            obj.SetValue(TitleBarProperty, value);

        #endregion

        #region TitleBarMode

        public static readonly DependencyProperty TitleBarModeProperty = DependencyProperty.RegisterAttached(
            "TitleBarMode",
            typeof(TitleBarMode),
            typeof(AcrylicElement),
            new PropertyMetadata(TitleBarMode.Default)
        );

        public static TitleBarMode GetTitleBarMode(DependencyObject obj) =>
            (TitleBarMode) obj.GetValue(TitleBarModeProperty);

        public static void SetTitleBarMode(DependencyObject obj, TitleBarMode value) =>
            obj.SetValue(TitleBarModeProperty, value);

        #endregion
    }
}