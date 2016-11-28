using System.Threading.Tasks;
using GeoPic.Services.Dialog;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using GeoPic.iOS.Services.Dialog;

[assembly: Dependency(typeof(DialogService))]
namespace GeoPic.iOS.Services.Dialog
{
    public class DialogService : IDialogService
    {
        readonly List<UIAlertView> _openDialogs = new List<UIAlertView>();

        public async Task<bool> ShowMessage(string title, string message)
        {
            return await Task.Run(() => Alert(title, message));
        }

        private bool Alert(string title, string message)
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var alert = new UIAlertView(title, message, null, null);
                _openDialogs.Add(alert);
                alert.Clicked += (sender, buttonArgs) =>
                {
                    _openDialogs.Remove(alert);
                };
                alert.Show();
            });

            return true;
        }
    }
}