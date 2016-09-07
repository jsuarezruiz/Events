using System;
using System.Threading.Tasks;

namespace GeoPic.Services.MediaPicker
{
    public interface IMediaPicker
    {
        bool IsCameraAvailable { get; }

        bool IsPhotosSupported { get; }

        bool IsVideosSupported { get; }

        Task<MediaFile> SelectPhotoAsync(CameraMediaStorageOptions options);

        Task<MediaFile> TakePhotoAsync(CameraMediaStorageOptions options);

        Task<MediaFile> SelectVideoAsync(VideoMediaStorageOptions options);

        Task<MediaFile> TakeVideoAsync(VideoMediaStorageOptions options);

        EventHandler<MediaPickerArgs> OnMediaSelected { get; set; }

        EventHandler<MediaPickerErrorArgs> OnError { get; set; }
    }
}