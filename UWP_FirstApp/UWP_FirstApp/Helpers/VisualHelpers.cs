using System;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Hosting;

namespace UWP_FirstApp.Helpers
{
    public static class VisualHelpers
    {
        public static Visual GetVisual(this UIElement element)
        {
            return ElementCompositionPreview.GetElementVisual(element);
        }

        public static ICompositionAnimationBase CreateOpacityAnimation(double seconds, float finalvalue)
        {
            var animation = Window.Current.Compositor.CreateScalarKeyFrameAnimation();
            animation.Target = nameof(Visual.Opacity);
            animation.Duration = TimeSpan.FromSeconds(seconds);
            animation.InsertKeyFrame(1, finalvalue);
            return animation;
        }

        public static void EnableLayoutImplicitAnimations(this UIElement element, TimeSpan t)
        {
            Compositor compositor;
            var result = element.GetVisual();
            compositor = result.Compositor;

            var elementImplicitAnimation = compositor.CreateImplicitAnimationCollection();
            elementImplicitAnimation[nameof(Visual.Offset)] = CreateOffsetAnimation(compositor, t);

            result.ImplicitAnimations = elementImplicitAnimation;
        }

        private static KeyFrameAnimation CreateOffsetAnimation(Compositor compositor, TimeSpan duration)
        {
            Vector3KeyFrameAnimation kf = compositor.CreateVector3KeyFrameAnimation();
            kf.InsertExpressionKeyFrame(1.0f, "this.FinalValue");
            kf.Duration = duration;
            kf.Target = "Offset";
            return kf;
        }
    }
}
