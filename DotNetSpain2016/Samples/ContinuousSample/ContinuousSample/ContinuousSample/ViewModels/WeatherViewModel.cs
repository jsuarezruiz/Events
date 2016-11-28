using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace ContinuousSample
{
	public class WeatherViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<WeatherRoot> items; 
		private ICommand weatherCommand;

		WeatherService WeatherService { get; } = new WeatherService();

		public WeatherViewModel()
		{
			Items = new ObservableCollection<WeatherRoot> ();
		}

		public ObservableCollection<WeatherRoot> Items
		{
			get { return items; }
			set 
			{ 
				items = value; 
				OnPropertyChanged(); 
			}
		}

		public ICommand WeatherCommand =>
		weatherCommand ??
		(weatherCommand = new Command(async () => await ExecuteWeatherCommand()));

		private async Task ExecuteWeatherCommand()
		{
			var result = await WeatherService.GetForecast("Madrid");

			if(result != null)
			{
				foreach(var item in result.Items)
				{
					items.Add(item);
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged([CallerMemberName]string name = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}
