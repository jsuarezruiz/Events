using System.Threading.Tasks;

namespace GeoPic.Services.Dialog
{
    public interface IDialogService
    {
        Task<bool> ShowMessage(string title, string message);
    }
}
