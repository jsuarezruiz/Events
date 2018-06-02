using Autofac;
using TemperatureMonitor.Services.AzureStorage;
using TemperatureMonitor.Services.RestApi;
using TemperatureMonitor.Services.Temperature;

namespace TemperatureMonitor.ViewModels.Base
{
    public class ViewModelLocator
    {
        private static IContainer _container;

        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TemperatureMonitorViewModel>();

            builder.RegisterType<RestApiService>().As<IRestApiService>();
            builder.RegisterType<TableStorageService>().As<ITableStorageService>();
            builder.RegisterType<FakeTemperatureSensorService>().As<ITemperatureSensorService>();

            if (_container != null)
            {
                _container.Dispose();
            }

            _container = builder.Build();
        }

        public TemperatureMonitorViewModel TemperatureMonitorViewModel
        {
            get { return _container.Resolve<TemperatureMonitorViewModel>(); }
        }
    }
}