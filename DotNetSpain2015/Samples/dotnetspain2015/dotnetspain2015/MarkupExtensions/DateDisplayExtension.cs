using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace dotnetspain2015.MarkupExtensions
{
    [ContentProperty("DisplayType")] 
    public class DateDisplayExtension : IMarkupExtension
    {
        public string DisplayType
        {
            get;
            set;
        }

        public string Binding
        {
            get;
            set;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
                throw new ArgumentNullException("A ServiceProvider must be supplied");

            // Lets try and calculate the value of the column
            if (DisplayType == "Date")
                return DateTime.Now.ToString("D");
            if (DisplayType == "Time")
                return DateTime.Now.ToString("t");
            return "The value could not be determined";
        }
    }
}
