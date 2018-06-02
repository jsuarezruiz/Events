using System;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;

namespace LiveXAMLAddin.Helpers
{
    public static class Live
    {
        public static readonly Regex Regex = new Regex("x:Class=\"([^\"]+)\"");

        public static Page GetPage(Page page, string fullTypeName)
        {
            if (page == null)
                return null;

            if (page.GetType().FullName == fullTypeName)
                return page;

            return null;
        }

        public static Task UpdatePageFromXamlAsync(Page page, string xaml)
        {
            var taskCompletionSource = new TaskCompletionSource<Page>();

            Device.BeginInvokeOnMainThread(() =>
            {
                var bindingContext = page.BindingContext;
                try
                {
                    Debug.WriteLine("Loading XAML...");
                    LoadXaml(page, xaml);
                    page.ForceLayout(); // Update
                    taskCompletionSource.SetResult(page);
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception.Message);
                    taskCompletionSource.SetException(exception);
                }
                finally
                {
                    page.BindingContext = bindingContext;
                    Debug.WriteLine("XAML Loaded!");
                }
            });

            return taskCompletionSource.Task;
        }

        public static string GetXamlException(Exception exception)
        {
            XNamespace xmlns = "http://xamarin.com/schemas/2014/forms";

            var errorPage = new XDocument(
                new XElement(xmlns + "ContentPage",
                new XElement(xmlns + "ScrollView",
                new XElement(xmlns + "StackLayout",
                new XAttribute("Margin", "12, 0"),
                new XElement(xmlns + "Label",
                    new XAttribute("Text", "Oops!"),
                    new XAttribute("TextColor", "Red"),
                    new XAttribute("FontSize", "Large")
                ),
                new XElement(xmlns + "Label",
                    new XAttribute("Text", exception.Message),
                    new XAttribute("TextColor", "Red"),
                    new XAttribute("LineBreakMode", "CharacterWrap"),
                    new XAttribute("FontSize", "Small")
                ))))).ToString();

            return errorPage;
        }

        public static void LoadXaml(BindableObject view, string xaml)
        {
            var xamlAssembly = Assembly.Load(new AssemblyName("Xamarin.Forms.Xaml"));
            var xamlLoader = xamlAssembly.GetType("Xamarin.Forms.Xaml.XamlLoader");
            var load = xamlLoader.GetRuntimeMethod("Load", new[] { typeof(BindableObject), typeof(string) });

            try
            {
                load.Invoke(null, new object[] { view, xaml });
            }
            catch (TargetInvocationException exception)
            {
                throw exception.InnerException; // To show to the user in the ErrorPage!
            }
        }
    }
}
