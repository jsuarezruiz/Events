using Xamarin.Forms;

namespace Movies.ViewModels.Base
{
    public class ViewModelBase : BindableObject
    {
        public virtual void OnAppearing(object navigationContext)
        {
        }
    }
}