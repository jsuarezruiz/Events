using GeoPic.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoPic.Services.GeoPic
{
    public interface IGeoPicService
    {
        Task<IEnumerable<Photo>> GetPhotosAsync();

        Task<bool> UploadPhotoAsync(PhotoUpload photo);
    }
}
