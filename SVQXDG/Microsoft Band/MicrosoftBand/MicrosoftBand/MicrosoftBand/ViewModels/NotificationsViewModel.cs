namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable.Notifications;
    using Microsoft.Band.Portable.Tiles;
    using Models;
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class NotificationsViewModel : ViewModelBase
    {
        // Variables
        private BandTile _tile;
        private BandNotificationManager _notifiactionManager;
        private string _title;
        private string _body;

        // Commands
        private ICommand _sendMessageCommand;
        private ICommand _sendMessageDialogCommand;
        private ICommand _showDialogCommand;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public string Body
        {
            get { return _body; }
            set
            {
                _body = value;
                RaisePropertyChanged();
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            var notificationData = navigationContext as NotificationData;
            BandClient = notificationData.BandClient;
            _tile = notificationData.Tile;
            _notifiactionManager = BandClient.NotificationManager;

            base.OnAppearing(navigationContext);
        }

        public ICommand SendMessageCommand
        {
            get { return _sendMessageCommand = _sendMessageCommand ?? new DelegateCommandAsync(SendMessageCommandExecute); }
        }

        public ICommand SendMessageDialogCommand
        {
            get { return _sendMessageDialogCommand = _sendMessageDialogCommand ?? new DelegateCommandAsync(SendMessageDialogCommandExecute); }
        }

        public ICommand ShowDialogCommand
        {
            get { return _showDialogCommand = _showDialogCommand ?? new DelegateCommandAsync(ShowDialogCommandExecute); }
        }

        private async Task SendMessageCommandExecute()
        {
            await _notifiactionManager.SendMessageAsync(_tile.Id, Title, Body, DateTime.Now);
        }

        private async Task SendMessageDialogCommandExecute()
        {
            await _notifiactionManager.SendMessageAsync(_tile.Id, Title, Body, DateTime.Now, true);
        }

        private async Task ShowDialogCommandExecute()
        {
            await _notifiactionManager.ShowDialogAsync(_tile.Id, Title, Body);
        }
    }
}