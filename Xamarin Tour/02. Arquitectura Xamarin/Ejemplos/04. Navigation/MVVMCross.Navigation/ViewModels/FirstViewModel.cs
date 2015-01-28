using System.Windows.Input;
using Cirrious.MvvmCross.ViewModels;

namespace MVVMCross.Navigation.ViewModels
{
    public class FirstViewModel
        : MvxViewModel
    {
        private MvxCommand _gotoSecondViewCommand;

        public ICommand GotoSecondViewCommand
        {
            get
            {
                _gotoSecondViewCommand = _gotoSecondViewCommand ?? new MvxCommand(DoGotoSecondViewCommand);
                return _gotoSecondViewCommand;
            }
        }

        private void DoGotoSecondViewCommand()
        {
            ShowViewModel<SecondViewModel>(new { parameter = "Test" });
        }
    }
}
