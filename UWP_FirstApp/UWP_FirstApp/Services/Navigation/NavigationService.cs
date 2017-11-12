using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Helpers;
using UWP_FirstApp.Views;
using Autofac;
using Windows.UI.Xaml.Controls;
using System;

namespace UWP_FirstApp.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        private bool _isNavigating;
        private IFrameAdapter Frame { get; }
        public NavigationService(IFrameAdapter frameAdapter, IComponentContext iocResolver)
        {
            Frame = frameAdapter;
        }

        public event EventHandler<bool> IsNavigatingChanged;

        public event EventHandler Navigated;

        public bool CanGoBack => Frame.CanGoBack;

        public Task NavigateToHomeAsync() => NavigateToPage<Home>();

        //public Task NavigateToFavoritesAsync() => NavigateToPage<Favorites>();

        //public Task NavigateToDownloadsAsync() => NavigateToPage<Downloads>();

        //public Task NavigateToNotesAsync() => NavigateToPage<Notes>();

        //public Task NavigateToNowPlayingAsync() => NavigateToPage<Player>();

        public Task NavigateToSettingsAsync() => NavigateToPage<SettingsPage>();

        public bool IsNavigating
        {
            get => _isNavigating;

            set
            {
                if (value != _isNavigating)
                {
                    _isNavigating = value;
                    IsNavigatingChanged?.Invoke(this, _isNavigating);

                    // Check that navigation just finished
                    if (!_isNavigating)
                    {
                        // Navigation finished
                        Navigated?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }

        public async Task GoBackAsync()
        {
            if (Frame.CanGoBack)
            {
                IsNavigating = true;

                Page navigatedPage = await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
                {
                    Frame.GoBack();
                    return Frame.Content as Page;
                });
            }
        }
        private Task NavigateToPage<TPage>()
        {
            return NavigateToPage<TPage>(parameter: null);
        }

        private async Task NavigateToPage<TPage>(object parameter)
        {
            // Early out if already in the middle of a Navigation
            if (_isNavigating)
            {
                return;
            }

            _isNavigating = true;

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Frame.Navigate(typeof(TPage), parameter: parameter);
            });
        }
    }
}
