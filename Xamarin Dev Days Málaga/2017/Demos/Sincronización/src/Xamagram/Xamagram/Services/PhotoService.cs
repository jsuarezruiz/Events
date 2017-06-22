using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Threading.Tasks;

namespace Xamagram.Services
{
    public class PhotoService
    {
        private static PhotoService _instance;

        public static PhotoService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PhotoService();
                }

                return _instance;
            }
        }

        public async Task<string> PickPhotoAsync()
        {
            try
            {
                var imagePath = string.Empty;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return null;
                }

                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file != null && file.Path != null)
                {
                    imagePath = file.Path;
                }

                return imagePath;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
        }

        public async Task<string> TakePhotoAsync()
        {
            try
            {
                var imagePath = string.Empty;

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    return null;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(
                    new StoreCameraMediaOptions());

                if (file != null && file.Path != null)
                {
                    imagePath = file.Path;
                }

                return imagePath;
            }
            catch (TaskCanceledException)
            {
                return null;
            }
        }
    }
}
