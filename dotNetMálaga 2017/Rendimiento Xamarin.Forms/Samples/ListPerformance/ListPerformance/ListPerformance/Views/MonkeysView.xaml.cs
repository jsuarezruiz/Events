using ListPerformance.ViewModels;
using System.Diagnostics;
using Xamarin.Forms;

namespace ListPerformance.Views
{
    public partial class MonkeysView : ContentPage
    {
        public MonkeysView()
        {
            var watch = Stopwatch.StartNew();
            InitializeComponent();
            BindingContext = new MonkeysViewModel();
            watch.Stop();
            Debug.WriteLine("Initialize");
            Debug.WriteLine("Elapsed: " + watch.ElapsedMilliseconds + "ms");
        }
    }
}