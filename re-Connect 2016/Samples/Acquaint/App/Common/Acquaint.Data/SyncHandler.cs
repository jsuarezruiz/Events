using System.Threading.Tasks;
using Acquaint.Abstractions;
using Acquaint.Models;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using Newtonsoft.Json.Linq;

namespace Acquaint.Data
{
	public class SyncHandler<T> : IMobileServiceSyncHandler where T : IObservableEntityData
	{
		public async Task<JObject> ExecuteTableOperationAsync(IMobileServiceTableOperation operation)
		{
			MobileServicePreconditionFailedException preconditionFailedException = null;
			JObject result = null;

			do
			{
				preconditionFailedException = null;
				try
				{
					result = await operation.ExecuteAsync();
				}
				catch (MobileServicePreconditionFailedException ex)
				{
					preconditionFailedException = ex;
				}

				// there is a conflict between the local version and the server version of the item
				if (preconditionFailedException != null)
				{
					// get the server's version of the item
					var serverItem = preconditionFailedException.Value.ToObject<T>();

					// Replace the local pending item's version value with the server item's version value.
					// This will force the local change to override the server version.
					// This is somewhat destructive (clobbering), and not favorable for all scenarios.
					// See the below commented out code for an alternative strategy that is more conservative.
					operation.Item[MobileServiceSystemColumns.Version] = serverItem.Version;


					/* The following commented out lines do not force the local copy to the server. Instead, an error message is presented to the user. */
					/* This could be improved even futher by presenting both versions of the data to the user and letting him/het decide with to keep. */
					//var localItem = operation.Item.ToObject<Acquaintance>();
					//RaiseDataSyncErrorEvent(new DataSyncErrorEventArgs<Acquaintance>(localItem, serverItem));
					//operation.AbortPush();
					//return result;
				}
			} 
			while (preconditionFailedException != null);

			return result;
		}

		public Task OnPushCompleteAsync(MobileServicePushCompletionResult result)
		{
			return Task.FromResult(0);
		}

		/// <summary>
		/// An event that is fired when a data sync error occurs.
		/// </summary>
		public event DataSyncErrorEventHandler<Acquaintance> OnDataSyncError;

		/// <summary>
		/// Raises the data sync error event.
		/// </summary>
		/// <param name="e">A DataSyncErrorEventArgs or type T.</param>
		protected virtual void RaiseDataSyncErrorEvent(DataSyncErrorEventArgs<Acquaintance> e)
		{
			DataSyncErrorEventHandler<Acquaintance> handler = OnDataSyncError;

			if (handler != null)
				handler(this, e);
		}
	}
}

