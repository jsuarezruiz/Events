using System.Diagnostics;
using TipCalc.ViewModel;
using Xamarin.Forms;

namespace TipCalc.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            var watch = Stopwatch.StartNew();
            InitializeComponent();
            BindingContext = new MainViewModel();
            watch.Stop();
            Debug.WriteLine("Initialize");
            Debug.WriteLine("Elapsed: " + watch.ElapsedMilliseconds + "ms");
        }
    }
}