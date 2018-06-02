using Movies.Web.Models.Movie;
using Movies.Web.Services.Movies;
using Movies.Web.Services.Navigation;
using Movies.Web.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Movies.Web.ViewModels
{
    public class DetailViewModel : ViewModelBase
    {
        private Movie _movie;
        private ObservableCollection<MovieCastMember> _casting;
        private string _video;

        private INavigationService _navigationService;
        private IMoviesService _moviesService;

        public DetailViewModel(
            INavigationService navigationService,
            IMoviesService moviesService)
        {
            _navigationService = navigationService;
            _moviesService = moviesService;
        }

        public Movie Movie
        {
            get { return _movie; }
            set
            {
                _movie = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MovieCastMember> Casting
        {
            get { return _casting; }
            set
            {
                _casting = value;
                OnPropertyChanged();
            }
        }

        public string Video
        {
            get { return _video; }
            set
            {
                _video = value;
                OnPropertyChanged();
            }
        }

        public ICommand PlayCommand => new Command(Play);

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is Movie)
            {
                IsBusy = true;

                var movie = (Movie)navigationData;

                try
                {
                    Movie = await _moviesService.FindByIdAsync(movie.Id);
                }
                catch
                {
                    Movie = movie;
                }

                var credits = await _moviesService.GetCreditsAsync(movie.Id);
                Casting = new ObservableCollection<MovieCastMember>(credits.CastMembers.Take(10));
                var videos = await _moviesService.GetVideosAsync(movie.Id);

                if (videos.Videos.Any())
                {
                    var video = videos.Videos.First();
                    Video = string.Format("{0}{1}", AppSettings.YouTubeUrl, video.Key);
                }

                IsBusy = false;
            }
        }

        private void Play()
        {
            if (string.IsNullOrEmpty(Video))
                return;

            Device.OpenUri(new Uri(Video));
        }
    }
}