using Microsoft.Practices.Unity;
using GeoPic.Services.Navigation;
using GeoPic.Services.GeoPic;
using GeoPic.Services.Internet;
using Xamarin.Forms;
using GeoPic.Services.Dialog;
using GeoPic.Services.MediaPicker;
using GeoPic.Services.Geolocator;

namespace GeoPic.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // ViewModels
            _container.RegisterType<ExtendedSplashViewModel>();
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<GeoPicDetailViewModel>();

            // Services     
            _container.RegisterType<IGeoPicService, GeoPicService>();
            _container.RegisterType<INavigationService, NavigationService>();

            IInternetService internetService = DependencyService.Get<IInternetService>();
            _container.RegisterInstance(internetService);

            IDialogService dialogService = DependencyService.Get<IDialogService>();
            _container.RegisterInstance(dialogService);

            IMediaPicker mediaPicker = DependencyService.Get<IMediaPicker>();
            _container.RegisterInstance(mediaPicker);

            IGeolocator geoLocator = DependencyService.Get<IGeolocator>();
            _container.RegisterInstance(geoLocator);
        }

        public ExtendedSplashViewModel ExtendedSplashViewModel
        {
            get { return _container.Resolve<ExtendedSplashViewModel>(); }
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }

        public GeoPicDetailViewModel GeoPicDetailViewModel
        {
            get { return _container.Resolve<GeoPicDetailViewModel>(); }
        }
    }
}