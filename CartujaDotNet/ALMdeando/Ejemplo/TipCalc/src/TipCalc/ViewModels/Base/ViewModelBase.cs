using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TipCalc.ViewModels.Base
{
    public class ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
