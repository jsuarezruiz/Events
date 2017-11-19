using Movies.ViewModels;
using Xamarin.Forms;

namespace Movies.Views
{
    public partial class MovieDetailView : ContentPage
    {
        private MovieDetailViewModel _movieDetailView;

        public MovieDetailView(object parameter)
        {
            InitializeComponent();

            Parameter = parameter;

            _movieDetailView = new MovieDetailViewModel();
            BindingContext = _movieDetailView;
        }

        private object Parameter { get; set; }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _movieDetailView.OnAppearing(Parameter);
        }
    }
}