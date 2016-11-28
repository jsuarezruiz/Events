using System.Diagnostics;
using Microsoft.Phone.Tasks;
using Xamarin.Forms;
using Xamarin.Forms_Services.Services.Call;
using Xamarin.Forms_Services.WinPhone.Services.Call;

[assembly: Dependency(typeof(CallService))]
namespace Xamarin.Forms_Services.WinPhone.Services.Call
{
    public class CallService : ICallService
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }
        public void MakeCall(string phone)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(phone, "^(\\(?\\+?[0-9]*\\)?)?[0-9_\\- \\(\\)]*$"))
            {
                var phoneCallTask = new PhoneCallTask {PhoneNumber = phone};

                phoneCallTask.Show();
            }
            else
            {
                Debug.WriteLine("Invalid number!");
            }
        }
    }
}
