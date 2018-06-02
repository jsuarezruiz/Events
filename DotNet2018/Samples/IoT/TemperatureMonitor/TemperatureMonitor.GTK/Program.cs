using TemperatureMonitor.GTK.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.GTK;

namespace TemperatureMonitor.GTK
{
    class Program
    {
        static void Main(string[] args)
        {
            Gtk.Application.Init();
            Forms.Init();
            PlotViewRenderer.Init();
            var app = new App();
            var window = new FormsWindow();
            window.LoadApplication(app);
            window.SetApplicationTitle("Temperature Monitor");
            window.Show();
            Gtk.Application.Run();
        }
    }
}