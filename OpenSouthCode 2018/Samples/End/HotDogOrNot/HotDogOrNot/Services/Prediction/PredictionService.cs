using HotDogOrNot.Models;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HotDogOrNot.Services.Prediction
{
    public class PredictionService
    {
        private static PredictionService _instance;

        public static PredictionService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PredictionService();

                return _instance;
            }
        }

        public async Task<VisionResult> MakePredictionRequestAsync(MediaFile file)
        {
            var client = new HttpClient();

            // Request headers - Send in the header your valid subscription key.
            client.DefaultRequestHeaders.Add("Prediction-Key", AppSettings.PredictionKey);

            // Request body. Loads Image from Disk.
            byte[] byteData = GetImageAsByteArray(file);

            using (var content = new ByteArrayContent(byteData))
            {
                // Set Content Type to Stream
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

                // Call the Prediction API
                HttpResponseMessage response = await client.PostAsync(AppSettings.PredictionURL, content);

                var responseString = await response.Content.ReadAsStringAsync();

                // Convert into VisionResult Model in hotdog.model for easier manipulation
                return JsonConvert.DeserializeObject<VisionResult>(responseString);
            }
        }

        private byte[] GetImageAsByteArray(MediaFile file)
        {
            var stream = file.GetStream();
            BinaryReader binaryReader = new BinaryReader(stream);
            return binaryReader.ReadBytes((int)stream.Length);
        }
    }
}