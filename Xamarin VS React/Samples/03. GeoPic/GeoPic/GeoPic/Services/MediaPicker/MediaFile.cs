using ExifLib;
using System;
using System.IO;

namespace GeoPic.Services.MediaPicker
{
    public sealed class MediaFile : IDisposable
    {
        #region Private Member Variables

        private readonly Action<bool> _dispose;

        private readonly string _path;

        private readonly Func<Stream> _streamGetter;

        private bool _isDisposed;

        #endregion Private Member Variables

        #region Constructors

        public MediaFile(string path, Func<Stream> streamGetter, Action<bool> dispose = null)
        {
            _dispose = dispose;
            _streamGetter = streamGetter;
            _path = path;
        }

        ~MediaFile()
        {
            Dispose(false);
        }

        #endregion Constructors

        #region Public Properties

        public string Path
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException(null);
                }

                return _path;
            }
        }

        public Stream Source
        {
            get
            {
                if (_isDisposed)
                {

                    throw new ObjectDisposedException(null);
                }

                return _streamGetter();
            }
        }

        public JpegInfo Exif
        {
            get
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException(null);
                }

                var result = ExifReader.ReadJpeg(Source);

                Source.Seek(0, SeekOrigin.Begin);

                return result;
            }
        }
        #endregion Public Properties

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion Public Methods

        #region Private Methods

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
            if (_dispose != null)
            {
                _dispose(disposing);
            }
        }

        #endregion Private Methods
    }
}