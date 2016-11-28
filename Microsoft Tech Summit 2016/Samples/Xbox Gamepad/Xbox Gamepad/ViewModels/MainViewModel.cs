using Windows.System;
using Xbox_Gamepad.ViewModels.Base;

namespace Xbox_Gamepad.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private string _key;

        public string Key
        {
            get { return _key; }
            set
            {
                _key = value;
                RaisePropertyChanged();
            }
        }
    }
}