using System;
using System.Diagnostics;
using MonoDevelop.Ide;
using Xamarin.Forms;

namespace LiveXAMLAddin.ViewModels
{
    public class PreviewerViewModel : BindableObject
    {
        View _preview;

        public PreviewerViewModel()
        {
            PreviewXaml(IdeApp.Workbench.ActiveDocument.Editor.Text);

            IdeApp.Workbench.ActiveDocument.Editor.TextChanged += (sender , args) =>
            {                
                PreviewXaml(IdeApp.Workbench.ActiveDocument.Editor.Text);
            };
        }

        public View Preview
        {
            get { return _preview; }
            set
            {
                _preview = value;
                OnPropertyChanged();
            }
        }

        async void PreviewXaml(string xaml)
        {
            var contentPage = new ContentPage();

            try
            {
                if (string.IsNullOrEmpty(xaml))
                    return;

                await Helpers.Live.UpdatePageFromXamlAsync(contentPage, xaml);
            }
            catch (Exception exception)
            {
                // Error 
                Debug.WriteLine(exception.Message);
                var xamlException = Helpers.Live.GetXamlException(exception);
                await Helpers.Live.UpdatePageFromXamlAsync(contentPage, xamlException);
            }

            Preview = contentPage.Content;
        }
    }
}