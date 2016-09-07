using GeoPic.Models;
using GeoPic.Services.Dialog;
using GeoPic.Services.Geolocator;
using GeoPic.Services.GeoPic;
using GeoPic.Services.Internet;
using GeoPic.Services.MediaPicker;
using GeoPic.Services.Navigation;
using GeoPic.ViewModels.Base;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GeoPic.ViewModels
{
    public class GeoPicDetailViewModel : ViewModelBase
    {
        // Consts
        private const string DefaultUrl = "https://javiersuarezruiz.files.wordpress.com/2015/03/xamarin.png";

        // Variables
        private bool _isNew;
        private PhotoCommand _photo;
        private string _title;
        private double _latitude;
        private double _longitude;

        // Services
        private IInternetService _internetService;
        private IGeoPicService _geoPicService;
        private IDialogService _dialogService;
        private INavigationService _navigationService;
        private IMediaPicker _mediaPicker;
        private IGeolocator _geoLocator;

        // Commands
        private ICommand _photoCommand;
        private ICommand _addCommand;
        private ICommand _cancelCommand;

        public GeoPicDetailViewModel(
            IInternetService internetService,
            IGeoPicService geoPicService,
            IDialogService dialogService,
            INavigationService navigationService,
            IMediaPicker mediaPicker,
            IGeolocator geoLocator)
        {
            _internetService = internetService;
            _geoPicService = geoPicService;
            _dialogService = dialogService;
            _navigationService = navigationService;
            _mediaPicker = mediaPicker;
            _geoLocator = geoLocator;
        }

        public bool IsNew
        {
            get { return _isNew; }
            set
            {
                _isNew = value;
                RaisePropertyChanged();
            }
        }

        public PhotoCommand Photo
        {
            get { return _photo; }
            set
            {
                _photo = value;
                RaisePropertyChanged();
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged();
            }
        }

        public double Latitude
        {
            get { return _latitude; }
            set
            {
                _latitude = value;
                RaisePropertyChanged();
            }
        }

        public double Longitude
        {
            get { return _longitude; }
            set
            {
                _longitude = value;
                RaisePropertyChanged();
            }
        }

        public ICommand PhotoCommand
        {
            get { return _photoCommand = _photoCommand ?? new DelegateCommand(PhotoCommandExecute); }
        }

        public ICommand AddCommand
        {
            get { return _addCommand = _addCommand ?? new DelegateCommand(AddCommandExecute); }
        }
        
        public ICommand CancelCommand
        {
            get { return _cancelCommand = _cancelCommand ?? new DelegateCommand(CancelCommandExecute); }
        }

        public override async void OnAppearing(object navigationContext)
        {
            base.OnAppearing(navigationContext);

            Photo = navigationContext as PhotoCommand;
            IsNew = Photo == null;
            Title = Photo != null ? string.Format("{0}, {1}", Photo.Latitude, Photo.Longitude) : "Nuevo";

            if (_geoLocator.IsListening)
                return;

            _geoLocator.StartListening(2000, 0, true);

            await GetPosition();
        }

        async Task GetPosition()
        {
#if DEBUG
            Latitude = 36.49;
            Longitude = 5.59;
#else
            if (!_geoLocator.IsGeolocationAvailable)
                return;

            var cancelSource = new CancellationTokenSource();
            var result = await _geoLocator.GetPositionAsync(
                timeout: 1000, cancelToken: cancelSource.Token, includeHeading: false);

            Latitude =  result.Latitude;
            Longitude = result.Longitude;
#endif
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();

            _geoLocator.StopListening();
        }

        private async void PhotoCommandExecute()
        {
            try
            {
                var mediaFile = await _mediaPicker.SelectPhotoAsync(new CameraMediaStorageOptions
                {
                    DefaultCamera = CameraDevice.Front,
                    MaxPixelDimension = 400
                });

                var result = ImageSource.FromStream(() => mediaFile.Source);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                await _dialogService.ShowMessage("Oops!", "No se ha podido obtener una foto utilizando la cámara.");
            }
        }

        private async void AddCommandExecute()
        {
            if (_internetService.HasConnection())
            {
                var newPhoto = new PhotoUpload
                {
                    Picture = DefaultUrl,
                    Location = string.Format("{0},{1}", Latitude, Longitude)
                };

                var added = await _geoPicService.UploadPhotoAsync(newPhoto);

                if (added)
                    _navigationService.NavigateBack();
                else
                    await _dialogService.ShowMessage("Error", 
                        "No se ha podido añadir la foto, por favor, inténtelo de nuevo más tarde.");
            }
            else
                await _dialogService.ShowMessage("Cuidado", "No hay conexión a Internet.");
        }

        private void CancelCommandExecute()
        {
            _navigationService.NavigateBack();
        }
    }
}
