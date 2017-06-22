using System.Threading.Tasks;
using System.Windows.Input;
using Xamagram.Models;
using Xamagram.Services;
using Xamagram.ViewModels.Base;
using Xamarin.Forms;

namespace Xamagram.ViewModels
{
    public class XamagramItemDetailViewModel : ViewModelBase
    {
        private XamagramItem _item;

        public XamagramItem Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged("Item");
            }
        }

        public ICommand EditCommand => new Command(Edit);
        public ICommand DeleteCommand => new Command(async () => await DeleteAsync());

        public override void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            if (navigationContext is XamagramItem)
            {
                Item = (XamagramItem)navigationContext;
            }
        }

        private void Edit()
        {
            NavigationService.Instance.NavigateTo<NewXamagramItemViewModel>(Item);
        }

        private async Task DeleteAsync()
        {
            if (Item.Id != null)
            {
                await XamagramMobileService.Instance.DeleteXamagramItemAsync(Item);

                NavigationService.Instance.NavigateBack();
            }
        }
    }
}