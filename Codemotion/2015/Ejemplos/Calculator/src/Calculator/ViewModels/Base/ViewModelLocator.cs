namespace Calculator.ViewModels.Base
{
    using Microsoft.Practices.Unity;
    using ViewModels;

    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // ViewModels
            _container.RegisterType<CalculatorViewModel>();
        }

        public CalculatorViewModel CalculatorViewModel => _container.Resolve<CalculatorViewModel>();
    }
}
