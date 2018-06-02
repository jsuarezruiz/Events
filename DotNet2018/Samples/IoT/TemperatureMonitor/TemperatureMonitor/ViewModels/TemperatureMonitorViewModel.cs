using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using TemperatureMonitor.Models;
using TemperatureMonitor.Services.AzureStorage;
using TemperatureMonitor.Services.RestApi;
using TemperatureMonitor.Services.Temperature;
using TemperatureMonitor.Shared;
using TemperatureMonitor.ViewModels.Base;
using Xamarin.Forms;

namespace TemperatureMonitor.ViewModels
{
    public class TemperatureMonitorViewModel : ViewModelBase
    {
        private readonly CancellationTokenSource _cancellationTokenSource;
        private DateTime _date;
        private SensorData _latest;
        private ObservableCollection<SensorData> _summary;
        private PlotModel _temperaturePlotModel;
        private PlotModel _humidyPlotModel;
        private bool _isLoading;

        private IRestApiService _restApiService;
        private ITemperatureSensorService _temperatureSensorService;
        private ITableStorageService _tableStorageService;

        public TemperatureMonitorViewModel(
            IRestApiService restApiService,
            ITemperatureSensorService temperatureSensorService,
            ITableStorageService tableStorageService)
        {
            _restApiService = restApiService;
            _temperatureSensorService = temperatureSensorService;
            _tableStorageService = tableStorageService;

            _cancellationTokenSource = new CancellationTokenSource();

            IsLoading = true;
            Summary = new ObservableCollection<SensorData>();
            Date = DateTime.Now;

            RefreshCommand = new Command(async () => await RefreshAsync());
            NextDateCommand = new Command(async  () => await NextDateAsync());
            PreviousDateCommand = new Command(async  () => await PreviousDateAsync());
        }

        public DateTime Date
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged();
            }
        }

        public SensorData Latest
        {
            get { return _latest; }
            set
            {
                _latest = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SensorData> Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                OnPropertyChanged();
            }
        }

        public PlotModel TemperaturePlotModel
        {
            get { return _temperaturePlotModel; }
            set
            {
                _temperaturePlotModel = value;
                OnPropertyChanged();
            }
        }

        public PlotModel HumidityPlotModel
        {
            get { return _humidyPlotModel; }
            set
            {
                _humidyPlotModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand { get; private set; }

        public ICommand PreviousDateCommand { get; private set; }

        public ICommand NextDateCommand { get; private set; }

        public override async void OnAppearing(object navigationContext)
        {
            StartSensorLogger();

            await GetLatestTemperatureAsync();
            await GetDateSummaryAsync(Date);

            base.OnAppearing(navigationContext);
        }

        public override void OnDisappearing()
        {
            StopSensorLogger();

            base.OnDisappearing();
        }

        private void StartSensorLogger()
        {
            Task.Run(async () =>
            {
                while (!_cancellationTokenSource.IsCancellationRequested)
                {
                    var sensorData = _temperatureSensorService.GetData();
                    await _tableStorageService.SaveDataAsync(sensorData);
                    await Task.Delay(5000);
                }

            }, _cancellationTokenSource.Token);
        }

        private void StopSensorLogger()
        {
            _cancellationTokenSource.Cancel();
        }

        private async Task GetLatestTemperatureAsync()
        {
            var result = await _restApiService.GetLatestAsync(AppSettings.DefaultTimeZone);

            if (result != null)
            {
                Latest = result;
            }
        }

        private async Task RefreshAsync()
        {
            await LoadData();
        }

        private async Task NextDateAsync()
        {
            Date = Date.AddDays(1);

            await LoadData();
        }

        private async Task PreviousDateAsync()
        {
            Date = Date.AddDays(-1);

            await LoadData();
        }

        private async Task GetDateSummaryAsync(DateTime date)
        {
            try
            {
                IsLoading = true;

                var model = await _restApiService.GetDateSummaryAsync(AppSettings.DefaultTimeZone, date);

                if (model != null)
                {
                    var results = model
                        .OrderBy(m => m.Date)
                        .ToList();

                    Summary.Clear();

                    foreach (var result in results)
                    {
                        Summary.Add(result);
                    }

                    LoadTemperaturePlot();
                    LoadHumidityemperaturePlot();          
                }
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadData()
        {
            await GetLatestTemperatureAsync();
            await GetDateSummaryAsync(Date);
        }

        private void LoadTemperaturePlot()
        {
            TemperaturePlotModel = new PlotModel
            {
                Title = "Temperature",
                TitleColor = OxyColors.Black              
            };

            var magnitudeAxis = new LinearAxis()
            {
                Minimum = 0,
                Maximum = 40,
                Title = "Temperature Cº",
                MajorGridlineThickness = 1,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.Parse("#E9ECEF"),
                CropGridlines = true,
                TickStyle = TickStyle.None
            };

            TemperaturePlotModel.Axes.Add(magnitudeAxis);

            var dateAxis = new CategoryAxis()
            {
                TickStyle = TickStyle.Outside,
                MaximumPadding = 0.05,
                MinimumPadding = 0.05
            };

            TemperaturePlotModel.Axes.Add(dateAxis);

            var temperatureCandleSerie = new ColumnSeries
            {
                FillColor = OxyColors.Red,    
                ColumnWidth = 0.5
            };

            TemperaturePlotModel.Series.Add(temperatureCandleSerie);

            var result = Summary
                .GroupBy(d => Convert.ToDateTime(d.Date).Hour)
             .Select(i => new
             {
                 Hour = i.Key,
                 Avg = i.Average(v => v.Temperature),
                 Min = i.Min(v => v.Temperature),
                 Max = i.Max(v => v.Temperature)
             });

            foreach (var item in result)
            {
                dateAxis.Labels.Add(string.Format("{0}:00", item.Hour));
                temperatureCandleSerie.Items.Add(new ColumnItem(item.Avg));
            }
        }

        private void LoadHumidityemperaturePlot()
        {
            HumidityPlotModel = new PlotModel
            {
                Title = "Humidity",
                TitleColor = OxyColors.Black
            };

            var magnitudeAxis = new LinearAxis()
            {
                Minimum = 0,
                Title = "Humidity %",
                MajorGridlineThickness = 1,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.Parse("#E9ECEF"),
                CropGridlines = true,
                TickStyle = TickStyle.None
            };

            HumidityPlotModel.Axes.Add(magnitudeAxis);

            var dateAxis = new CategoryAxis()
            {
                TickStyle = TickStyle.Outside
            };

            HumidityPlotModel.Axes.Add(dateAxis);

            var humiditySerie = new ColumnSeries
            {
                FillColor = OxyColors.Blue
            };

            HumidityPlotModel.Series.Add(humiditySerie);

            var result = Summary
                .GroupBy(d => Convert.ToDateTime(d.Date).Hour)
             .Select(i => new
             {
                 Hour = i.Key,
                 Avg = i.Average(v => v.Humidity),
                 Min = i.Min(v => v.Humidity),
                 Max = i.Max(v => v.Humidity)
             });

            foreach (var item in result)
            {
                dateAxis.Labels.Add(string.Format("{0}:00", item.Hour));
                humiditySerie.Items.Add(new ColumnItem(item.Avg));
            }
        }
    }
}