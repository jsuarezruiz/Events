using LiveXAMLAddin.ViewModels;
using Xamarin.Forms;

namespace LiveXAMLAddin.Views
{
    public partial class EditorView : ContentPage
    {
        public EditorView()
        {
            InitializeComponent();

            BindingContext = new EditorViewModel();
        }
    }
}