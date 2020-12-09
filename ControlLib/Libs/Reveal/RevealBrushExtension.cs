using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using ControlLib.Libs.Converter;

namespace ControlLib.Libs.Reveal {
    public class RevealBrushExtension : MarkupExtension {
        public RevealBrushExtension() { }

        public Color Color { get; set; } = Colors.Black;
        public double Opacity { get; set; } = 1;

        public double Radius { get; set; } = 100;

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var pvt = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            var target = pvt?.TargetObject as DependencyObject;

            var bgColor = Color.FromArgb(0, Color.R, Color.G, Color.B);
            var brush = new RadialGradientBrush(Color, bgColor) {
                MappingMode = BrushMappingMode.Absolute, RadiusX = Radius, RadiusY = Radius
            };

            var opacityBinding = new Binding("Opacity") {
                Source = target,
                Path = new PropertyPath(PointerTracker.IsEnterProperty),
                Converter = new OpacityConverter(),
                ConverterParameter = Opacity
            };
            BindingOperations.SetBinding(brush, Brush.OpacityProperty, opacityBinding);

            var binding = new MultiBinding {Converter = new RelativePositionConverter()};
            binding.Bindings.Add(new Binding
                {Source = target, Path = new PropertyPath(PointerTracker.RootObjectProperty)});
            binding.Bindings.Add(new Binding {Source = target});
            binding.Bindings.Add(
                new Binding {Source = target, Path = new PropertyPath(PointerTracker.PositionProperty)});

            BindingOperations.SetBinding(brush, RadialGradientBrush.CenterProperty, binding);
            BindingOperations.SetBinding(brush, RadialGradientBrush.GradientOriginProperty, binding);
            return brush;
        }
    }
}