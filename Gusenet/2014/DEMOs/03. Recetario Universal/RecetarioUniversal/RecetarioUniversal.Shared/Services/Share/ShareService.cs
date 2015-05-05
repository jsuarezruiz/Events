namespace RecetarioUniversal.Services.Share
{
    using Windows.ApplicationModel.DataTransfer;
    using Windows.UI.Xaml;
    using Windows.UI.Xaml.Controls;

    public class ShareService : IShareService
    {
        //Variables
        private DataTransferManager _transferManager;   //Static class that we use to initiate sharing operations.
        private string _title;
        private string _content;

        /// <summary>
        /// Share a Plain Text.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void Share(string title, string content)
        {
            //Get the DataTransferManager object that is specific to the active window.
            _transferManager = DataTransferManager.GetForCurrentView();
            // This event is fired when a sharing operation starts.
            _transferManager.DataRequested += OnShareRequested;

            _title = title;
            _content = content;

            DataTransferManager.ShowShareUI();
        }

        private void OnShareRequested(DataTransferManager sender,
            DataRequestedEventArgs args)
        {
            var view = GetCurrentView();
            if (view == null)
                return;

            DataPackage data = args.Request.Data;
            data.Properties.Title = _title;
            data.SetText(_content);
        }

        /// <summary>
        /// Get the current view from the share is launching.
        /// </summary>
        /// <returns></returns>
        internal FrameworkElement GetCurrentView()
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
                return frame.Content as FrameworkElement;

            return Window.Current.Content as FrameworkElement;
        }
    }
}
