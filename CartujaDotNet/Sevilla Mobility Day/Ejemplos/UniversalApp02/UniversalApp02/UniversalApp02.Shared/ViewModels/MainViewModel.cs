using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UniversalApp02.Models;
using UniversalApp02.Services.Standings;
using UniversalApp02.ViewModels.Base;

namespace UniversalApp02.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Services
        private IStandingService _standingService;

        //Variables
        private ObservableCollection<DriverStanding> _driverStanding;
        private ObservableCollection<ConstructorStanding> _constructorStanding;

        //Commands
        private ICommand _refreshCommand;

        //Constructor
        public MainViewModel(IStandingService standingService)
        {
            _standingService = standingService;

            _driverStanding = new ObservableCollection<DriverStanding>();
            _constructorStanding = new ObservableCollection<ConstructorStanding>();
        }

        public ObservableCollection<DriverStanding> DriverStanding
        {
            get { return _driverStanding; }
            set { _driverStanding = value; }
        }

        public ObservableCollection<ConstructorStanding> ConstructorStanding
        {
            get { return _constructorStanding; }
            set { _constructorStanding = value; }
        }

        public ICommand RefreshCommand
        {
            get { return _refreshCommand = _refreshCommand ?? new DelegateCommandAsync(RefreshCommandDelegate); }
        }

        public override System.Threading.Tasks.Task OnNavigatedFrom(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            return null;
        }

        public override async Task OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs args)
        {
            await LoadStandingsData();
        }

        private async Task LoadStandingsData()
        {
            var driverStandings = await _standingService.GetSeasonDriverStandingsCollectionAsync();
            var drivers = driverStandings.StandingsLists.First().DriverStandings;

            foreach (var driver in drivers)
            {
                DriverStanding.Add(driver);
            }

            var constructorStandings = await _standingService.GetSeasonConstructorStandingsCollectionAsync();
            var constructors = constructorStandings.StandingsLists.First().ConstructorStandings;

            foreach (var constructor in constructors)
            {
                ConstructorStanding.Add(constructor);
            }
        }

        public async Task RefreshCommandDelegate()
        {
            await LoadStandingsData();
        }
    }
}
