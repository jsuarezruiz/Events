using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Orbit.ViewModels.Base
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Frame _appFrame;

        public Frame AppFrame
        {
            get { return _appFrame; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public abstract Task OnNavigatedFrom(NavigationEventArgs args);

        public abstract Task OnNavigatedTo(NavigationEventArgs args);

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var Handler = PropertyChanged;
            if (Handler != null)
                Handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetAppFrame(Frame viewFrame)
        {
            _appFrame = viewFrame;
        }
    }
}