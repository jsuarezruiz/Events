namespace RecetarioUniversal.Services.LocalData
{
    using RecetarioUniversal.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ILocalDataService
    {
        Task<IEnumerable<RecipeDataItem>> Load(string file);
    }
}
