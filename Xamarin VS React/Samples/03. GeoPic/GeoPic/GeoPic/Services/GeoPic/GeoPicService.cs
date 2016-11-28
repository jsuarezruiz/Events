using GeoPic.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GeoPic.Services.GeoPic
{
    public class GeoPicService : IGeoPicService
    {
        private const string BaseUri = "http://vps.sirikon.me:3000/swagger";

        public async Task<IEnumerable<Photo>> GetPhotosAsync()
        {
            var uri = new Uri(string.Format(@"{0}/api/pictures", BaseUri));
            var client = new HttpClient();
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var photos = JsonConvert.DeserializeObject<IEnumerable<Photo>>(result);

                return photos;
            }

            return null;
        }

        public async Task<bool> UploadPhotoAsync(PhotoUpload photo)
        {
            var uri = new Uri(string.Format(@"{0}/api/pictures", BaseUri));
            var client = new HttpClient();

            var json = JsonConvert.SerializeObject(photo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var pictureUploadResult = JsonConvert.DeserializeObject<PictureUploadResult>(result);

                return pictureUploadResult.Status;
            }

            return false;
        }
    }
}