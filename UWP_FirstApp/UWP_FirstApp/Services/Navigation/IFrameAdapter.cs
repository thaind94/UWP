using System;
using Windows.UI.Xaml.Navigation;

namespace UWP_FirstApp.Services.Navigation
{
    public interface IFrameAdapter
    {

        event NavigationFailedEventHandler NavigationFailed;

        object Content { get; }

        bool CanGoBack { get; }

        bool Navigate(Type sourcePageType, object parameter);

        void GoBack();
    }
}
