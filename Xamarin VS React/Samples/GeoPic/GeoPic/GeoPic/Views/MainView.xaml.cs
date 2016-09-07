using GeoPic.ViewModels;
using System;
using Xamarin.Forms;

namespace GeoPic.Views
{
    public partial class MainView : TabbedPage
    {
        private object Parameter { get; set; }

        public MainView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            BindingContext = App.Locator.MainViewModel;

            // En Android, usaremos el botón flotante, eliminamos el botón de la colección ToolBarItems
            if (Device.OS == TargetPlatform.Android)
            {
                ToolbarItems.Remove(AddToolbarItem);

                FabButton.Clicked += AndroidAddButtonClicked;
            }
        }

        protected override void OnAppearing()
        {
            var viewModel = BindingContext as MainViewModel;
            if (viewModel != null) viewModel.OnAppearing(Parameter);
        }

        protected override void OnDisappearing()
        {
            var viewModel = BindingContext as MainViewModel;
            if (viewModel != null) viewModel.OnDisappearing();
        }

        void AndroidAddButtonClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as MainViewModel;
            if (viewModel != null)
                viewModel.AddCommand.Execute(null);
        }
    }
}
