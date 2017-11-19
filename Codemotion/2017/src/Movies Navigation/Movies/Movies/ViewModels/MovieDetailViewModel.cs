using Movies.Models;
using Movies.ViewModels.Base;

namespace Movies.ViewModels
{
    public class MovieDetailViewModel : ViewModelBase
    {
        private Movie _movie;

        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                OnPropertyChanged();
            }
        }

        public override void OnAppearing(object navigationContext)
        {
            if (navigationContext is Movie)
            {
                Movie = (Movie)navigationContext;
            }

            base.OnAppearing(navigationContext);
        }
    }
}