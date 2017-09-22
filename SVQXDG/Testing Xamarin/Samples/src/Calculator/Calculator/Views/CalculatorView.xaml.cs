using Xamarin.Forms;

namespace Calculator.Views
{
    public partial class CalculatorView : ContentPage
    {
        public CalculatorView()
        {
            InitializeComponent();

            BindingContext = App.Locator.CalculatorViewModel;
        }
    }
}
