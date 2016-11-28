namespace RecetarioUniversal.Services.Dialog
{
    using System.Threading.Tasks;

    public interface IDialogService
    {
        Task  Show(string message);
    }
}
