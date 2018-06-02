using MonoDevelop.Components.Commands;
using Xamarin.Forms;

namespace LiveXAMLAddin.Commands
{
    public class InitXamarinFormsCommand : CommandHandler
    {
        protected override void Run()
        {
            Forms.Init();
        }
    }
}