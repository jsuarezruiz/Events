using GeoPic.Services.Navigation;
using GeoPic.ViewModels.Base;
using System.Threading.Tasks;

namespace GeoPic.ViewModels
{
    public class ExtendedSplashViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public ExtendedSplashViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            await Task.Delay(3000);
        
            _navigationService.NavigateTo<MainViewModel>();
            _navigationService.RemoveFirstPageFromBackStack();
        }
    }
}
