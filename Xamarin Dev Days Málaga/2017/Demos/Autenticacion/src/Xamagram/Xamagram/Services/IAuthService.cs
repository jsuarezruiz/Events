using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace Xamagram.Services
{
    public interface IAuthService
    {
        Task LoginAsync(MobileServiceClient client, MobileServiceAuthenticationProvider provider);
    }
}