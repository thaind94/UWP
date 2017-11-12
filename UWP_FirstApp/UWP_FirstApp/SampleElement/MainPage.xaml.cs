using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWP_FirstApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private static MainPage _instance;
        private INavigationService _navigationService;
        public MainPage()
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
                //_navigationService.NavigateToSettingsAsync();
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
        //private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        //{
        //    splitView.IsPaneOpen = !splitView.IsPaneOpen;
        //}

        //private void IconsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (Page1.IsSelected) mainFrame.Navigate(typeof(Page1));
        //    else if (Page2.IsSelected) mainFrame.Navigate(typeof(Page2));
        //    else mainFrame.Navigate(typeof(Page3));
        //    ChangeTitle();
        //}

        //private void ChangeTitle()
        //{
        //    if (mainFrame.CurrentSourcePageType == typeof(Page1))
        //    {
        //        Back.Visibility = Visibility.Collapsed;
        //        PageTitle.Text = "Page 1";
        //    }
        //    else if (mainFrame.CurrentSourcePageType == typeof(Page2))
        //    {
        //        Back.Visibility = Visibility.Visible;
        //        PageTitle.Text = "Page 2";
        //    }
        //    else if (mainFrame.CurrentSourcePageType == typeof(Page3))
        //    {
        //        Back.Visibility = Visibility.Visible;
        //        PageTitle.Text = "Page 3";
        //    }
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    splitView.IsPaneOpen = !splitView.IsPaneOpen;
        //}

        //private void Home_Click(object sender, RoutedEventArgs e)
        //{
        //    mainFrame.Navigate(typeof(Page1));
        //}

        //private void Back_Click(object sender, RoutedEventArgs e)
        //{
        //    if (mainFrame.CanGoBack) mainFrame.GoBack();
        //    ChangeTitle();
        //}

        //private void Next_Click(object sender, RoutedEventArgs e)
        //{
        //    if (mainFrame.CanGoForward) mainFrame.GoForward();
        //}

        //private void hamburgerButton_Click(object sender, RoutedEventArgs e)
        //{

        //}

        //private async void SayHello_ClickAsync(object sender, RoutedEventArgs e)
        //{
        //    HttpClient httpClient = new HttpClient();
        //    HttpResponseMessage httpResponse = new HttpResponseMessage();
        //    Uri uri = new Uri("https://tv.zing.vn");
        //    string httpResponseBody = "";
        //    try
        //    {
        //        httpResponse = await httpClient.GetAsync(uri);
        //        httpResponse.EnsureSuccessStatusCode();
        //        httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        httpResponseBody = "ERROR" + ex.HResult.ToString("X") + "Message" + ex.Message;
        //    }
        //    var imageUrls = GetListImage(httpResponseBody);
        //    for (var i = 0; i < imageUrls.Count; i++)
        //    {
        //        //MainGrid.RowDefinitions.Add(new RowDefinition
        //        //{
        //        //    Height = GridLength.Auto
        //        //});
        //        var image = new Image
        //        {
        //            Source = new BitmapImage(imageUrls[i])                    
        //        };
        //        //Grid.SetRow(image, i);
        //        slideGrid.Children.Add(image);                
        //    }
        //    zing_slide.Height = slideGrid.Height;

        //}
        //private List<Uri> GetListImage(string html)
        //{
        //    string pattern = @"_fade_(.*?)\n(.*?)\n(.*?)src=""(.*?)""";
        //    var matchs = Regex.Matches(html, pattern);
        //    List<Uri> listImage = new List<Uri>();
        //    for (var i = 0; i < matchs.Count; i++)
        //    {
        //        listImage.Add(new Uri("https:" + matchs[i].Groups[4].Value));
        //    }
        //    return listImage;
        //}
    }
}
