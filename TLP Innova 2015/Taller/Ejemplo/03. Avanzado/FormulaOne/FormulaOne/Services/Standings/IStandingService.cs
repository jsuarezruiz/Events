using FormulaOne.Models;
using System.Threading.Tasks;

namespace FormulaOne.Services.Standings
{
    public interface IStandingService
    {
        Task<StandingTable> GetSeasonConstructorStandingsCollectionAsync(string season = "current");

        Task<StandingTable> GetSeasonDriverStandingsCollectionAsync(string season = "current");
    }
}
