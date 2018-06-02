using  Movies.Web.Models;
using  Movies.Web.Models.Movie;
using System.Threading.Tasks;

namespace  Movies.Web.Services.Movies
{
    public interface IMoviesService
    {
        Task<Movie> FindByIdAsync(int movieId, string language = "en");

        Task<Movie> GetLatestAsync(string language = "en");

        Task<SearchResponse<Movie>> GetUpcomingAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetTopRatedAsync(int pageNumber = 1, string language = "en");

        Task<SearchResponse<Movie>> GetPopularAsync(int pageNumber = 1, string language = "en");

        Task<MovieCredit> GetCreditsAsync(int movieId, string language = "en");

        Task<MovieVideo> GetVideosAsync(int movieId, string language = "en");
    }
}