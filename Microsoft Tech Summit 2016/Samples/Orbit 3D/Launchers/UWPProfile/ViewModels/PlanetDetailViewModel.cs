using Orbit.Models;
using Orbit.ViewModels.Base;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;

namespace Orbit.ViewModels
{
    public class PlanetDetailViewModel : ViewModelBase
    {
        private Planet _planet;

        public Planet Planet
        {
            get { return _planet; }
            set
            {
                _planet = value;
                RaisePropertyChanged();
            }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            return null;
        }
    }
}
