using DevDaysWorkshop.Models;
using DevDaysWorkshop.ViewModels.Base;
using Xamarin.Forms;

namespace DevDaysWorkshop.ViewModels
{
    public class SpeakerDetailViewModel : ViewModelBase
    {
        private Speaker _item;

        public Speaker Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            if (navigationContext is Speaker)
            {
                Item = (Speaker)navigationContext;
            }
        }
    }
}
