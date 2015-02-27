using System.Threading.Tasks;
using Xamarin.Forms;

namespace dotnetspain2015.Views
{
    public partial class LoadingView : ContentPage
    {
        public LoadingView()
        {
            InitializeComponent();

            Initialize();
        }
        async void Initialize()
        {
            await ExecuteStoryboardAsync();
            await this.Navigation.PushModalAsync(new MainView());
        }
        async Task ExecuteStoryboardAsync()
        {
            await Task.Delay(3000);
            await LogoImage.ScaleTo(0.75f, length: 1000, easing: Easing.CubicOut);
            await LogoImage.ScaleTo(25, length: 250, easing: Easing.CubicIn);
        }
    }
}
