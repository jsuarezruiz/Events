using Microsoft.Practices.Unity;
using Orbit.Services.Planets;

namespace Orbit.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<PlanetsViewModel>();
            _container.RegisterType<PlanetDetailViewModel>();

            _container.RegisterType<IPlanetsService, PlanetsService>(new ContainerControlledLifetimeManager());
        }

        public PlanetsViewModel PlanetsViewModel
        {
            get { return _container.Resolve<PlanetsViewModel>(); }
        }

        public PlanetDetailViewModel PlanetDetailViewModel
        {
            get { return _container.Resolve<PlanetDetailViewModel>(); }
        }
    }
}