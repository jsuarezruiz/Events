using Movies.Models;
using Movies.Services;
using Movies.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MoviesViewModel : ViewModelBase
    {
        private ObservableCollection<Movie> _movies;
        private Movie _movie;

        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged();
            }
        }
        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;

                if(_movie != null)
                {
                    NavigationService.Instance.NavigateTo<MovieDetailViewModel>(_movie);
                }
            }
        }

        public ICommand GetMoviesCommand => new Command(LoadMoviesAsync);

        private async void LoadMoviesAsync()
        {
            var result = await MoviesService.Instance.GetPopularAsync();

            var movies = result.Results;

            Movies = new ObservableCollection<Movie>(movies);
        }
    }
}