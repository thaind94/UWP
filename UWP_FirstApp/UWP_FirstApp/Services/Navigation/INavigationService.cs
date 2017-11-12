using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP_FirstApp.Services.Navigation
{
    public interface INavigationService
    {
        event EventHandler Navigated;

        bool CanGoBack { get; }

        Task NavigateToHomeAsync();

        //Task NavigateToFavoritesAsync();

        //Task NavigateToDownloadsAsync();

        //Task NavigateToNotesAsync();

        //Task NavigateToNowPlayingAsync();

        Task NavigateToSettingsAsync();

        Task GoBackAsync();
    }
}
