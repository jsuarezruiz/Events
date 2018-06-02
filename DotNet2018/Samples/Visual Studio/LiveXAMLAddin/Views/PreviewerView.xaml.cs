using LiveXAMLAddin.ViewModels;
using Xamarin.Forms;

namespace LiveXAMLAddin.Views
{
    public partial class PreviewerView : ContentPage
    {
        public PreviewerView()
        {
            InitializeComponent();
          
            BindingContext = new PreviewerViewModel();
        }
    }
}