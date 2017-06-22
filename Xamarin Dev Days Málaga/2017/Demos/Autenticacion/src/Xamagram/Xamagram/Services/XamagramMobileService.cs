using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;
using Plugin.Connectivity;
using Plugin.SecureStorage;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xamagram.Models;
using Xamarin.Forms;

namespace Xamagram.Services
{
    public partial class XamagramMobileService
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

        public XamagramMobileService()
        {
            _client = new MobileServiceClient(GlobalSettings.AzureUrl);
        }

        private async Task InitializeAsync()
        {
            if (_xamagramItemTable != null)
                return;

            // Inicialización de SQLite local
            var store = new MobileServiceSQLiteStore(GlobalSettings.Database);
            store.DefineTable<XamagramItem>();

            await _client.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());
            _xamagramItemTable = _client.GetSyncTable<XamagramItem>();

            // Limpiar registros offline.
            await _xamagramItemTable.PurgeAsync(true);
        }

        public async Task<IEnumerable<XamagramItem>> ReadXamagramItemsAsync()
        {
            await InitializeAsync();
            await SynchronizeAsync();
            return await _xamagramItemTable.ToEnumerableAsync();
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

            await SynchronizeAsync();
        }

        public async Task DeleteXamagramItemAsync(XamagramItem xamagramItem)
        {
            await InitializeAsync();

            await _xamagramItemTable.DeleteAsync(xamagramItem);

            await SynchronizeAsync();
        }

        private async Task SynchronizeAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;

            try
            {         
                // Subir cambios a la base de datos remota
                await _client.SyncContext.PushAsync();

                // El primer parámetro es el nombre de la query utilizada intermanente por el client SDK para implementar sync.
                // Utiliza uno diferente por cada query en la App
                await _xamagramItemTable.PullAsync($"all{nameof(XamagramItem)}", _xamagramItemTable.CreateQuery());
            }
            catch (MobileServicePushFailedException ex)
            {
                if (ex.PushResult.Status == MobileServicePushStatus.CancelledByAuthenticationError)
                {
                    await LoginAsync();
                    await SynchronizeAsync();
                    return;
                }

                if (ex.PushResult != null)
                    foreach (var result in ex.PushResult.Errors)
                        await ResolveErrorAsync(result);
            }
            catch (MobileServiceInvalidOperationException ex)
            {
                if (ex.Response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    await LoginAsync();
                    await SynchronizeAsync();
                    return;
                }

                throw;
            }
        }

        public async Task LoginAsync()
        {
            const string userIdKey = ":UserId";
            const string tokenKey = ":Token";

            if (CrossSecureStorage.Current.HasKey(userIdKey)
                && CrossSecureStorage.Current.HasKey(tokenKey))
            {
                string userId = CrossSecureStorage.Current.GetValue(userIdKey);
                string token = CrossSecureStorage.Current.GetValue(tokenKey);

                _client.CurrentUser = new MobileServiceUser(userId)
                {
                    MobileServiceAuthenticationToken = token
                };

                return;
            }

            var authService = DependencyService.Get<IAuthService>();
            await authService.LoginAsync(_client, MobileServiceAuthenticationProvider.Twitter);

            var user = _client.CurrentUser;

            if (user != null)
            {
                CrossSecureStorage.Current.SetValue(userIdKey, user.UserId);
                CrossSecureStorage.Current.SetValue(tokenKey, user.MobileServiceAuthenticationToken);
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
