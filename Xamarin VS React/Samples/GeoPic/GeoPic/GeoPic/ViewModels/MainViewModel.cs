using GeoPic.Models;
using GeoPic.Services.Dialog;
using GeoPic.Services.GeoPic;
using GeoPic.Services.Internet;
using GeoPic.Services.Navigation;
using GeoPic.ViewModels.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GeoPic.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        // Variables
        private ObservableCollection<PhotoCommand> _photos;
        private PhotoCommand _selectedPhoto;
        
        // Services
        private IInternetService _internetService;
        private IGeoPicService _geoPicService;
        private IDialogService _dialogService;
        private INavigationService _navigationService;

        // Commands
        private ICommand _geoPicDetailCommand;
        private ICommand _addCommand;

        public MainViewModel(IInternetService internetService,
            IGeoPicService geoPicService,
            IDialogService dialogService,
            INavigationService navigationService)
        {
            _internetService = internetService;
            _geoPicService = geoPicService;
            _dialogService = dialogService;
            _navigationService = navigationService;
        }

        public ObservableCollection<PhotoCommand> Photos
        {
            get { return _photos; }
            set
            {
                _photos = value;
                RaisePropertyChanged();
            }
        }

        public PhotoCommand SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                _selectedPhoto = value;
                
                if(_selectedPhoto != null)
                {
                    _selectedPhoto.Command.Execute(_selectedPhoto);
                }
            }
        }

        public ICommand GeoPicDetailCommand
        {
            get { return _geoPicDetailCommand = _geoPicDetailCommand ?? new DelegateCommand<PhotoCommand>(GeoPicDetailCommandExecute); }
        }

        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new DelegateCommand(AddCommandExecute); }
        }

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            var hasConnection = _internetService.HasConnection();

            if (hasConnection)
            {
                var result = await _geoPicService.GetPhotosAsync();

                if (result != null)
                {
                    var temp = new List<PhotoCommand>();

                    foreach (var photo in result)
                    {
                        temp.Add(new PhotoCommand
                        {
                            Id = photo.Id,
                            Latitude = photo.Latitude,
                            Longitude = photo.Longitude,
                            Url = photo.Url,
                            Command = GeoPicDetailCommand
                        });
                    }

                    Photos = new ObservableCollection<PhotoCommand>(temp);
                }
            }
            else
                await _dialogService.ShowMessage("Cuidado", "No hay conexión a Internet.");
        }

        private void GeoPicDetailCommandExecute(PhotoCommand photo)
        {
            _navigationService.NavigateTo<GeoPicDetailViewModel>(photo);
        }

        private void AddCommandExecute()
        {
            _navigationService.NavigateTo<GeoPicDetailViewModel>();
        }
    }
}
