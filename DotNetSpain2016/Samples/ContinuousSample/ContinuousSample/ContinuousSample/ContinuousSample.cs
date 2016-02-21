using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace ContinuousSample
{
	public class CustomCell : ViewCell
	{
		public CustomCell ()
		{
			var icon = new Image {
				HeightRequest = 50,
				WidthRequest = 50,
				Aspect = Aspect.AspectFill,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
			};

			icon.SetBinding (Image.SourceProperty, "DisplayIcon");


			var title = new Label {
				FontSize = Device.GetNamedSize (NamedSize.Medium, typeof(Label)),
				FontAttributes = FontAttributes.Bold
			};
			title.SetBinding(Label.TextProperty, new Binding("DisplayTemp"));

			var subTitle = new Label {

				FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
			};
				
			subTitle.SetBinding(Label.TextProperty, new Binding("DisplayDate"));

			View = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				Padding = new Thickness (15, 5, 5, 15),
				Children = {
					new StackLayout 
					{
						Orientation = StackOrientation.Horizontal,
						Children = { icon, title, subTitle }
					}
				}
			};
		}
	}

	public class CustomPage : ContentPage
	{
		const string ForecaseUri = "http://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=cf000962d14ef49fb37a835571427e3d";

		private WeatherForecastRoot data = null;
	
		protected override async void OnAppearing ()
		{
			base.OnAppearing ();

			data = await LoadData ();

			var stackLayout = new StackLayout ();

			var listView = new ListView ();
			listView.ItemsSource = data?.Items;
			listView.ItemTemplate = new DataTemplate (typeof(CustomCell));
			stackLayout.Children.Add (listView);

			Title = "Continuous";
			Content = stackLayout;
		}

		private async Task<WeatherForecastRoot> LoadData()
		{
			var client = new HttpClient ();
			var url = string.Format(ForecaseUri, "Madrid");
			var json = await client.GetStringAsync (url);

			return JsonConvert.DeserializeObject<WeatherForecastRoot>(json);
		}
	}

	public class BasicPage : ContentPage
	{	
		public BasicPage()
		{
			Content = new StackLayout 
			{	
				VerticalOptions = LayoutOptions.Center,
				Children = {
					new Label {
						Text = "Welcome to Xamarin Forms using Continuous!",
						TextColor = Color.Red
					}
				}
			};
		}
	}

	public class App : Application
	{
		public App ()
		{
			// The root page of your application
			MainPage = 	
				new ContentPage {	
				Content = new StackLayout {	VerticalOptions = LayoutOptions.Center,
					Children = {
						new Label {
							Text = "Welcome to Xamarin Forms using Continuous!"
						}
					}
				}
			};
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}

