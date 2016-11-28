namespace RecetarioUniversal.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateBack();

        void NavigateTo<T>(object parameter = null);

        void ClearNavigationHistory();
    }
}
