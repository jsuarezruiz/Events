using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using WeatherApp.Models;
using WeatherApp.Services;
using Xamarin.Forms;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : BindableObject
    {
        private bool _isBusy;
        private string _temp;
        private string _condition;
        private WeatherForecastRoot _forecast;
        private ICommand _reloadCommand;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChanged();
            }
        }

        public string Condition
        {
            get { return _condition; }
            set
            {
                _condition = value;
                OnPropertyChanged();
            }
        }

        public WeatherForecastRoot Forecast
        {
            get { return _forecast; }
            set
            {
                _forecast = value;
                OnPropertyChanged();
            }
        }

        public ICommand ReloadCommand =>
            _reloadCommand ??
            (_reloadCommand = new Command(async () => await GetWeatherAsync()));

        public async Task GetWeatherAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                WeatherRoot weatherRoot = null;
                var units = AppSettings.IsImperial ? Units.Imperial : Units.Metric;
                weatherRoot = await WeatherService.Instance.GetWeatherAsync(AppSettings.Location.Trim(), units);
                Forecast = await WeatherService.Instance.GetForecast(weatherRoot.CityId, units);
                var unit = AppSettings.IsImperial ? "F" : "C";
                Temp = $"{weatherRoot?.MainWeather?.Temperature ?? 0}°{unit}";
                Condition = $"{weatherRoot.Name}: {weatherRoot?.Weather?[0]?.Description ?? string.Empty}";
            }
            catch (Exception ex)
            {
                Temp = "Unable to get Weather";
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}