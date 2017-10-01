using ListPerformance.Models;
using System;
using Xamarin.Forms;

namespace ListPerformance.Views.Templates
{
    public partial class MonkeyCell : ViewCell
    {
        public MonkeyCell()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            var item = BindingContext as Monkey;

            if (item == null)
            {
                return;
            }

            MonkeyImage.Source = ImageSource.FromUri(new Uri(item.Image));
            MonkeyName.Text = item.Name;
        }
    }
}