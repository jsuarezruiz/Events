using LiveXAMLAddin.Windows;
using MonoDevelop.Components.Commands;

namespace LiveXAMLAddin.Commands
{
    public class LiveXAMLCommand : CommandHandler
    {
        protected override void Update(CommandInfo info)
        {
            info.Enabled = true;
            info.Visible = true;
        }

        protected override void Run()
        {
            new LiveXAMLWindow().Show();
        }
    }
}