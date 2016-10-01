using System.Threading.Tasks;
using Orbit.Services.Planets;
using Orbit.ViewModels.Base;
using Windows.UI.Xaml.Navigation;
using System.Collections.ObjectModel;
using Orbit.Models;
using System.Linq;
using System.Windows.Input;
using Orbit.Views;

namespace Orbit.ViewModels
{
    public class PlanetsViewModel : ViewModelBase
    {
        private ObservableCollection<Planet> _planets;
        private Planet _hero;

        private ICommand _navigateCommand;

        private IPlanetsService _planetsService;

        public PlanetsViewModel(IPlanetsService planetsService)
        {
            _planetsService = planetsService;
        }

        public ObservableCollection<Planet> Planets
        {
            get { return _planets; }
            set
            {
                _planets = value;
                RaisePropertyChanged();
            }
        }

        public Planet Hero
        {
            get { return _hero; }
            set
            {
                _hero = value;
                RaisePropertyChanged();
            }
        }

        public ICommand NavigateCommand
        {
            get { return _navigateCommand = _navigateCommand ?? new DelegateCommand<Planet>(NavigateCommandExecute); }
        }

        public override Task OnNavigatedFrom(NavigationEventArgs args)
        {
            return null;
        }

        public override Task OnNavigatedTo(NavigationEventArgs args)
        {
            Planets = _planetsService.GetPlanets();

            Hero = Planets.First();

            Planets.RemoveAt(0);

            return null;
        }

        private void NavigateCommandExecute(Planet planet)
        {
            AppFrame.Navigate(typeof(PlanetDetailView), planet);
        }
    }
}