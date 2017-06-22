using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamagram.Models;
using Xamagram.Services;
using Xamagram.ViewModels.Base;
using Xamarin.Forms;

namespace Xamagram.ViewModels
{
    public class XamagramItemsViewModel : ViewModelBase
    {
        private ObservableCollection<XamagramItem> _items;
        private XamagramItem _selectedItem;
        private bool _isBusy;

        public ObservableCollection<XamagramItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged("Items");
            }
        }

        public XamagramItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;

                // Creando un servicio de navegación
                NavigationService.Instance.NavigateTo<XamagramItemDetailViewModel>(_selectedItem);
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                if (_isBusy == value)
                    return;

                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        public ICommand RefreshCommand => new Command(async () => await RefreshAsync());
        public ICommand NewCommand => new Command(New);

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);
            await LoadXamagramItemsAsync();
        }

        private async Task RefreshAsync()
        {
            await LoadXamagramItemsAsync();
        }

        private void New()
        {
            NavigationService.Instance.NavigateTo<NewXamagramItemViewModel>();
        }

        private async Task LoadXamagramItemsAsync()
        {
            IsBusy = true;

            var result = await XamagramMobileService.Instance.ReadXamagramItemsAsync();

            if (result != null)
            {
                Items = new ObservableCollection<XamagramItem>(result);
            }

            IsBusy = false;
        }
    }
}