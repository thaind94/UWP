﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Hosting;

namespace UWP_FirstApp.Helpers
{
    public static class NavigationAnimationHelpers
    {
        public static bool NavigateWithFadeOutgoing(this Frame frame, object parameter, Type destination)
        {
            ImplicitHideFrameContent(frame);

            return frame.Navigate(destination, parameter);
        }

        private static void ImplicitHideFrameContent(Frame frame)
        {
            if (frame.Content != null)
            {
                SetImplicitHide(frame.Content as UIElement);
            }
        }

        private static void SetImplicitHide(UIElement thisPtr)
        {
            ElementCompositionPreview.SetImplicitHideAnimation(thisPtr, VisualHelpers.CreateOpacityAnimation(0.4, 0));
            Canvas.SetTop(thisPtr, 1);
        }
    }
}
