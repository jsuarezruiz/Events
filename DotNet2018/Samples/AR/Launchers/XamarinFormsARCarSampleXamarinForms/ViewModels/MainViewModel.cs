using System.Windows.Input;
using XamarinFormsARCarSample.Helpers;
using Xamarin.Forms;
using XamarinFormsARCarSample.ViewModels.Base;
using System;
using XamarinFormsARCarSampleXamarinForms.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace XamarinFormsARCarSample.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isInit;
        private bool _isEnabled;
        private ObservableCollection<CustomColor> _colors;

        public MainViewModel()
        {
            IsInit = false;
            IsEnabled = true;
            Colors = new ObservableCollection<CustomColor>();
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

        public ICommand OpenMenuCommand => new Command(OpenMenu);

        public ICommand CloseMenuCommand => new Command(CloseMenu);

        public ICommand ColorTappedCommand => new Command<CustomColor>(ChooseColor);

        public override Task InitializeAsync(object navigationData)
        {
            WaveEngineFacade.Initialized += OnInitialized;

            return base.InitializeAsync(navigationData);
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