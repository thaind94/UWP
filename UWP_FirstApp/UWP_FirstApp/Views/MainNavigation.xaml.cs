using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWP_FirstApp.Helpers;
using UWP_FirstApp.Services.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWP_FirstApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainNavigation : Page
    {
        private static MainNavigation _instance;

        private INavigationService _navigationService;

        public MainNavigation()
        {
            _instance = this;
            InitializeComponent();
            windowTitle.EnableLayoutImplicitAnimations(TimeSpan.FromMilliseconds(100));

            var nav = SystemNavigationManager.GetForCurrentView();

            nav.BackRequested += Nav_BackRequested;
        }

        private void Nav_BackRequested(object sender, BackRequestedEventArgs e)
        {
            var ignored = _navigationService.GoBackAsync();
            e.Handled = true;
        }

        public Frame AppFrame
        {
            get
            {
                return appNavFrame;
            }
        }

        public TitleBarHelper TitleHelper
        {
            get
            {
                return TitleBarHelper.Instance;
            }
        }

        public void InitializeNavigationService(INavigationService navigationService)
        {
            _navigationService = navigationService;
            // TODO: Hook into Navigation Events for loading screen
            _navigationService.Navigated += NavigationService_Navigated;
        }

        private void Navview_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                //_navigationService.NavigeToSettingsAsync();
            }
            switch (args.InvokedItem as string)
            {
                case "Home":
                    _navigationService.NavigateToHomeAsync();
                    break;
                //case "Now playing":
                //    _navigationService.NavigateToNowPlayingAsync();
                //    break;
                //case "Favorites":
                //    _navigationService.NavigateToFavoritesAsync();
                //    break;
                //case "Notes":
                //    _navigationService.NavigateToNotesAsync();
                //    break;
                //case "Downloads":
                //    _navigationService.NavigateToDownloadsAsync();
                //    break;
            }
        }

        private void NavigationService_Navigated(object sender, EventArgs e)
        {
            var ignored = DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                var nav = SystemNavigationManager.GetForCurrentView();
                nav.AppViewBackButtonVisibility = _navigationService.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            });
        }

        private void AppNavFrame_Navigated(object sender, NavigationEventArgs e)
        {
            var ignored = DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                var nav = SystemNavigationManager.GetForCurrentView();
                nav.AppViewBackButtonVisibility = _navigationService.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
            });
        }
    }
}
