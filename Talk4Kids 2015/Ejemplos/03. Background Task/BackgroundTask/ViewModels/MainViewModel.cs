namespace BackgroundTask.ViewModels
{
    using System;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Navigation;
    using System.Windows.Input;
    using Windows.ApplicationModel.Background;
    using Helpers;
    using Base;

    public class MainViewModel : ViewModelBase
    {
        // Variables
        private string _info;

        // Commands
        private ICommand _registerCommand;
        private ICommand _unRegisterCommand;


        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
                RaisePropertyChanged();
            }
        }

        public ICommand RegisterCommand
        {
            get { return _registerCommand = _registerCommand ?? new DelegateCommandAsync(RegisterCommandExecute); }
        }

        public ICommand UnRegisterCommand
        {
            get { return _unRegisterCommand = _unRegisterCommand ?? new DelegateCommand(UnRegisterCommandExecute); }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }

        private async Task RegisterCommandExecute()
        {
            await BackgroundTaskHelper.RegisterBackgroundTasksAsync(typeof(global::Task.BackgroundTask),
                            new TimeTrigger(15, false));

            Info += "Task Registered" + Environment.NewLine;
        }

        private void UnRegisterCommandExecute()
        {
            BackgroundTaskHelper.UnregisterBackgroundTask(typeof(global::Task.BackgroundTask));

            Info += "Task Unregistered" + Environment.NewLine;
        }
    }
}
