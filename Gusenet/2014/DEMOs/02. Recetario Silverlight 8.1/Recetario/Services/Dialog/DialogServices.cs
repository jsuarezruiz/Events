using System.Windows;
using Recetario.Services.Interfaces;

namespace Recetario.Services
{
    public class DialogService : IDialogService
    {
        public void Show(string message)
        {
            MessageBox.Show(message);
        }

        public void Show(string message, string caption)
        {
            MessageBox.Show(message, caption, MessageBoxButton.OK);
        }
    }
}