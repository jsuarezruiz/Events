using System.Collections.Generic;

namespace Recetario.Services.LocalData
{
    public interface ILocalDataService
    {
        IEnumerable<T> Load<T>(string file);
    }
}
