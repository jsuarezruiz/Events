using  Movies.Web.Models.Movie;
using  Movies.Web.Models.TVShow;
using  Movies.Web.Services.Movies;
using Movies.Web.Services.Navigation;
using  Movies.Web.Services.TVShow;
using  Movies.Web.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace  Movies.Web.ViewModels
{
    class HomeViewModel : ViewModelBase
    {
        private Movie _highlight;
        private ObservableCollection<Movie> _topRatedMovies;
        private ObservableCollection<Movie> _popularMovies;
        private ObservableCollection<TVShow> _topRatedTvShows;
        private ObservableCollection<TVShow> _popularTvShows;

        private IMoviesService _moviesService;
        private ITVShowService _tvShowService;
        private INavigationService _navigationService;

        public HomeViewModel(
            IMoviesService moviesService,
            ITVShowService tvShowService,
            INavigationService navigationService)
        {
            _moviesService = moviesService;
            _tvShowService = tvShowService;
            _navigationService = navigationService;

            TopRatedMovies = new ObservableCollection<Movie>();
        }

        public Movie Highlight
        {
            get { return _highlight; }
            set
            {
                _highlight = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> TopRatedMovies
        {
            get { return _topRatedMovies; }
            set
            {
                _topRatedMovies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Movie> PopularMovies
        {
            get { return _popularMovies; }
            set
            {
                _popularMovies = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TVShow> TopRatedTvShows
        {
            get { return _topRatedTvShows; }
            set
            {
                _topRatedTvShows = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TVShow> PopularTvShows
        {
            get { return _popularTvShows; }
            set
            {
                _popularTvShows = value;
                OnPropertyChanged();
            }
        }

        public ICommand MovieDetailCommand => new Command<Movie>(MovieDetailAsync);

        public override async Task InitializeAsync(object navigationData)
        {
            IsBusy = true;

            await LoadTopRatedMoviesAync();
            await LoadPopularMoviesAync();
            await LoadTopRatedTvShowsAync();
            await LoadPopularTvShowsAync();

            IsBusy = false;
        }

        private async Task LoadTopRatedMoviesAync()
        {
            var result = await _moviesService.GetTopRatedAsync();

            TopRatedMovies = new ObservableCollection<Movie>(result.Results.Take(10));
        }

        private async Task LoadPopularMoviesAync()
        {
            var result = await _moviesService.GetPopularAsync();

            PopularMovies = new ObservableCollection<Movie>(result.Results.Take(10));
            Highlight = result.Results.First();
        }

        private async Task LoadTopRatedTvShowsAync()
        {
            var result = await _tvShowService.GetTopRatedAsync();

            TopRatedTvShows = new ObservableCollection<TVShow>(result.Results.Take(10));
        }

        private async Task LoadPopularTvShowsAync()
        {
            var result = await _tvShowService.GetPopularAsync();

            PopularTvShows = new ObservableCollection<TVShow>(result.Results.Take(10));
        }

        private async void MovieDetailAsync(object obj)
        {
            if (obj is Movie movie)
            {
                await _navigationService.NavigateToAsync<DetailViewModel>(movie);
            }
        }
    }
}