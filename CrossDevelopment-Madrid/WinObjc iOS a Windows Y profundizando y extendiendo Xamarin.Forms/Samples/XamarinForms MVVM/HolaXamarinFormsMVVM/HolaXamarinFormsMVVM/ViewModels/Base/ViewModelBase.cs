using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HolaXamarinFormsMVVM.ViewModels.Base
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handle = PropertyChanged;
            if (handle != null)
                handle(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
