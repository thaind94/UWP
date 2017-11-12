using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP_FirstApp.Services.Navigation;

namespace UWP_FirstApp.ViewModels
{
    class HomeViewModel
    {
        private INavigationService _navigationService;

        public HomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}
