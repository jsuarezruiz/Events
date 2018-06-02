using SlideOverKit;
using System;
using Xamarin.Forms;
using XamarinFormsARCarSample.ViewModels;

namespace XamarinFormsARCarSample.Views
{
    public partial class MainView : MenuContainerPage
    {
        private ColorsView _colorsView;

        public MainView()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);

            _colorsView = new ColorsView();

            SlideMenu = _colorsView;

            MessagingCenter.Subscribe<MainViewModel>(this, MessengerKeys.OpenColors, (sender) =>
            {
                OpenColors();
            });

            MessagingCenter.Subscribe<MainViewModel>(this, MessengerKeys.CloseColors, (sender) =>
            {
                CloseColors();
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            WaveEngineSurface.Game = App.Game;

            ForceLayout();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            if (_colorsView == null)
            {
                return;
            }

            _colorsView.BindingContext = BindingContext;
        }

        private void OpenColors()
        {
            ShowMenuAction?.Invoke();
        }

        private void CloseColors()
        {
            HideMenuAction?.Invoke();
        }
    }
}