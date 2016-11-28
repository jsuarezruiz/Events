namespace RecetarioUniversal.Base
{
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using Windows.UI.Xaml.Controls;
    using Windows.UI.Xaml.Navigation;

    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private Frame _appFrame;
        private bool _isBusy;

        public ViewModelBase()
        {
        }

        public Frame AppFrame
        {
            get { return _appFrame; }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set 
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method allows Page and ViewModel to communicate navigation states
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnNavigatedTo(NavigationEventArgs e) { }

        /// <summary>
        /// This method allows Page and ViewModel to communicate navigation states
        /// </summary>
        /// <param name="e"></param>
        public virtual void OnNavigatedFrom(NavigationEventArgs e) { }

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        internal void SetAppFrame(Frame viewFrame)
        {
            _appFrame = viewFrame;
        }
    }
}
