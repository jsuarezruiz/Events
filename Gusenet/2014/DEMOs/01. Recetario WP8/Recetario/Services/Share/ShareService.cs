using Microsoft.Phone.Tasks;

namespace Recetario.Services.Share
{
    public class ShareService : IShareService
    {
        public void Share(string title, string message, string link)
        {
            var shareLinkTask = new ShareLinkTask
            {
                Title = title,
                Message = message,
                LinkUri = new System.Uri(link, System.UriKind.Absolute)
            };

            shareLinkTask.Show();
        }
    }
}
