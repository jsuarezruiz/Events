using Microsoft.Phone.Controls;
using Xamarin.Forms.Platform.WinPhone;
using Xamarin.Forms_Services.WinPhone.Services.Call;

namespace Xamarin.Forms_Services.WinPhone
{
    public partial class MainPage : FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            SupportedOrientations = SupportedPageOrientation.PortraitOrLandscape;

            Forms.Forms.Init();
            CallService.Init();

            LoadApplication(new Forms_Services.App());
        }
    }
}