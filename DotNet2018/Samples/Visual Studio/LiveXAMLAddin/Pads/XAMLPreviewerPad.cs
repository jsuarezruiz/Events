using LiveXAMLAddin.Views;
using MonoDevelop.Components;
using MonoDevelop.Ide.Gui;
using Xamarin.Forms.Platform.GTK.Extensions;

namespace LiveXAMLAddin.Pads
{
    public class XAMLPreviewerPad : PadContent
    {
        Gtk.VBox _content;

        public override Control Control { get { return _content; } }

        protected override void Initialize(IPadWindow window)
        {
            base.Initialize(window);

            _content = new Gtk.VBox();

            var page = new PreviewerView();

            var pageContainer = page.CreateContainer();
            pageContainer.SetSizeRequest(800, 480);

            _content.PackStart(pageContainer, true, true, 0);

            _content.ShowAll();
        }

    }
}