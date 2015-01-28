using System.Diagnostics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms_Services.iOS.Services.Call;
using Xamarin.Forms_Services.Services.Call;

[assembly: Dependency(typeof(CallService))]
namespace Xamarin.Forms_Services.iOS.Services.Call
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
                NSUrl url = new NSUrl(string.Format(@"telprompt://{0}", phone));
                UIApplication.SharedApplication.OpenUrl(url);
            }
            else
            {
                Debug.WriteLine("Invalid phone number!");
            }
        }
    }
}