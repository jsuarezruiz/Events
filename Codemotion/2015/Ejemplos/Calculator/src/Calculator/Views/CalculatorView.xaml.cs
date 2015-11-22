namespace Calculator.Views
{
    using Xamarin.Forms;

    public partial class CalculatorView : ContentPage
    {
        public CalculatorView()
        {
            InitializeComponent();

            BindingContext = App.Locator.CalculatorViewModel;
        }
    }
}
