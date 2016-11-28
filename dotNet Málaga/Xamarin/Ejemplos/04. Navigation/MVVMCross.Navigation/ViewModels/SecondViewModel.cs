using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace MVVMCross.Navigation.ViewModels
{
    public class SecondViewModel : MvxViewModel
    {
        private string _parameter;
        private MvxCommand _goBackCommand;

        public string Parameter
        {
            get { return _parameter; }
            set { _parameter = value; RaisePropertyChanged(() => Parameter); }
        }

        public ICommand GoBackCommand
        {
            get
            {
                _goBackCommand = _goBackCommand ?? new MvxCommand(DoGoBackCommand);
                return _goBackCommand;
            }
        }

        public void Init(string parameter)
        {
            Parameter = parameter;
        }

        private void DoGoBackCommand()
        {
            Close(this);
        }
    }
}
