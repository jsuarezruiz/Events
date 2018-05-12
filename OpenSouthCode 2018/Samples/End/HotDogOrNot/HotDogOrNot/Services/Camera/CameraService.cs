using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Threading.Tasks;

namespace HotDogOrNot.Services.Camera
{
    public class CameraService
    {
        private static CameraService _instance;

        public static CameraService Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CameraService();

                return _instance;
            }
        }

        public async Task<MediaFile> PickPhotoAsync()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                return null;
            }

            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);

            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Camera });
                status = results[Permission.Camera];
            }

            if (status == PermissionStatus.Granted)
            {
                var file = await CrossMedia.Current.PickPhotoAsync();

                if (file == null)
                {
                    return null;
                }

                return file;
            }

            return null;
        }
    }
}