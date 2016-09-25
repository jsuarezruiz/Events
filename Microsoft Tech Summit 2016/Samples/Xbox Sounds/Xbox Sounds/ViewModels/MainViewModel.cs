using System.Windows.Input;
using Xbox_Sounds.Services.Sounds;
using Xbox_Sounds.ViewModels.Base;

namespace Xbox_Sounds.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Commands
        private ICommand _focusCommand;
        private ICommand _goBackCommand;
        private ICommand _hideCommand;
        private ICommand _invokeCommand;
        private ICommand _moveNextCommand;
        private ICommand _movePreviousCommand;
        private ICommand _showCommand;

        // Services
        private ISoundService _soundService;

        public MainViewModel(ISoundService soundService)
        {
            _soundService = soundService;
        }

        public ICommand FocusCommand
        {
            get { return _focusCommand = _focusCommand ?? new DelegateCommand(FocusCommandExecute); }
        }

        public ICommand GoBackCommand
        {
            get { return _goBackCommand = _goBackCommand ?? new DelegateCommand(GoBackCommandExecute); }
        }

        public ICommand HideCommand
        {
            get { return _hideCommand = _hideCommand ?? new DelegateCommand(HideCommandExecute); }
        }

        public ICommand InvokeCommand
        {
            get { return _invokeCommand = _invokeCommand ?? new DelegateCommand(InvokeCommandExecute); }
        }

        public ICommand MoveNextCommand
        {
            get { return _moveNextCommand = _moveNextCommand ?? new DelegateCommand(MoveNextCommandExecute); }
        }

        public ICommand MovePreviousCommand
        {
            get { return _movePreviousCommand = _movePreviousCommand ?? new DelegateCommand(MovePreviousCommandExecute); }
        }

        public ICommand ShowCommand
        {
            get { return _showCommand = _showCommand ?? new DelegateCommand(ShowCommandExecute); }
        }

        private void FocusCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.Focus);
        }

        private void GoBackCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.GoBack);
        }

        private void HideCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.Hide);
        }

        private void InvokeCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.Invoke);
        }

        private void MoveNextCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.MoveNext);
        }

        private void MovePreviousCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.MovePrevious);
        }

        private void ShowCommandExecute()
        {
            _soundService.Play(SoundService.SoundKind.Show);
        }
    }
}
