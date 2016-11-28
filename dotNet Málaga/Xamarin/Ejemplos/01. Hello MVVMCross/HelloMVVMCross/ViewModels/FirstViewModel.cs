using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace HelloMVVMCross.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
        private string _message;
        private int _clicCounter;

        private MvxCommand _helloCommand;

        public string Message
		{
            get { return _message; }
            set { _message = value; RaisePropertyChanged(() => Message); }
		}

        public ICommand HelloCommand
        {
            get
            {
                _helloCommand = _helloCommand ?? new MvxCommand(HelloCommandExecute);
                return _helloCommand;
            }
        }

        private void HelloCommandExecute()
        {
            _clicCounter++;
            Message = string.Format("Button clicked {0} times", _clicCounter);
        }
    }
}
