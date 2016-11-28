namespace SilverlightApp.ViewModels
{
    using System.Windows.Input;
    using Base;

    public class MainViewModel : ViewModelBase
    {
        private int _count;
        private string _message;

        private ICommand _helloCommand;

        public MainViewModel()
        {
            _count = 0;
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertyChanged();
            }
        }

        public ICommand HelloCommand
        {
            get { return _helloCommand = _helloCommand ?? new DelegateCommand(HelloCommandExecute); }
        }

        private void HelloCommandExecute()
        {
            _count++;
            Message = $"Count: {_count}";
        }
    }
}
