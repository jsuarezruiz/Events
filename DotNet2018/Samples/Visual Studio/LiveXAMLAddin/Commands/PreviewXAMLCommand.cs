using System;
using MonoDevelop.Components.Commands;
using MonoDevelop.Ide;
using MonoDevelop.Ide.Gui;

namespace LiveXAMLAddin.Commands
{
    public class PreviewXAMLCommand : CommandHandler
    {
        protected override void Run()
        {
            Pad pad = null;

            var pads = IdeApp.Workbench.Pads;

            foreach (var p in IdeApp.Workbench.Pads)
            {
                if (string.Equals(p.Id, "LiveXAMLAddin.Pads.XAMLPreviewerPad", StringComparison.OrdinalIgnoreCase))
                {
                    pad = p;
                }
            }

            if (pad == null)
            {
                var content = new Pads.XAMLPreviewerPad();

                pad = IdeApp.Workbench.ShowPad(content, "LiveXAMLAddin.Pads.XAMLPreviewerPad", "XAML Previewer", "Right", null);

                if (pad == null)
                    return;
            }

            pad.Sticky = true;
            pad.AutoHide = false;
            pad.BringToFront();
        }

        protected override void Update(CommandInfo info)
        {
            info.Visible = true;

            if (IdeApp.Workbench.ActiveDocument != null && IdeApp.Workbench.ActiveDocument.FileName.ToString().Contains(".xaml"))
            {
                info.Enabled = true;
            }
            else
            {
                info.Enabled = false;
            }
        }
    }
}