namespace RecetarioUniversal.Services.Storage
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using Windows.Storage;

    public class StorageService : IStorageService
    {
        private async Task<StorageFile> GetFileIfExistsAsync(StorageFolder folder, string fileName)
        {
            try
            {
                return await folder.GetFileAsync(fileName);

            }
            catch
            {
                return null;
            }
        }

        public async Task<T> LoadObjectFromStorage<T>()
        {
            T ObjToLoad = default(T);

            try
            {
                StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

                string filename = GetFileName<T>();
                StorageFile storageFile = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);

                //using (Stream inStream = await storageFolder.OpenStreamForReadAsync(filename))
                using (Stream inStream = await storageFile.OpenStreamForReadAsync())
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    ObjToLoad = (T)serializer.Deserialize(inStream);
                }
            }
            catch (Exception error)
            {
                throw new NotImplementedException(error.Message);
            }

            return ObjToLoad;
        }

        public async Task SaveObjectToStorage<T>(T ObjectToSave)
        {
            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            string filename = GetFileName<T>();

            using (Stream fs = await storageFolder.OpenStreamForWriteAsync(filename, CreationCollisionOption.ReplaceExisting))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    XmlSerializer ser = new XmlSerializer(typeof(T));
                    ser.Serialize(sw, ObjectToSave);
                }
            }
        }

        public string GetFileName<T>()
        {
            return typeof(T).FullName + ".xml";
        }

        public async Task<bool> IsObjectPersisted<T1>()
        {
            string file = GetFileName<T1>();

            StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            StorageFile storageFile = await GetFileIfExistsAsync(storageFolder, file);

            return (storageFile != null);
        }

        public T LoadSetttingFromStorage<T>(string Key)
        {
            T ObjToLoad = default(T);

            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(Key))
            {
                using (StringReader sr = new StringReader((string)ApplicationData.Current.LocalSettings.Values[Key]))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    ObjToLoad = (T)serializer.Deserialize(sr);
                }
            }

            return ObjToLoad;
        }

        public void SaveSettingToStorage(string Key, object Setting)
        {
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                XmlSerializer ser = new XmlSerializer(Setting.GetType());
                ser.Serialize(sw, Setting);
            }

            if (!ApplicationData.Current.LocalSettings.Values.ContainsKey(Key))
            {
                ApplicationData.Current.LocalSettings.Values.Add(Key, sb.ToString());
            }
            else
            {
                ApplicationData.Current.LocalSettings.Values[Key] = sb.ToString();
            }

        }

        public bool IsSettingPersisted(string Key)
        {
            return ApplicationData.Current.LocalSettings.Values.ContainsKey(Key);
        }
    }
}
