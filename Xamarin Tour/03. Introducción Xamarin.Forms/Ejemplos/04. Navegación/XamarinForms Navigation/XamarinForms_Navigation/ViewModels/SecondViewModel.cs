using System.Windows.Input;
using Xamarin.Forms;
using XamarinForms_Navigation.ViewModels.Base;

namespace XamarinForms_Navigation.ViewModels
{
    public class SecondViewModel : ViewModelBase
    {
        private INavigation _navigation;
        private DelegateCommand _backCommand;

        public SecondViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        public ICommand BackCommand
        {
            get { return _backCommand = _backCommand ?? new DelegateCommand(BackCommandExecute); }
        }

        private void BackCommandExecute()
        {
            _navigation.PopAsync();
        }
    }
}
