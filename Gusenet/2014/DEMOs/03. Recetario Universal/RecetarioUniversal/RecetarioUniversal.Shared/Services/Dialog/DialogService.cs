namespace RecetarioUniversal.Services.Dialog
{
    using Windows.UI.Popups;
    using System;
    using System.Threading.Tasks;

    public class DialogService : IDialogService
    {
        public async Task Show(string message)
        {
            MessageDialog messageDialog = new MessageDialog(message);
            await messageDialog.ShowAsync();
        }
    }
}
