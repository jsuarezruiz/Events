using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;

namespace Recetario.Services.Storage
{
    public class StorageService : IStorageService
    {
        // Deserializa un objeto de un fichero
        public T Load<T>(string fileName)
        {
            // Accede al Isolated Storage
            using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!myIsolatedStorage.FileExists(fileName))
                {
                    // El fichero no existe.
                    return default(T);
                }

                // El fichero existe. Abrelo
                using (IsolatedStorageFileStream stream = myIsolatedStorage.OpenFile(fileName, FileMode.Open))
                {
                    // Deserializa el contenido
                    var xml = new XmlSerializer(typeof (T));
                    var data = (T) xml.Deserialize(stream);
                    return data;
                }
            }
        }

        // Serializa un objeto a fichero
        public bool Save<T>(string fileName, T data)
        {
            try
            {
                // Accede al Isolated Storage
                using (IsolatedStorageFile myIsolatedStorage = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    // Crea un fichero para el contenido
                    using (IsolatedStorageFileStream fileStream = myIsolatedStorage.CreateFile(fileName))
                    {
                        // Serializa el objeto al fichero
                        var xml = new XmlSerializer(typeof (T));
                        xml.Serialize(fileStream, data);
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}