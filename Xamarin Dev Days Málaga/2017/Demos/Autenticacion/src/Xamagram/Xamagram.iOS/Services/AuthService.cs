using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using UIKit;
using Xamagram.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(AuthService))]
namespace Xamagram.Services
{
    public class AuthService : IAuthService
    {
        public Task LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider)
        {
            return client.LoginAsync(UIApplication.SharedApplication.KeyWindow.RootViewController, provider);
        }
    }
}