using Microsoft.Practices.Unity;
using Xbox_Sounds.Services.Sounds;

namespace Xbox_Sounds.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();

            _container.RegisterType<ISoundService, SoundService>(new ContainerControlledLifetimeManager());
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }
    }
}