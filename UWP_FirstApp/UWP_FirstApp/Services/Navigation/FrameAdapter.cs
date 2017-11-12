using System;
using UWP_FirstApp.Helpers;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace UWP_FirstApp.Services.Navigation
{
    public class FrameAdapter : IFrameAdapter
    {
        private Frame _internalFrame;

        public FrameAdapter(Frame internalFrame)
        {
            _internalFrame = internalFrame;
        }

        public event NavigationFailedEventHandler NavigationFailed { add => _internalFrame.NavigationFailed += value; remove => _internalFrame.NavigationFailed -= value; }

        public object Content => _internalFrame.Content;

        public bool CanGoBack => _internalFrame.CanGoBack;

        public void GoBack() => _internalFrame.GoBack();

        public bool Navigate(Type sourcePageType, object parameter)
        {
            return _internalFrame.NavigateWithFadeOutgoing(parameter, sourcePageType);
        }
    }
}
