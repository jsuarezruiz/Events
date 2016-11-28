using GeoPic.iOS.Services.Internet;
using GeoPic.Services.Internet;
using System.Net;
using SystemConfiguration;
using Xamarin.Forms;

[assembly: Dependency(typeof(InternetService))]
namespace GeoPic.iOS.Services.Internet
{
    public class InternetService : IInternetService
    {
        private static NetworkReachability _defaultRoute;

        public InternetService()
        {
            _defaultRoute = new NetworkReachability(new IPAddress(0));
        }

        public bool HasConnection()
        {
            NetworkReachabilityFlags flags;
            _defaultRoute.TryGetFlags(out flags);

            return flags.HasFlag(NetworkReachabilityFlags.Reachable);
        }
    }
}