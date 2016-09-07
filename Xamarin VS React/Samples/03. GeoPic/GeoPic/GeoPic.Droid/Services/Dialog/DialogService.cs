using Android.App;
using System.Threading.Tasks;
using Xamarin.Forms;
using GeoPic.Services.Dialog;
using System.Collections.Generic;
using GeoPic.Droid.Services.Dialog;

[assembly: Dependency(typeof(DialogService))]
namespace GeoPic.Droid.Services.Dialog
{
    public class DialogService : IDialogService
    {
        List<AlertDialog> _openDialogs = new List<AlertDialog>();

        public async Task<bool> ShowMessage(string title, string content)
        {
            return await Task.Run(() => Alert(title, content));
        }

        private bool Alert(string title, string content)
        {
            var alert = new AlertDialog.Builder(Forms.Context);
            alert.SetTitle(title);
            alert.SetMessage(content);

            alert.SetNegativeButton("ok", (sender, e) =>
            {
                _openDialogs.Remove((AlertDialog)sender);
            });

            Device.BeginInvokeOnMainThread(() =>
            {
                var dialog = alert.Show();
                _openDialogs.Add(dialog);
                dialog.SetCanceledOnTouchOutside(false);
                dialog.SetCancelable(false);
            });

            return true;
        }
    }
}