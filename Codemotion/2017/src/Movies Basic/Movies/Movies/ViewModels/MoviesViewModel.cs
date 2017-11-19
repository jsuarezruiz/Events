using Movies.Models;
using Movies.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.ViewModels
{
    public class MoviesViewModel : BindableObject
    {
        private ObservableCollection<Movie> _movies;

        public ObservableCollection<Movie> Movies
        {
            get { return _movies; }
            set
            {
                _movies = value;
                OnPropertyChanged();
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