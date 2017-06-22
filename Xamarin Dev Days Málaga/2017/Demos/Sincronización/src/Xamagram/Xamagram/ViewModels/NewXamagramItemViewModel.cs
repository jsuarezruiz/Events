using System.Threading.Tasks;
using System.Windows.Input;
using Xamagram.Models;
using Xamagram.Services;
using Xamagram.ViewModels.Base;
using Xamarin.Forms;

namespace Xamagram.ViewModels
{
    public class NewXamagramItemViewModel : ViewModelBase
    {
        private string _id;
        private string _imageUrl;
        private string _name;
        private string _description;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
                OnPropertyChanged("ImageUrl");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }

        public ICommand CameraCommand => new Command(async () => await CameraAsync());

        public ICommand SaveCommand => new Command(async () => await SaveAsync());

        public ICommand CancelCommand => new Command(Cancel);

        public override void OnAppearing(object navigationContext)
        {
            if(navigationContext is XamagramItem)
            {
                var xamagramItem = (XamagramItem)navigationContext;

                Id = xamagramItem.Id;
                ImageUrl = xamagramItem.Image;
                Name = xamagramItem.Name;
                Description = xamagramItem.Description;
            }

            base.OnAppearing(navigationContext);
        }

        private async Task CameraAsync()
        {
            var result = await PhotoService.Instance.TakePhotoAsync();

            if(!string.IsNullOrEmpty(result))
            {
                ImageUrl = result;
            }
        }

        private async Task SaveAsync()
        {
            var xamagramItem = new XamagramItem
            {
                Id = Id,
                Name = Name,
                Image = ImageUrl,
                Description = Description 
            };

            await XamagramMobileService.Instance.
                AddOrUpdateXamagramItemAsync(xamagramItem);

            NavigationService.Instance.NavigateBack();
        }

        private void Cancel()
        {
            NavigationService.Instance.NavigateBack();
        }
    }
}
