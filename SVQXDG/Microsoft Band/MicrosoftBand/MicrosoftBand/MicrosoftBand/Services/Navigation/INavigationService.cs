namespace MicrosoftBand.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TDestinationViewModel>(object navigationContext = null);

        void NavigateBack();

        void NavigateBackToFirst();
    }
}