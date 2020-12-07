using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Shell;
using ControlLib.Components.Window.Data;
using ControlLib.Libs.Acrylic;
using WindowStyle = ControlLib.Components.Window.Data.WindowStyle;

namespace ControlLib.Components.Window {
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:ControlLib="clr-namespace:ControlLib.Components.Window"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:ControlLib="clr-namespace:ControlLib.Components.Window;assembly=ControlLib.Components.Window"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <ControlLib:AcrylicWindow/>
    ///
    /// </summary>
    public class AcrylicWindow : System.Windows.Window {
        static AcrylicWindow() {
            var type = typeof(AcrylicWindow);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));

            TintColorProperty = AcrylicElement.TintColorProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(Colors.White, FrameworkPropertyMetadataOptions.Inherits)
            );
            TintOpacityProperty = AcrylicElement.TintOpacityProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(0.6, FrameworkPropertyMetadataOptions.Inherits)
            );
            NoiseOpacityProperty = AcrylicElement.NoiseOpacityProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(0.03, FrameworkPropertyMetadataOptions.Inherits)
            );
            FallbackColorProperty = AcrylicElement.FallbackColorProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(Colors.LightGray, FrameworkPropertyMetadataOptions.Inherits)
            );
            ExtendViewIntoTitleBarProperty = AcrylicElement.ExtendViewIntoTitleBarProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits)
            );
            AcrylicWindowStyleProperty = AcrylicElement.AcrylicWindowStyleProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(Data.WindowStyle.Normal, FrameworkPropertyMetadataOptions.Inherits)
            );
            TitleBarProperty = AcrylicElement.TitleBarProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)
            );
            TitleBarModeProperty = AcrylicElement.TitleBarModeProperty.AddOwner(
                typeof(AcrylicWindow),
                new FrameworkPropertyMetadata(TitleBarMode.Default, FrameworkPropertyMetadataOptions.Inherits)
            );
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            EnableBlur(this);

            if (this.GetTemplateChild("captionGrid") is FrameworkElement caption) {
                caption.SizeChanged += (s, e) => {
                    var chrome = WindowChrome.GetWindowChrome(this);
                    chrome.CaptionHeight = e.NewSize.Height;
                };
            }
        }

        #region Inner Methods

        internal static void EnableBlur(System.Windows.Window win) {
            var windowHelper = new WindowInteropHelper(win);

            AcrylicHelper.EnableBlur(windowHelper.Handle);

            win.CommandBindings.Add(new CommandBinding(SystemCommands.CloseWindowCommand,
                (_, __) => { SystemCommands.CloseWindow(win); }));
            win.CommandBindings.Add(new CommandBinding(SystemCommands.MinimizeWindowCommand,
                (_, __) => { SystemCommands.MinimizeWindow(win); }));
            win.CommandBindings.Add(new CommandBinding(SystemCommands.MaximizeWindowCommand,
                (_, __) => { SystemCommands.MaximizeWindow(win); }));
            win.CommandBindings.Add(new CommandBinding(SystemCommands.RestoreWindowCommand,
                (_, __) => { SystemCommands.RestoreWindow(win); }));

            void OnContentRendered(object sender, EventArgs e) {
                if (win.SizeToContent != SizeToContent.Manual) {
                    win.InvalidateMeasure();
                }

                win.ContentRendered -= OnContentRendered;
            }

            win.ContentRendered += OnContentRendered;
        }

        #endregion

        #region Property Back Store

        /// <summary>
        /// Tint Color
        /// </summary>
        public static readonly DependencyProperty TintColorProperty;

        /// <summary>
        /// Tint Opacity
        /// </summary>
        public static readonly DependencyProperty TintOpacityProperty;

        /// <summary>
        /// Noise Opacity
        /// </summary>
        public static readonly DependencyProperty NoiseOpacityProperty;

        /// <summary>
        /// Fallback Color
        /// </summary>
        public static readonly DependencyProperty FallbackColorProperty;

        /// <summary>
        /// Extend View Into Title Bar
        /// </summary>
        public static readonly DependencyProperty ExtendViewIntoTitleBarProperty;

        /// <summary>
        /// Window Style
        /// </summary>
        public static readonly DependencyProperty AcrylicWindowStyleProperty;

        /// <summary>
        /// Title Bar
        /// </summary>
        public static readonly DependencyProperty TitleBarProperty;

        /// <summary>
        /// Title Bar Mode
        /// </summary>
        public static readonly DependencyProperty TitleBarModeProperty;

        #endregion

        #region Dependency Property

        public System.Windows.Media.Color TintColor {
            get => (System.Windows.Media.Color) GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public double TintOpacity {
            get => (double) GetValue(TintOpacityProperty);
            set => SetValue(TintOpacityProperty, value);
        }

        public double NoiseOpacity {
            get => (double) GetValue(NoiseOpacityProperty);
            set => SetValue(NoiseOpacityProperty, value);
        }

        public System.Windows.Media.Color FallbackColor {
            get => (System.Windows.Media.Color) GetValue(FallbackColorProperty);
            set => SetValue(FallbackColorProperty, value);
        }

        public bool ExtendViewIntoTitleBar {
            get => (bool) GetValue(ExtendViewIntoTitleBarProperty);
            set => SetValue(ExtendViewIntoTitleBarProperty, value);
        }

        public WindowStyle AcrylicWindowStyle {
            get => (WindowStyle) GetValue(AcrylicWindowStyleProperty);
            set => SetValue(AcrylicWindowStyleProperty, value);
        }

        public FrameworkElement TitleBar {
            get => (FrameworkElement) GetValue(TitleBarProperty);
            set => SetValue(TitleBarProperty, value);
        }

        public TitleBarMode TitleBarMode {
            get => (TitleBarMode) GetValue(TitleBarModeProperty);
            set => SetValue(TitleBarModeProperty, value);
        }

        #endregion

        #region Attached Property

        public static bool GetEnabled(DependencyObject obj) {
            return (bool) obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value) {
            obj.SetValue(EnabledProperty, value);
        }

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(AcrylicWindow),
            new PropertyMetadata(false, OnEnableChanged)
        );

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is not System.Windows.Window) return;
            System.Windows.Window win = (d as System.Windows.Window)!;
            var value = (bool) e.NewValue;
            if (!value) return;
            var dic = new ResourceDictionary() {
                Source = new Uri("pack://application:,,,/ControlLib;component/Styles/Window/Window.xaml")
            };
            var style = dic["AcrylicWindowStyle"] as Style;
            win.Style = style;

            win.Loaded += (_, __) => { EnableBlur(win); };
            if (win.IsLoaded) EnableBlur(win);
        }

        #endregion

        #region Acrylic Elements Methods

        public static double GetNoiseOpacity(DependencyObject obj) {
            return (double) obj.GetValue(AcrylicElement.NoiseOpacityProperty);
        }

        public static void SetNoiseOpacity(DependencyObject obj, double value) {
            obj.SetValue(AcrylicElement.NoiseOpacityProperty, value);
        }

        public static double GetTintOpacity(DependencyObject obj) {
            return (double) obj.GetValue(AcrylicElement.TintOpacityProperty);
        }

        public static void SetTintOpacity(DependencyObject obj, double value) {
            obj.SetValue(AcrylicElement.TintOpacityProperty, value);
        }

        public static System.Windows.Media.Color GetTintColor(DependencyObject obj) {
            return (System.Windows.Media.Color) obj.GetValue(AcrylicElement.TintColorProperty);
        }

        public static void SetTintColor(DependencyObject obj, System.Windows.Media.Color value) {
            obj.SetValue(AcrylicElement.TintColorProperty, value);
        }

        public static System.Windows.Media.Color GetFallbackColor(DependencyObject obj) {
            return (System.Windows.Media.Color) obj.GetValue(AcrylicElement.FallbackColorProperty);
        }

        public static void SetFallbackColor(DependencyObject obj, System.Windows.Media.Color value) {
            obj.SetValue(AcrylicElement.FallbackColorProperty, value);
        }

        public static bool GetExtendViewIntoTitleBar(DependencyObject obj) {
            return (bool) obj.GetValue(AcrylicElement.ExtendViewIntoTitleBarProperty);
        }

        public static void SetExtendViewIntoTitleBar(DependencyObject obj, bool value) {
            obj.SetValue(AcrylicElement.ExtendViewIntoTitleBarProperty, value);
        }

        public static WindowStyle GetAcrylicWindowStyle(DependencyObject obj) {
            return (WindowStyle) obj.GetValue(AcrylicElement.AcrylicWindowStyleProperty);
        }

        public static void SetAcrylicWindowStyle(DependencyObject obj, WindowStyle value) {
            obj.SetValue(AcrylicElement.AcrylicWindowStyleProperty, value);
        }

        public static FrameworkElement GetTitleBar(DependencyObject obj) {
            return (FrameworkElement) obj.GetValue(AcrylicElement.TitleBarProperty);
        }

        public static void SetTitleBar(DependencyObject obj, FrameworkElement value) {
            obj.SetValue(AcrylicElement.TitleBarProperty, value);
        }

        public static TitleBarMode GetTitleBarMode(DependencyObject obj) {
            return (TitleBarMode) obj.GetValue(AcrylicElement.TitleBarModeProperty);
        }

        public static void SetTitleBarMode(DependencyObject obj, TitleBarMode value) {
            obj.SetValue(AcrylicElement.TitleBarModeProperty, value);
        }

        #endregion
    }
}