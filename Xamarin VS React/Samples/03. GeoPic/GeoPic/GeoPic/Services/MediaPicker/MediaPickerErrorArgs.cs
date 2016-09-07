using System;

namespace GeoPic.Services.MediaPicker
{
    public class MediaPickerErrorArgs : EventArgs
    {
        public MediaPickerErrorArgs(Exception ex)
        {
            Error = ex;
        }

        public Exception Error { get; private set; }
    }
}
