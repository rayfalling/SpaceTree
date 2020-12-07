using System.Windows;

namespace ControlLib.Libs.Reveal {
    public static class PointerTracker {
        public static double GetX(DependencyObject obj) {
            return (double) obj.GetValue(XProperty);
        }

        private static void SetX(DependencyObject obj, double value) {
            obj.SetValue(XProperty, value);
        }

        // Using a DependencyProperty as the backing store for X.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty XProperty =
            DependencyProperty.RegisterAttached("X", typeof(double), typeof(PointerTracker),
                new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.Inherits));


        public static double GetY(DependencyObject obj) {
            return (double) obj.GetValue(YProperty);
        }

        private static void SetY(DependencyObject obj, double value) {
            obj.SetValue(YProperty, value);
        }

        // Using a DependencyProperty as the backing store for Y.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty YProperty =
            DependencyProperty.RegisterAttached("Y", typeof(double), typeof(PointerTracker),
                new FrameworkPropertyMetadata(double.NaN, FrameworkPropertyMetadataOptions.Inherits));


        public static Point GetPosition(DependencyObject obj) {
            return (Point) obj.GetValue(PositionProperty);
        }

        private static void SetPosition(DependencyObject obj, Point value) {
            obj.SetValue(PositionProperty, value);
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
            DependencyProperty.RegisterAttached("Position", typeof(Point), typeof(PointerTracker),
                new FrameworkPropertyMetadata(new Point(0, 0), FrameworkPropertyMetadataOptions.Inherits));


        public static bool GetIsEnter(DependencyObject obj) {
            return (bool) obj.GetValue(IsEnterProperty);
        }

        private static void SetIsEnter(DependencyObject obj, bool value) {
            obj.SetValue(IsEnterProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsEnter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsEnterProperty =
            DependencyProperty.RegisterAttached("IsEnter", typeof(bool), typeof(PointerTracker),
                new FrameworkPropertyMetadata(false, FrameworkPropertyMetadataOptions.Inherits));


        public static UIElement GetRootObject(DependencyObject obj) {
            return (UIElement) obj.GetValue(RootObjectProperty);
        }

        private static void SetRootObject(DependencyObject obj, UIElement value) {
            obj.SetValue(RootObjectProperty, value);
        }

        // Using a DependencyProperty as the backing store for RootObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RootObjectProperty =
            DependencyProperty.RegisterAttached("RootObject", typeof(UIElement), typeof(PointerTracker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));


        public static bool GetEnabled(DependencyObject obj) {
            return (bool) obj.GetValue(EnabledProperty);
        }

        public static void SetEnabled(DependencyObject obj, bool value) {
            obj.SetValue(EnabledProperty, value);
        }

        // Using a DependencyProperty as the backing store for Enabled.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnabledProperty =
            DependencyProperty.RegisterAttached("Enabled", typeof(bool), typeof(PointerTracker),
                new PropertyMetadata(false, OnEnabledChanged));

        private static void OnEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var ctrl = d as UIElement;
            var newValue = (bool) e.NewValue;
            var oldValue = (bool) e.OldValue;
            if (ctrl == null) return;

            if (oldValue && !newValue) {
                ctrl.MouseEnter -= Ctrl_MouseEnter;
                ctrl.PreviewMouseMove -= Ctrl_PreviewMouseMove;
                ctrl.MouseLeave -= Ctrl_MouseLeave;

                ctrl.ClearValue(PointerTracker.RootObjectProperty);
            }

            if (!oldValue && newValue) {
                ctrl.MouseEnter += Ctrl_MouseEnter;
                ctrl.PreviewMouseMove += Ctrl_PreviewMouseMove;
                ctrl.MouseLeave += Ctrl_MouseLeave;

                SetRootObject(ctrl, ctrl);
            }
        }

        private static void Ctrl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
            var ctrl = sender as UIElement;
            if (ctrl != null) {
                SetIsEnter(ctrl, true);
            }
        }

        private static void Ctrl_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
            if (!(sender is UIElement ctrl) || !GetIsEnter(ctrl)) return;
            var pos = e.GetPosition(ctrl);

            SetX(ctrl, pos.X);
            SetY(ctrl, pos.Y);
            SetPosition(ctrl, pos);
        }

        private static void Ctrl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
            if (sender is UIElement ctrl) {
                SetIsEnter(ctrl, false);
            }
        }
    }
}