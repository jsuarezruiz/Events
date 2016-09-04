using System;

namespace GeoPic.Services.MediaPicker
{
    public class MediaPickerArgs : EventArgs
    {
        public MediaPickerArgs(MediaFile mf)
        {
            MediaFile = mf;
        }

        public MediaFile MediaFile { get; private set; }
    }
}
