using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void SayHello_ClickAsync(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponse = new HttpResponseMessage();
            Uri uri = new Uri("https://tv.zing.vn");
            string httpResponseBody = "";
            try
            {
                httpResponse = await httpClient.GetAsync(uri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                httpResponseBody = "ERROR" + ex.HResult.ToString("X") + "Message" + ex.Message;
            }
            var imageUrls = GetListImage(httpResponseBody);
            for (var i = 0; i < imageUrls.Count; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition
                {
                    Height = GridLength.Auto
                });
                var image = new Image
                {
                    Source = new BitmapImage(imageUrls[i])                    
                };
                Grid.SetRow(image, i);
                MainGrid.Children.Add(image);
            }
        }
        private List<Uri> GetListImage(string html)
        {
            string pattern = @"_fade_(.*?)\n(.*?)\n(.*?)src=""(.*?)""";
            var matchs = Regex.Matches(html, pattern);
            List<Uri> listImage = new List<Uri>();
            for (var i = 0; i < matchs.Count; i++)
            {
                listImage.Add(new Uri("https:" + matchs[i].Groups[4].Value));
            }
            return listImage;
        }
    }
}
