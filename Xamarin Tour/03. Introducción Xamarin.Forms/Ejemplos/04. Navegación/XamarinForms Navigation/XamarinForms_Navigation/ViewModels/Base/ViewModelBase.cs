using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XamarinForms_Navigation.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var Handler = PropertyChanged;
            if (Handler != null)
                Handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
