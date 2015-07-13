namespace FormulaOne.ViewModels
{
    using System.Threading.Tasks;
    using Base;
    using Services.Standings;
    using Windows.UI.Xaml.Navigation;

    public class MainViewModel : ViewModelBase
	{
		//Services
		private IStandingService _standingService;

		public MainViewModel(IStandingService standingService)
		{
			_standingService = standingService;
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
