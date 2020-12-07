using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ControlLib.Animations {
    public class BrushAnimation : AnimationTimeline {
        #region Override

        public override Type TargetPropertyType => typeof(Brush);

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue,
            AnimationClock animationClock) {
            if (!animationClock.CurrentProgress.HasValue)
                return Brushes.Transparent;

            var originValue = From;
            var dstValue = To;

            var progress = animationClock.CurrentProgress.Value;
            if (progress == 0)
                return originValue;
            if (Math.Abs(progress - 1) < Tolerance)
                return dstValue;

            var easingFunction = EasingFunction;
            progress = easingFunction.Ease(progress);

            return new VisualBrush(new Border() {
                Width = 1,
                Height = 1,
                Background = originValue,
                Child = new Border() {
                    Background = dstValue,
                    Opacity = progress,
                }
            });
        }

        protected override Freezable CreateInstanceCore() {
            return new BrushAnimation();
        }

        #endregion

        private const double Tolerance = 0.01;

        #region From

        public Brush From {
            get => (Brush) GetValue(FromProperty);
            set => SetValue(FromProperty, value);
        }

        public static readonly DependencyProperty FromProperty = DependencyProperty.Register(
            "From",
            typeof(Brush),
            typeof(BrushAnimation),
            new PropertyMetadata(null)
        );

        #endregion

        #region To

        public Brush To {
            get => (Brush) GetValue(ToProperty);
            set => SetValue(ToProperty, value);
        }

        public static readonly DependencyProperty ToProperty = DependencyProperty.Register(
            "To", typeof(Brush),
            typeof(BrushAnimation),
            new PropertyMetadata(null)
        );

        #endregion

        #region EasingFunction

        public IEasingFunction EasingFunction {
            get => (IEasingFunction) GetValue(EasingFunctionProperty);
            set => SetValue(EasingFunctionProperty, value);
        }

        public static readonly DependencyProperty EasingFunctionProperty = DependencyProperty.Register(
            "EasingFunction",
            typeof(IEasingFunction),
            typeof(BrushAnimation),
            new PropertyMetadata(null)
        );

        #endregion
    }
}