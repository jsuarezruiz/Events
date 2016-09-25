using Microsoft.Practices.Unity;

namespace Xbox_Gamepad.ViewModels.Base
{
    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            _container.RegisterType<MainViewModel>();
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }
    }
}