using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ControlLib.Components.Layout {
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
    ///     <ControlLib:AcrylicPanel/>
    ///
    /// </summary>
    public class AcrylicPanel : ContentControl {
        #region Property

        #region Target

        public FrameworkElement Target {
            get => (FrameworkElement) GetValue(TargetProperty);
            set => SetValue(TargetProperty, value);
        }

        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register(
            "Target",
            typeof(FrameworkElement),
            typeof(AcrylicPanel),
            new PropertyMetadata(null)
        );

        #endregion

        #region Source

        public FrameworkElement Source {
            get => (FrameworkElement) GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly DependencyProperty SourceProperty = DependencyProperty.Register(
            "Source",
            typeof(FrameworkElement),
            typeof(AcrylicPanel),
            new PropertyMetadata(null)
        );

        #endregion

        #region Tint Color

        public Color TintColor {
            get => (Color) GetValue(TintColorProperty);
            set => SetValue(TintColorProperty, value);
        }

        public static readonly DependencyProperty TintColorProperty = DependencyProperty.Register(
            "TintColor",
            typeof(Color),
            typeof(AcrylicPanel),
            new PropertyMetadata(Colors.White)
        );

        #endregion

        #region Tint Opacity

        public double TintOpacity {
            get => (double) GetValue(TintOpacityProperty);
            set => SetValue(TintOpacityProperty, value);
        }

        public static readonly DependencyProperty TintOpacityProperty = DependencyProperty.Register(
            "TintOpacity",
            typeof(double),
            typeof(AcrylicPanel),
            new PropertyMetadata(0.0)
        );

        #endregion

        #region Noise Opacity

        public double NoiseOpacity {
            get => (double) GetValue(NoiseOpacityProperty);
            set => SetValue(NoiseOpacityProperty, value);
        }

        public static readonly DependencyProperty NoiseOpacityProperty = DependencyProperty.Register(
            "NoiseOpacity",
            typeof(double),
            typeof(AcrylicPanel),
            new PropertyMetadata(0.03)
        );

        #endregion

        private bool _isChanged;

        #endregion

        #region Construct

        static AcrylicPanel() {
            var type = typeof(AcrylicPanel);
            DefaultStyleKeyProperty.OverrideMetadata(type, new FrameworkPropertyMetadata(type));
        }

        public AcrylicPanel() { Source = this; }

        #endregion


        #region Override

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();

            if (GetTemplateChild("rect") is Rectangle rect) {
                rect.LayoutUpdated += (_, __) => {
                    if (_isChanged) return;
                    _isChanged = true;
                    BindingOperations.GetBindingExpressionBase(rect, RenderTransformProperty)?.UpdateTarget();

                    Dispatcher.BeginInvoke(
                        new Action(() => { _isChanged = false; }),
                        System.Windows.Threading.DispatcherPriority.DataBind
                    );
                };
            }
        }

        #endregion
    }
}