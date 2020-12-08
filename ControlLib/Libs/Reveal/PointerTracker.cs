using System.Windows;

namespace ControlLib.Libs.Reveal {
    public static class PointerTracker {
        #region Property

        #region Mouse X

        public static double GetX(DependencyObject obj) => (double) obj.GetValue(XProperty);

        private static void SetX(DependencyObject obj, double value) => obj.SetValue(XProperty, value);

        public static readonly DependencyProperty XProperty = DependencyProperty.RegisterAttached(
            "X",
            typeof(double),
            typeof(PointerTracker),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.Inherits)
        );

        #endregion

        #region Mouse Y

        public static double GetY(DependencyObject obj) => (double) obj.GetValue(YProperty);

        private static void SetY(DependencyObject obj, double value) => obj.SetValue(YProperty, value);

        public static readonly DependencyProperty YProperty = DependencyProperty.RegisterAttached(
            "Y",
            typeof(double),
            typeof(PointerTracker),
            new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.Inherits)
        );

        #endregion

        #region Position

        public static Point GetPosition(DependencyObject obj) =>
            (Point) obj.GetValue(PositionProperty);

        private static void SetPosition(DependencyObject obj, Point value) =>
            obj.SetValue(PositionProperty, value);

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty = DependencyProperty.RegisterAttached(
            "Position",
            typeof(Point),
            typeof(PointerTracker),
            new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.Inherits)
        );

        #endregion

        #region Mouse Enter

        public static bool GetIsEnter(DependencyObject obj) => (bool) obj.GetValue(IsEnterProperty);

        private static void SetIsEnter(DependencyObject obj, bool value) => obj.SetValue(IsEnterProperty, value);

        // Using a DependencyProperty as the backing store for IsEnter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnterProperty = DependencyProperty.RegisterAttached(
            "IsEnter",
            typeof(bool),
            typeof(PointerTracker),
            new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits)
        );

        #endregion

        #region Root

        public static UIElement GetRootObject(DependencyObject obj) =>
            (UIElement) obj.GetValue(RootObjectProperty);

        private static void SetRootObject(DependencyObject obj, UIElement value) =>
            obj.SetValue(RootObjectProperty, value);

        // Using a DependencyProperty as the backing store for RootObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RootObjectProperty = DependencyProperty.RegisterAttached(
            "RootObject",
            typeof(UIElement),
            typeof(PointerTracker),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits)
        );

        #endregion

        #region Enable

        public static bool GetEnabled(DependencyObject obj) =>
            (bool) obj.GetValue(EnabledProperty);

        public static void SetEnabled(DependencyObject obj, bool value) =>
            obj.SetValue(EnabledProperty, value);

        public static readonly DependencyProperty EnabledProperty = DependencyProperty.RegisterAttached(
            "Enabled",
            typeof(bool),
            typeof(PointerTracker),
            new PropertyMetadata(false, OnEnabledChanged)
        );

        #endregion

        #endregion

        #region Event

        private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var newValue = (bool) e.NewValue;
            var oldValue = (bool) e.OldValue;
            // ReSharper disable once InvertIf
            if (d is UIElement ctrl) {
                if (oldValue && !newValue) {
                    ctrl.MouseEnter -= MouseEnter;
                    ctrl.PreviewMouseMove -= PreviewMouseMove;
                    ctrl.MouseLeave -= MouseLeave;

                    ctrl.ClearValue(PointerTracker.RootObjectProperty);
                }

                // ReSharper disable once InvertIf
                if (!oldValue && newValue) {
                    ctrl.MouseEnter += MouseEnter;
                    ctrl.PreviewMouseMove += PreviewMouseMove;
                    ctrl.MouseLeave += MouseLeave;

                    SetRootObject(ctrl, ctrl);
                }
            }
        }

        #region Delegate

        private static void MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is UIElement ctrl) {
                SetIsEnter(ctrl, true);
            }
        }

        private static void PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            if (!(sender is UIElement ctrl) || !GetIsEnter(ctrl)) return;
            var pos = e.GetPosition(ctrl);

            SetX(ctrl, pos.X);
            SetY(ctrl, pos.Y);
            SetPosition(ctrl, pos);
        }

        private static void MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is UIElement ctrl) {
                SetIsEnter(ctrl, false);
            }
        }

        #endregion

        #endregion
    }
}