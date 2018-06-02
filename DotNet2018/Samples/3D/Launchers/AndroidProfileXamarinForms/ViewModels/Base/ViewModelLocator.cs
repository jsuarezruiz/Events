using Autofac;
using XamarinForms3DCarSample.Services.Navigation;

namespace XamarinForms3DCarSample.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static IContainer _container;

        private static readonly ViewModelLocator _instance = new ViewModelLocator();

        public static ViewModelLocator Instance
        {
            get { return _instance; }
        }

        protected ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            // Services    
            builder.RegisterType<NavigationService>().As<INavigationService>();

            // View models
            builder.RegisterType<MainViewModel>();

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = builder.Build();
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(System.Type type)
        {
            return _container.Resolve(type);
        }
    }
}