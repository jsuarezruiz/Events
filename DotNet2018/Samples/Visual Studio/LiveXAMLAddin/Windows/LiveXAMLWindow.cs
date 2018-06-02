using LiveXAMLAddin.Views;
using Xamarin.Forms.Platform.GTK.Extensions;

namespace LiveXAMLAddin.Windows
{
    public class LiveXAMLWindow : Gtk.Window
    {
        public LiveXAMLWindow()
            : base(Gtk.WindowType.Toplevel)
        {
            Title = "Live XAML Editor";
            WindowPosition = Gtk.WindowPosition.Center;

            var page = new EditorView();

            Add(page.CreateContainer());

            SetSizeRequest(800, 480);
        }
    }
}