namespace ApplicationTrigger.ViewModels
{
    using System;
    using System.Diagnostics;
    using Windows.ApplicationModel.Core;
    using Windows.UI.Core;
    using Task.Helpers;
    using Windows.Storage;
    using System.Threading.Tasks;
    using Windows.UI.Xaml.Navigation;
    using Windows.ApplicationModel.Background;
    using System.Windows.Input;
    using Task;
    using Helpers;
    using Base;

    public class MainViewModel : ViewModelBase
    {
        // Variables
        private string _info;
        private ApplicationTrigger _trigger;

        // Commands
        private ICommand _registerCommand;
        private ICommand _unRegisterCommand;
        private ICommand _signalCommand;

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

        public ICommand SignalCommand
        {
            get { return _signalCommand = _signalCommand ?? new DelegateCommandAsync(SignalCommandExecute); }
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
            _trigger = new ApplicationTrigger();

            var task = await BackgroundTaskHelper.RegisterApplicationTrigger(typeof(BackgroundTask), _trigger);

            AttachProgressAndCompletedHandlers(task);

            Info += "Registered" + Environment.NewLine;
        }

        private void UnRegisterCommandExecute()
        {
            BackgroundTaskHelper.UnregisterBackgroundTask(typeof(BackgroundTask));

            Info += "UnRegistered" + Environment.NewLine;
        }

        private async Task SignalCommandExecute()
        {
            var settings = ApplicationData.Current.LocalSettings;
            settings.Values.Remove(typeof(BackgroundTask).Name);

            //Signal the ApplicationTrigger
            await _trigger.RequestAsync();

            Info += "Signal" + Environment.NewLine;
        }

        private void AttachProgressAndCompletedHandlers(IBackgroundTaskRegistration task)
        {
            try
            {
                task.Progress += OnProgress;
                task.Completed += OnCompleted;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void OnProgress(IBackgroundTaskRegistration task, BackgroundTaskProgressEventArgs args)
        {
            var progress = "Progress from Foreground: " + args.Progress + "%";
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                            () =>
                            {
                                Info += progress + Environment.NewLine;
                                ToastHelper.ShowToast(progress);
                            });
        }

        private async void OnCompleted(IBackgroundTaskRegistration task, BackgroundTaskCompletedEventArgs args)
        {
            ToastHelper.ShowToast("Completed");
            await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                () =>
                {
                    Info += "Completed" + Environment.NewLine;
                });
        }
    }
}
