using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using ControlLib.Components.Layout;

namespace ControlLib.Libs.Acrylic {
    // ReSharper disable once UnusedMember.Global
    internal class AcrylicBrushExtension : MarkupExtension {
        public string TargetName { get; set; }

        public Color TintColor { get; set; } = Colors.White;

        public double TintOpacity { get; set; } = 0.0;

        public double NoiseOpacity { get; set; } = 0.03;

        public AcrylicBrushExtension() { TargetName = ""; }

        public AcrylicBrushExtension(string target, string targetName) { TargetName = target; }

        public override object ProvideValue(IServiceProvider serviceProvider) {
            var pvt = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;

            if (pvt?.TargetObject is FrameworkElement target) {
                var acrylicPanel = new AcrylicPanel() {
                    TintColor = TintColor,
                    TintOpacity = TintOpacity,
                    NoiseOpacity = NoiseOpacity,
                    Width = target.Width,
                    Height = target.Height
                };
                BindingOperations.SetBinding(acrylicPanel, AcrylicPanel.TargetProperty,
                    new Binding() {ElementName = TargetName});
                BindingOperations.SetBinding(acrylicPanel, AcrylicPanel.SourceProperty,
                    new Binding() {Source = target});

                return new VisualBrush(acrylicPanel) {
                    Stretch = Stretch.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Top,
                    ViewboxUnits = BrushMappingMode.Absolute,
                };
            }

            return new object();
        }
    }
}