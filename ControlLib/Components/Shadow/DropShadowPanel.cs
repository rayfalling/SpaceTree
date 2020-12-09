using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;
using ControlLib.Libs.Converter;

namespace ControlLib.Components.Shadow {
    public class DropShadowPanel : Decorator {
        #region Property

        #region Backgroud

        public Brush Background {
            get => (Brush) GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register(
            "Background",
            typeof(Brush),
            typeof(DropShadowPanel),
            new PropertyMetadata(null)
        );

        #endregion

        #region Blur Radius

        public double BlurRadius {
            get => (double) GetValue(BlurRadiusProperty);
            set => SetValue(BlurRadiusProperty, value);
        }

        public static readonly DependencyProperty BlurRadiusProperty = DependencyProperty.Register(
            "BlurRadius",
            typeof(double),
            typeof(DropShadowPanel),
            new PropertyMetadata(20.0)
        );

        #endregion

        #region Color

        public Color Color {
            get => (Color) GetValue(ColorProperty);
            set => SetValue(ColorProperty, value);
        }

        public static readonly DependencyProperty ColorProperty = DependencyProperty.Register(
            "Color", typeof(Color),
            typeof(DropShadowPanel),
            new PropertyMetadata(Colors.Black)
        );

        #endregion

        #region Direction

        public double Direction {
            get => (double) GetValue(DirectionProperty);
            set => SetValue(DirectionProperty, value);
        }

        public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register(
            "Direction",
            typeof(double),
            typeof(DropShadowPanel),
            new PropertyMetadata(315.0)
        );

        #endregion

        #region Shadow Opacity

        public double ShadowOpacity {
            get => (double) GetValue(ShadowOpacityProperty);
            set => SetValue(ShadowOpacityProperty, value);
        }

        public static readonly DependencyProperty ShadowOpacityProperty = DependencyProperty.Register(
            "ShadowOpacity",
            typeof(double),
            typeof(DropShadowPanel),
            new PropertyMetadata(0.8)
        );

        #endregion

        #region Rendering Bias

        public RenderingBias RenderingBias {
            get => (RenderingBias) GetValue(RenderingBiasProperty);
            set => SetValue(RenderingBiasProperty, value);
        }

        public static readonly DependencyProperty RenderingBiasProperty = DependencyProperty.Register(
            "RenderingBias",
            typeof(RenderingBias),
            typeof(DropShadowPanel),
            new PropertyMetadata(RenderingBias.Performance)
        );

        #endregion

        #region Shadow Depth

        public double ShadowDepth {
            get => (double) GetValue(ShadowDepthProperty);
            set => SetValue(ShadowDepthProperty, value);
        }

        public static readonly DependencyProperty ShadowDepthProperty = DependencyProperty.Register(
            "ShadowDepth",
            typeof(double),
            typeof(DropShadowPanel),
            new PropertyMetadata(0.0)
        );

        #endregion

        #region Shadow Mode

        public ShadowMode ShadowMode {
            get => (ShadowMode) GetValue(ShadowModeProperty);
            set => SetValue(ShadowModeProperty, value);
        }

        public static readonly DependencyProperty ShadowModeProperty = DependencyProperty.Register(
            "ShadowMode",
            typeof(ShadowMode),
            typeof(DropShadowPanel),
            new PropertyMetadata(ShadowMode.Content)
        );

        #endregion

        #region ContainerVisual

        private ContainerVisual InternalVisual { get; }

        #endregion

        #endregion

        #region Construct

        public DropShadowPanel() {
            InternalVisual = new ContainerVisual();
            AddVisualChild(InternalVisual);
        }

        #endregion

        #region Private Methods

        private UIElement InternalChild {
            get {
                var children = InternalVisual.Children!;
                if (children.Count != 0) return children[0] as UIElement ?? new UIElement();
                return new UIElement();
            }
            set {
                var children = InternalVisual.Children;
                if (children.Count != 0) children.Clear();
                children.Add(value);
            }
        }

        protected internal UIElement CreateInternalVisual(UIElement value) {
            var effect = new DropShadowEffect();
            BindingOperations.SetBinding(effect, DropShadowEffect.BlurRadiusProperty,
                new Binding("BlurRadius") {Source = this});
            BindingOperations.SetBinding(effect, DropShadowEffect.ColorProperty,
                new Binding("Color") {Source = this});
            BindingOperations.SetBinding(effect, DropShadowEffect.DirectionProperty,
                new Binding("Direction") {Source = this});
            BindingOperations.SetBinding(effect, DropShadowEffect.OpacityProperty,
                new Binding("ShadowOpacity") {Source = this});
            BindingOperations.SetBinding(effect, DropShadowEffect.RenderingBiasProperty,
                new Binding("RenderingBias") {Source = this});
            BindingOperations.SetBinding(effect, DropShadowEffect.ShadowDepthProperty,
                new Binding("ShadowDepth") {Source = this});

            var st = new Style();

            var brush = new VisualBrush(value) {
                TileMode = TileMode.None,
                Stretch = Stretch.None,
                AlignmentX = AlignmentX.Left,
                AlignmentY = AlignmentY.Top,
                ViewboxUnits = BrushMappingMode.Absolute
            };
            var contentTrigger = new DataTrigger {
                Binding = new Binding("ShadowMode") {Source = this},
                Value = ShadowMode.Content,
            };
            contentTrigger.Setters.Add(new Setter {
                Property = Shape.FillProperty,
                Value = brush
            });
            st.Triggers.Add(contentTrigger);

            var outerTrigger = new DataTrigger {
                Binding = new Binding("ShadowMode") {Source = this},
                Value = ShadowMode.Outer,
            };
            outerTrigger.Setters.Add(new Setter {
                Property = ClipProperty,
                Value = new MultiBinding {
                    Bindings = {
                        new Binding("ActualWidth") {Source = this},
                        new Binding("ActualHeight") {Source = this},
                        new Binding("BlurRadius") {Source = this},
                    },
                    Converter = new ClipInnerRectConverter()
                }
            });
            outerTrigger.Setters.Add(new Setter {
                Property = Shape.FillProperty,
                Value = Brushes.White
            });
            st.Triggers.Add(outerTrigger);

            var innerTrigger = new DataTrigger {
                Binding = new Binding("ShadowMode") {Source = this},
                Value = ShadowMode.Inner,
            };
            innerTrigger.Setters.Add(new Setter {
                Property = Shape.StrokeProperty,
                Value = Brushes.White
            });
            innerTrigger.Setters.Add(new Setter {
                Property = Shape.StrokeThicknessProperty,
                Value = new Binding("BlurRadius") {Source = this}
            });
            innerTrigger.Setters.Add(new Setter {
                Property = MarginProperty,
                Value = new Binding("BlurRadius") {Source = this, Converter = new NegativeMarginConverter()}
            });
            innerTrigger.Setters.Add(new Setter {
                Property = ClipProperty,
                Value = new MultiBinding {
                    Bindings = {
                        new Binding("ActualWidth") {Source = this},
                        new Binding("ActualHeight") {Source = this},
                        new Binding("BlurRadius") {Source = this},
                    },
                    Converter = new ClipOuterRectConverter()
                }
            });
            st.Triggers.Add(innerTrigger);

            var border = new Rectangle {
                Effect = effect,
                Style = st,
            };

            var grid = new Grid();
            BindingOperations.SetBinding(grid, Panel.BackgroundProperty, new Binding("Background") {Source = this});
            grid.Children.Add(border);
            grid.Children.Add(value);

            return grid;
        }

        #endregion

        #region Override

        public override UIElement Child {
            get => InternalChild;
            set {
                var old = InternalChild;

                if (old == value) return;
                RemoveLogicalChild(old);

                var ic = CreateInternalVisual(value);
                AddLogicalChild(ic);
                InternalChild = ic;

                InvalidateMeasure();
            }
        }

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index) {
            if (index != 0) {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return InternalVisual;
        }

        protected override IEnumerator LogicalChildren {
            get {
                var list = new List<UIElement> {InternalChild};
                return list.GetEnumerator();
            }
        }

        #endregion
    }
}