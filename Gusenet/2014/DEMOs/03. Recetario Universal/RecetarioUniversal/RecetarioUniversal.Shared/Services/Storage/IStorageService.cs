namespace RecetarioUniversal.Services.Storage
{
    using System.Threading.Tasks;
    using Windows.Storage;

    public interface IStorageService
    {
        Task<T> LoadObjectFromStorage<T>();
        Task SaveObjectToStorage<T>(T ObjectToSave);
    }
}
