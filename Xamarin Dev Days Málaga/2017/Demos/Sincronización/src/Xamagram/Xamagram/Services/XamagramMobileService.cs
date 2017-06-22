using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamagram.Models;

namespace Xamagram.Services
{
    public class XamagramMobileService
    {
        private MobileServiceClient _client;
        private IMobileServiceSyncTable<XamagramItem> _xamagramItemTable;
        private static XamagramMobileService _instance;

        public static XamagramMobileService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XamagramMobileService();
                }

                return _instance;
            }
        }

        public async Task InitializeAsync()
        {
            if (_client != null)
                return;

            // Inicialización de SQLite local
            var store = new MobileServiceSQLiteStore("xamagram.db");
            store.DefineTable<XamagramItem>();

            _client = new MobileServiceClient(GlobalSettings.AzureUrl);
            _xamagramItemTable = _client.GetSyncTable<XamagramItem>();

            //Inicializa the utilizando IMobileServiceSyncHandler.
            await _client.SyncContext.InitializeAsync(store,
                new MobileServiceSyncHandler());

            if (CrossConnectivity.Current.IsConnected)
            {
                try
                {
                    // Subir cambios a la base de datos remota
                    await _client.SyncContext.PushAsync();

                    await _xamagramItemTable.PullAsync(
                        "allXamagramItems", _xamagramItemTable.CreateQuery());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: {0}", ex.Message);
                }
            }
        }

        public async Task<IEnumerable<XamagramItem>> ReadXamagramItemsAsync()
        {
            await InitializeAsync();
            return await _xamagramItemTable.ReadAsync();
        }

        public async Task AddOrUpdateXamagramItemAsync(XamagramItem xamagramItem)
        {
            await InitializeAsync();

            if (string.IsNullOrEmpty(xamagramItem.Id))
            {
                await _xamagramItemTable.InsertAsync(xamagramItem);
            }
            else
            {
                await _xamagramItemTable.UpdateAsync(xamagramItem);
            }

            await SynchronizeXamagramItemAsync(xamagramItem.Id);
        }

        public async Task DeleteXamagramItemAsync(XamagramItem xamagramItem)
        {
            await InitializeAsync();

            await _xamagramItemTable.DeleteAsync(xamagramItem);

            await SynchronizeXamagramItemAsync(xamagramItem.Id);
        }

        private async Task SynchronizeXamagramItemAsync(string xamagramItemId)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;

            try
            {              
                // Subir cambios a la base de datos remota
                await _client.SyncContext.PushAsync();

                // El primer parámetro es el nombre de la query utilizada intermanente por el client SDK para implementar sync.
                // Utiliza uno diferente por cada query en la App
                await _xamagramItemTable.PullAsync("syncXamagramItem" + xamagramItemId,
                    _xamagramItemTable.Where(r => r.Id == xamagramItemId));
            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult != null)
                {
                    foreach (var result in ex.PushResult.Errors)
                    {
                        await ResolveErrorAsync(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Excepción: {0}", ex.Message);
            }
        }

        private async Task ResolveErrorAsync(MobileServiceTableOperationError result)
        {
            // Ignoramos si no podemos validar ambas partes.
            if (result.Result == null || result.Item == null)
                return;

            var serverItem = result.Result.ToObject<XamagramItem>();
            var localItem = result.Item.ToObject<XamagramItem>();

            if (serverItem.Name == localItem.Name
                && serverItem.Id == localItem.Id)
            {
                // Los elementos sin iguales, ignoramos el conflicto
                await result.CancelAndDiscardItemAsync();
            }
            else
            {
                // Para nosotros, gana el cliente
                localItem.AzureVersion = serverItem.AzureVersion;
                await result.UpdateOperationAsync(JObject.FromObject(localItem));
            }
        }
    }
}
