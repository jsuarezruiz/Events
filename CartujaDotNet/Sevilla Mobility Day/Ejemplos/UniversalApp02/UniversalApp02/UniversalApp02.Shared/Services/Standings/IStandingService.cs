using UniversalApp02.Models;
using System.Threading.Tasks;

namespace UniversalApp02.Services.Standings
{
    public interface IStandingService
    {
        Task<StandingTable> GetSeasonConstructorStandingsCollectionAsync(string season = "current");

        Task<StandingTable> GetSeasonDriverStandingsCollectionAsync(string season = "current");
    }
}
