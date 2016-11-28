namespace MicrosoftBand.ViewModels.Base
{
    using Microsoft.Practices.Unity;
    using Services.Navigation;
    using ViewModels;
    using Xamarin.Forms;

    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // ViewModels
            _container.RegisterType<AddTileViewModel>();
            _container.RegisterType<ConnectionViewModel>();
            _container.RegisterType<CustomizationViewModel>();
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<NotificationsViewModel>();
            _container.RegisterType<SensorsViewModel>();
            _container.RegisterType<TilesViewModel>();
            _container.RegisterType<VibrationsViewModel>();

            // Services
            _container.RegisterType<INavigationService, NavigationService>();
        }

        public AddTileViewModel AddTileViewModel
        {
            get { return _container.Resolve<AddTileViewModel>(); }
        }

        public ConnectionViewModel ConnectionViewModel
        {
            get { return _container.Resolve<ConnectionViewModel>(); }
        }

        public CustomizationViewModel CustomizationViewModel
        {
            get { return _container.Resolve<CustomizationViewModel>(); }
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }

        public NotificationsViewModel NotificationsViewModel
        {
            get { return _container.Resolve<NotificationsViewModel>(); }
        }

        public SensorsViewModel SensorsViewModel
        {
            get { return _container.Resolve<SensorsViewModel>(); }
        }

        public TilesViewModel TilesViewModel
        {
            get { return _container.Resolve<TilesViewModel>(); }
        }

        public VibrationsViewModel VibrationsViewModel
        {
            get { return _container.Resolve<VibrationsViewModel>(); }
        }
    }
}
