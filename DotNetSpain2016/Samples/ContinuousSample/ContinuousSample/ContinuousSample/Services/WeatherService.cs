using System.Threading.Tasks;
using System.Net.Http;
using static Newtonsoft.Json.JsonConvert;

namespace ContinuousSample
{
    public class WeatherService 
	{
		const string ForecaseUri = "http://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=cf000962d14ef49fb37a835571427e3d";

		public async Task<WeatherForecastRoot> GetForecast(string id)
		{
            using (var client = new HttpClient())
            {
                var url = string.Format(ForecaseUri, id);
                var json = await client.GetStringAsync(url);

                if (string.IsNullOrWhiteSpace(json))
                    return null;

                return DeserializeObject<WeatherForecastRoot>(json);
            }
        }
	}
}

