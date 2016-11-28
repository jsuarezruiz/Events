using Orbit.Models;
using System.Collections.ObjectModel;

namespace Orbit.Services.Planets
{
    public interface IPlanetsService
    {
        ObservableCollection<Planet> GetPlanets();
    }
}
