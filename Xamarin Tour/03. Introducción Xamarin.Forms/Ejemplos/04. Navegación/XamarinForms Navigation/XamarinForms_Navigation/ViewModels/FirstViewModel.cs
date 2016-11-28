using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_Navigation.ViewModels.Base;
using XamarinForms_Navigation.Views;

namespace XamarinForms_Navigation.ViewModels
{
    public class FirstViewModel : ViewModelBase
    {
        private INavigation _navigation;
        private DelegateCommand _navigateCommand;

        public FirstViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public ICommand NavigateCommand
        {
            get { return _navigateCommand = _navigateCommand ?? new DelegateCommand(NavigateCommandExecute); }
        }

        private void NavigateCommandExecute()
        {
            _navigation.PushAsync(new SecondView());
        }
    }
}
