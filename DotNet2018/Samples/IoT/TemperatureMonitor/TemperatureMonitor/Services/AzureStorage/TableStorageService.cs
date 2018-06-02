using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TemperatureMonitor.Models;
using TemperatureMonitor.Shared;

namespace TemperatureMonitor.Services.AzureStorage
{
    public class TableStorageService : ITableStorageService
    {
        private readonly CloudTableClient _client;

        public TableStorageService()
        {
            var storageAccount = CloudStorageAccount.Parse(AppSettings.StorageConnectionString);
            _client = storageAccount.CreateCloudTableClient();
        }

        public async Task<bool> SaveDataAsync(SensorData data)
        {
            try
            {
                var table = _client.GetTableReference(AppSettings.StorageTableName);
                await table.CreateIfNotExistsAsync();

                var entity = new SensorDataTableEntity(AppSettings.StoragePartitionKey, data.Temperature, data.Humidity);
                var operation = TableOperation.InsertOrReplace(entity);
                var result = await table.ExecuteAsync(operation);

                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }
    }
}