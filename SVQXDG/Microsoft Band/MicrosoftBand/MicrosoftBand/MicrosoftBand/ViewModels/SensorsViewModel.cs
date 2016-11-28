namespace MicrosoftBand.ViewModels
{
    using Base;
    using Microsoft.Band.Portable;
    using Microsoft.Band.Portable.Sensors;

    public class SensorsViewModel : ViewModelBase
    {
        // Variables
        private long _totalDistance;
        private BandSensorManager _sensorManager;

        public long TotalDistance
        {
            get { return _totalDistance; }
            set
            {
                _totalDistance = value;
                RaisePropertyChanged();
            }
        }

        public override async void OnAppearing(object navigationContext)
        {
            // Init
            BandClient = navigationContext as BandClient;
            _sensorManager = BandClient.SensorManager;

            // Get Distance Data
            await _sensorManager.Distance.StartReadingsAsync(BandSensorSampleRate.Ms128);
            _sensorManager.Distance.ReadingChanged += (s, e) =>
            {
                TotalDistance = e.SensorReading.TotalDistance;
                var speed = e.SensorReading.Speed;
                var pace = e.SensorReading.Pace;
                var currentmotion = e.SensorReading.CurrentMotion;
            };

            base.OnAppearing(navigationContext);
        }

        public override async void OnDisappearing()
        {
            await _sensorManager.Distance.StartReadingsAsync(BandSensorSampleRate.Ms128);

            base.OnDisappearing();
        }
    }
}