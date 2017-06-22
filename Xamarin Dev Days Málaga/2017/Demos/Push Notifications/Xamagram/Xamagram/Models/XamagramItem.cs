using Microsoft.WindowsAzure.MobileServices;

namespace Xamagram.Models
{
    public class XamagramItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Version]
        public string AzureVersion { get; set; }
    }
}
