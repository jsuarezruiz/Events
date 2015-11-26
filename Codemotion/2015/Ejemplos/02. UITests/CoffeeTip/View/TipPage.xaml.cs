using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CoffeeTip.ViewModel;

namespace CoffeeTip.View
{
    public partial class TipPage : ContentPage
    {
        public TipPage()
        {
            InitializeComponent();
            BindingContext = new TipViewModel();
        }
    }
}

