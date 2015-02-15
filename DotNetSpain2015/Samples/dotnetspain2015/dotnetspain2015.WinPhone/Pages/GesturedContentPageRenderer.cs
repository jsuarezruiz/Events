using dotnetspain2015.CustomPages;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

namespace dotnetspain2015.WinPhone.Pages
{
    class GesturedContentPageRenderer : PageRenderer
    {
        GesturedContentPage _page;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e); 
            
            _page = (GesturedContentPage)e.NewElement;
        }
    }
}
