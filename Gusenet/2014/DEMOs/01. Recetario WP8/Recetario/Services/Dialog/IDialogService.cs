
namespace Recetario.Services.Interfaces
{
    public interface IDialogService
    {
        void Show(string message);

        void Show(string message, string caption);
    }
}
