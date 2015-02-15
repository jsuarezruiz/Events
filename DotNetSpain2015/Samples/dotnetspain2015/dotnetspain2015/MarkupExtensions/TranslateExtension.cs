using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using dotnetspain2015.Services.Localize;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dotnetspain2015.MarkupExtensions
{
    class TranslateExtension : IMarkupExtension
    {
        const string ResourceId = "dotnetspain2015.Resx.AppResources";
        readonly CultureInfo _ci;

        public TranslateExtension()
        {
            _ci = DependencyService.Get<ILocalizeService>().GetCurrentCultureInfo();
        }

        public string Text { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            var temp = new ResourceManager(ResourceId
            , typeof(TranslateExtension).GetTypeInfo().Assembly);
            var translation = temp.GetString(Text, _ci) ?? Text;

            return translation;
        }
    }
}
