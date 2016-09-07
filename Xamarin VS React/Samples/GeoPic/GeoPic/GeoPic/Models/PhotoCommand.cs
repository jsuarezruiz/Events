using System.Windows.Input;

namespace GeoPic.Models
{
    public class PhotoCommand : Photo
    {
        public ICommand Command { get; set; }
    }
}
