namespace RecetarioUniversal.Services.Navigation
{
    using System;
    using System.Threading.Tasks;
    using Windows.System;

    public class NavigationService : INavigationService
    {
        public void NavigateBack()
        {
            App.Frame.GoBack();
        }

        public void NavigateTo<T>(object parameter = null)
        {
            if (parameter != null)
                App.Frame.Navigate(typeof(T), parameter);
            else
                App.Frame.Navigate(typeof(T));
        }

        public void ClearNavigationHistory()
        {
            App.Frame.BackStack.Clear();
        }
    }
}
