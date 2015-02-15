namespace dotnetspain2015.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Models;
    using Base;
    using Views;
    using Xamarin.Forms;

    public class MainViewModel : ViewModelBase
    {
        // Variables
        private ObservableCollection<Sample> _samples;

        // Services
        private readonly INavigation _navigationService;

        // Commands
        private ICommand _navigateToSampleCommand;

        public MainViewModel(INavigation navigationService)
        {
            _navigationService = navigationService;

            LoadSamples();
        }

        public ObservableCollection<Sample> Samples
        {
            get { return _samples; }
            set { _samples = value; }
        }

        private void LoadSamples()
        {
            _samples = new ObservableCollection<Sample>
            {
                new Sample {Id = 1, Name = "Service"},
                new Sample {Id = 2, Name = "MarkupExension"},
                new Sample {Id = 3, Name = "Custom Control Renderer"},
                new Sample {Id = 4, Name = "Extend Control"},
                new Sample {Id = 5, Name = "Custom Gesture Page"}
            };

            if (Device.OS == TargetPlatform.WinPhone)
                Samples.Add(new Sample {Id = 6, Name = "Native Control"});
        }

        public ICommand NavigateToSampleCommand
        {
            get
            {
                return
                    _navigateToSampleCommand =
                        _navigateToSampleCommand ?? new DelegateCommand<string>(NavigateToSampleCommandExecute);
            }
        }

        private async void NavigateToSampleCommandExecute(string parameter)
        {
            switch (parameter)
            {
                case "Service":
                    await _navigationService.PushAsync(new ServiceView());
                    break;
                case "MarkupExension":
                    await _navigationService.PushAsync(new MarkupExtensionView());
                    break;
                case "Extend Control":
                    await _navigationService.PushAsync(new ExtendControlView());
                    break;
                case "Custom Control Renderer":
                    await _navigationService.PushAsync(new CustomControlRendererView());
                    break;
                case "Custom Gesture Page":
                    await _navigationService.PushAsync(new SwipeGesturedContentView());
                    break;
                case "Native Control":
                    await _navigationService.PushAsync(new NativeControlView());
                    break;
            }
        }
    }
}
