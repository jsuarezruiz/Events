using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms_Services.Services.Call;
using Xamarin.Forms_Services.ViewModels.Base;

namespace Xamarin.Forms_Services.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Services
        private ICallService _callService;

        // Commands
        private DelegateCommand _callCommand;

        public ICommand CallCommand
        {
            get { return _callCommand = _callCommand ?? new DelegateCommand(CallCommandExecute); }
        }

        private void CallCommandExecute()
        {
            _callService = DependencyService.Get<ICallService>();

            _callService.MakeCall("612345678");
        }
    }
}
