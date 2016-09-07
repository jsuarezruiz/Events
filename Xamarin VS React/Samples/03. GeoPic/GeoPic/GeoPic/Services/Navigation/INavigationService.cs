using System;

namespace GeoPic.Services.Navigation
{
    public interface INavigationService
    {
        void NavigateTo<TDestinationViewModel>(object navigationContext = null);

        void NavigateTo(Type destinationType, object navigationContext = null);

        void NavigateBack();

        void RemoveFirstPageFromBackStack();

        void RemoveLastPageFromBackStack();
    }
}
