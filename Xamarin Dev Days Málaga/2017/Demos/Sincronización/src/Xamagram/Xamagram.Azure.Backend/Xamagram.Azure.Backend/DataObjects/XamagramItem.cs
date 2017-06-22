using Microsoft.Azure.Mobile.Server;

namespace Xamagram.DataObjects
{
    public class XamagramItem : EntityData
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}