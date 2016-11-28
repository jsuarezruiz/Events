using dotnetspain2015.Services.Localize;
using System.Threading;
using dotnetspain2015.WinPhone.Services.Localize;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalizeService))]
namespace dotnetspain2015.WinPhone.Services.Localize
{
    class LocalizeService: ILocalizeService
    {
        public void SetLocale()
        {

        }

        public System.Globalization.CultureInfo GetCurrentCultureInfo()
        {
            return Thread.CurrentThread.CurrentUICulture;
        }
    
    }
}
