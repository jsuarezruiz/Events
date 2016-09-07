using System;

namespace GeoPic.Services.MediaPicker
{
    public class MediaStorageOptions
    {
        #region Base Options

        protected MediaStorageOptions()
        {
        }

        public string Directory { get; set; }

        public string Name { get; set; }

        public int? MaxPixelDimension { get; set; }

        public int? PercentQuality { get; set; }
    }

    #endregion Base Options

    #region Camera Options

    public enum CameraDevice
    {
        Rear,
        Front
    }

    public class CameraMediaStorageOptions : MediaStorageOptions
    {
        public CameraMediaStorageOptions()
        {
            SaveMediaOnCapture = true;
        }

        public CameraDevice DefaultCamera { get; set; }

        public bool SaveMediaOnCapture { get; set; }
    }

    #endregion Camera Options

    #region Video Options

    public enum VideoQuality
    {
        Low = 0,
        Medium = 1,
        High = 2
    }

    public class VideoMediaStorageOptions : MediaStorageOptions
    {
        public VideoMediaStorageOptions()
        {
            Quality = VideoQuality.High;
            DesiredLength = TimeSpan.FromMinutes(10);
            SaveMediaOnCapture = true;
        }

        public CameraDevice DefaultCamera { get; set; }

        public bool SaveMediaOnCapture { get; set; }

        public TimeSpan DesiredLength { get; set; }

        public VideoQuality Quality { get; set; }
    }

    #endregion Video Options
}