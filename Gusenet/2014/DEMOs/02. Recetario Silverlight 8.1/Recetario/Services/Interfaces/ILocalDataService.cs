using System.Collections.Generic;

namespace Recetario.Services.Interfaces
{
    public interface ILocalDataService
    {
        IEnumerable<T> Load<T>(string file);
    }
}
