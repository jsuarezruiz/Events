namespace Recetario.Services.Storage
{
    public interface IStorageService
    {
        T Load<T>(string fileName);
        bool Save<T>(string fileName, T data);
    }
}
