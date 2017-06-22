using Microsoft.WindowsAzure.Storage;
using Plugin.Media.Abstractions;
using System;
using System.Threading.Tasks;

namespace Xamagram.Services
{
    public class BlobService
    {
        private static BlobService _instance;

        public static BlobService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BlobService();
                }

                return _instance;
            }
        }

        public async Task<string> UploadPhotoAsync(MediaFile photo)
        {
            // Conectar con la cuenta Azure Storage.
            // NOTA: Se deben utilizar tokens SAS en lugar de Shared Keys en aplicaciones en producción.
            var storageAccount = CloudStorageAccount.Parse(GlobalSettings.BlobSharedKey);
            var blobClient = storageAccount.CreateCloudBlobClient();

            // Crear el contenedor blob si no existe.
            var container = blobClient.GetContainerReference(GlobalSettings.BlobContainerName);
            await container.CreateIfNotExistsAsync();

            // Subimos el blob a Azure Storage.
            var blob = container.GetBlockBlobReference(Guid.NewGuid().ToString());
            blob.Properties.ContentType = "image/png";
            await blob.UploadFromStreamAsync(photo.GetStream());

            return blob.Uri.ToString();
        }
    }
}
