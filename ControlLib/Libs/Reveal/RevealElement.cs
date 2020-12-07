using System.Windows;
using System.Windows.Media;

namespace ControlLib.Libs.Reveal {
    public class RevealElement {
        public static Brush GetMouseOverForeground(DependencyObject obj) {
            return (Brush) obj.GetValue(MouseOverForegroundProperty);
        }

        public static void SetMouseOverForeground(DependencyObject obj, Brush value) {
            obj.SetValue(MouseOverForegroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverForeground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.RegisterAttached("MouseOverForeground", typeof(Brush), typeof(RevealElement),
                new FrameworkPropertyMetadata(Brushes.Black, FrameworkPropertyMetadataOptions.Inherits));


        public static Brush GetMouseOverBackground(DependencyObject obj) {
            return (Brush) obj.GetValue(MouseOverBackgroundProperty);
        }

        public static void SetMouseOverBackground(DependencyObject obj, Brush value) {
            obj.SetValue(MouseOverBackgroundProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.RegisterAttached("MouseOverBackground", typeof(Brush), typeof(RevealElement),
                new FrameworkPropertyMetadata(Brushes.White, FrameworkPropertyMetadataOptions.Inherits));


        public static double GetBorderRadius(DependencyObject obj) {
            return (double) obj.GetValue(BorderRadiusProperty);
        }

        public static void SetBorderRadius(DependencyObject obj, double value) {
            obj.SetValue(BorderRadiusProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderRadiusProperty =
            DependencyProperty.RegisterAttached("BorderRadius", typeof(double), typeof(RevealElement),
                new FrameworkPropertyMetadata(0.0, FrameworkPropertyMetadataOptions.Inherits));


        public static double GetBorderOpacity(DependencyObject obj) {
            return (double) obj.GetValue(BorderOpacityProperty);
        }

        public static void SetBorderOpacity(DependencyObject obj, double value) {
            obj.SetValue(BorderOpacityProperty, value);
        }

        // Using a DependencyProperty as the backing store for BorderOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BorderOpacityProperty =
            DependencyProperty.RegisterAttached("BorderOpacity", typeof(double), typeof(RevealElement),
                new PropertyMetadata(0.0));


        public static double GetMouseOverBorderOpacity(DependencyObject obj) {
            return (double) obj.GetValue(MouseOverBorderOpacityProperty);
        }

        public static void SetMouseOverBorderOpacity(DependencyObject obj, double value) {
            obj.SetValue(MouseOverBorderOpacityProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseOverBorderOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseOverBorderOpacityProperty =
            DependencyProperty.RegisterAttached("MouseOverBorderOpacity", typeof(double), typeof(RevealElement),
                new PropertyMetadata(0.5));


        public static double GetPressBorderOpacity(DependencyObject obj) {
            return (double) obj.GetValue(PressBorderOpacityProperty);
        }

        public static void SetPressBorderOpacity(DependencyObject obj, double value) {
            obj.SetValue(PressBorderOpacityProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressBorderOpacity.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressBorderOpacityProperty =
            DependencyProperty.RegisterAttached("PressBorderOpacity", typeof(double), typeof(RevealElement),
                new PropertyMetadata(0.5));


        public static Brush GetPressTintBrush(DependencyObject obj) {
            return (Brush) obj.GetValue(PressTintBrushProperty);
        }

        public static void SetPressTintBrush(DependencyObject obj, Brush value) {
            obj.SetValue(PressTintBrushProperty, value);
        }

        // Using a DependencyProperty as the backing store for PressTintBrush.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PressTintBrushProperty =
            DependencyProperty.RegisterAttached("PressTintBrush", typeof(Brush), typeof(RevealElement),
                new PropertyMetadata(Brushes.Gray));
    }
}