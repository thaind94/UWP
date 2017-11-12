using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_FirstApp.Helpers;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;

namespace UWP_FirstApp.Services
{
    public static class ThemeSelectorService
    {
        private const string SettingsKey = "RequestedTheme";

        private static ResourceDictionary _customTheme = new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Branded.xaml", UriKind.Absolute) };

        private static ResourceDictionary _stockTheme = new ResourceDictionary { Source = new Uri("ms-appx:///Themes/Stock.xaml", UriKind.Absolute) };

        public static ElementThemeExtended Theme { get; set; } = ElementThemeExtended.Default;

        public static void SetRequestedTheme()
        {
            if (Window.Current.Content is FrameworkElement frameworkElement)
            {
                ElementTheme trueTheme;
                if (Theme == ElementThemeExtended.Custom)
                {
                    if (Application.Current.Resources.MergedDictionaries.Contains(_stockTheme))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(_stockTheme);
                    }

                    Application.Current.Resources.MergedDictionaries.Add(_customTheme);

                    trueTheme = ElementTheme.Dark;

                    if (frameworkElement.RequestedTheme == ElementTheme.Dark)
                    {
                        frameworkElement.RequestedTheme = ElementTheme.Light;
                    }
                }
                else
                {
                    if (Application.Current.Resources.MergedDictionaries.Contains(_customTheme))
                    {
                        Application.Current.Resources.MergedDictionaries.Remove(_customTheme);
                    }

                    // for the case we switch between light and dark which share stock
                    if (!Application.Current.Resources.MergedDictionaries.Contains(_stockTheme))
                    {
                        Application.Current.Resources.MergedDictionaries.Add(_stockTheme);
                    }

                    trueTheme = (ElementTheme)Theme;

                    if (frameworkElement.RequestedTheme == ElementTheme.Dark)
                    {
                        frameworkElement.RequestedTheme = ElementTheme.Light;
                    }
                }

                frameworkElement.RequestedTheme = trueTheme;
            }

            SetupTitlebar();

        }

        public static ElementTheme GetHomeTheme()
        {
            if (Theme == ElementThemeExtended.Custom)
            {
                return ElementTheme.Light;
            }

            return TrueTheme();
        }

        public static Style GetHomeBackground()
        {
            if (Theme == ElementThemeExtended.Custom)
            {
                return Application.Current.Resources["HomePageBackground"] as Style;
            }

            return Application.Current.Resources["PageBackground"] as Style;
        }

        public static ElementTheme TrueTheme()
        {
            var frameworkElement = Window.Current.Content as FrameworkElement;
            return frameworkElement.ActualTheme;
        }

        private static void SetupTitlebar()
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = Colors.Transparent;
                    if (TrueTheme() == ElementTheme.Dark)
                    {
                        titleBar.ButtonForegroundColor = Colors.White;
                        titleBar.ForegroundColor = Colors.White;
                    }
                    else
                    {
                        titleBar.ButtonForegroundColor = Colors.Black;
                        titleBar.ForegroundColor = Colors.Black;
                    }

                    titleBar.BackgroundColor = Colors.Black;

                    titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
                    titleBar.ButtonInactiveForegroundColor = Colors.LightGray;

                    CoreApplicationViewTitleBar coreTitleBar = TitleBarHelper.Instance.TitleBar;

                    coreTitleBar.ExtendViewIntoTitleBar = true;
                }
            }
        }
    }
}
