using Android.Content;
using Android.Net;
using GeoPic.Droid.Services.Internet;
using GeoPic.Services.Internet;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetService))]
namespace GeoPic.Droid.Services.Internet
{
    public class InternetService : IInternetService
    {
        public bool HasConnection()
        {
            var connectivityManager = (ConnectivityManager)
            Android.App.Application.Context.GetSystemService(Context.ConnectivityService);
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            return (activeConnection != null) && activeConnection.IsConnected;
        }
    }
}