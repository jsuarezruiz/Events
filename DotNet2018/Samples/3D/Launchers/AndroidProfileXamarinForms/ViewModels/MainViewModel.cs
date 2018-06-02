using System.Windows.Input;
using XamarinForms3DCarSample.Helpers;
using Xamarin.Forms;
using XamarinForms3DCarSample.ViewModels.Base;
using System;
using XamarinForms3DCarSampleXamarinForms.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace XamarinForms3DCarSample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isCamera1;
        private bool _isCamera2;
        private bool _isCamera3;
        private bool _isInit;
        private bool _isEnabled;
        private ObservableCollection<CustomColor> _colors;

        public MainViewModel()
        {
            IsCamera1 = true;
            IsCamera2 = false;
            IsCamera3 = false;
            IsInit = false;
            IsEnabled = true;
            Colors = new ObservableCollection<CustomColor>();
        }

        public bool IsCamera1
        {
            get { return _isCamera1; }
            set
            {
                _isCamera1 = value;
                RaisePropertyChanged(() => IsCamera1);
            }
        }

        public bool IsCamera2
        {
            get { return _isCamera2; }
            set
            {
                _isCamera2 = value;
                RaisePropertyChanged(() => IsCamera2);
            }
        }

        public bool IsCamera3
        {
            get { return _isCamera3; }
            set
            {
                _isCamera3 = value;
                RaisePropertyChanged(() => IsCamera3);
            }
        }
        public bool IsInit
        {
            get { return _isInit; }
            set
            {
                _isInit = value;
                RaisePropertyChanged(() => IsInit);
            }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        public ObservableCollection<CustomColor> Colors
        {
            get { return _colors; }
            set
            {
                _colors = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        public ICommand ChangeCameraCommand => new Command<string>(ChangeCamera);

        public ICommand OpenMenuCommand => new Command(OpenMenu);

        public ICommand CloseMenuCommand => new Command(CloseMenu);

        public ICommand ColorTappedCommand => new Command<CustomColor>(ChooseColor);

        public override Task InitializeAsync(object navigationData)
        {
            WaveEngineFacade.Initialized += OnInitialized;
            WaveEngineFacade.AnimationCompleted += OnAnimationCompleted;

            return Task.FromResult(true);
        }

        private void ChangeCamera(string camera)
        {
            IsEnabled = false;

            var cameraType = Enum.Parse(typeof(CameraType), camera);

            IsCamera1 = false;
            IsCamera2 = false;
            IsCamera3 = false;

            switch ((CameraType)cameraType)
            {
                case CameraType.Camera1:
                    IsCamera1 = true;
                    break;
                case CameraType.Camera2:
                    IsCamera2 = true;
                    break;
                case CameraType.Camera3:
                    IsCamera3 = true;
                    break;
            }

            WaveEngineFacade.SetActiveCamera((int)cameraType);
        }

        private void OpenMenu()
        {
            MessagingCenter.Send(this, MessengerKeys.OpenColors);
        }

        private void CloseMenu()
        {
            MessagingCenter.Send(this, MessengerKeys.CloseColors);
        }

        private void OnInitialized(object sender, EventArgs e)
        {
            IsInit = true;

            foreach (var color in LoadColors())
            {
                Colors.Add(color);
            }
        }

        private List<CustomColor> LoadColors()
        {
            return new List<CustomColor>
            {
                new CustomColor { Hex = "#00ff00" },
                new CustomColor { Hex = "#ff0000" },
                new CustomColor { Hex = "#0000ff" },
                new CustomColor { Hex = "#f4a460" },
                new CustomColor { Hex = "#ffb6c1" },
                new CustomColor { Hex = "#ffff00" },
                new CustomColor { Hex = "#ffa500" },
                new CustomColor { Hex = "#cccccc" },
                new CustomColor { Hex = "#ffffff" },
                new CustomColor { Hex = "#000000" },
                new CustomColor { Hex = "#800080" },
                new CustomColor { Hex = "#000080" },
                new CustomColor { Hex = "#ff00ff" },
                new CustomColor { Hex = "#00ffff" }
            };
        }

        private void ChooseColor(CustomColor color)
        {
            MessagingCenter.Send(this, MessengerKeys.CloseColors);
            WaveEngineFacade.UpdateColor(color);
        }

        private void OnAnimationCompleted(object sender, EventArgs e)
        {
            IsEnabled = true;
        }
    }
}
