namespace MicrosoftBand.ViewModels.Base
{
    using Microsoft.Band.Portable;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private BandDeviceInfo _bandDeviceInfo;
        private BandClient _bandClient;

        public BandDeviceInfo BandInfo
        {
            get { return _bandDeviceInfo; }
            set
            {
                _bandDeviceInfo = value;
                RaisePropertyChanged();
            }
        }

        public BandClient BandClient
        {
            get { return _bandClient; }
            set
            {
                _bandClient = value;
                RaisePropertyChanged();
            }
        }

        public virtual void OnAppearing(object navigationContext)
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
