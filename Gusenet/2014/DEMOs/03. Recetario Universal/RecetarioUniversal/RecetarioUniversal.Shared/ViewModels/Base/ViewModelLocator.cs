namespace RecetarioUniversal.Base
{
    using Microsoft.Practices.Unity;
    using Services.Dialog;
    using Services.LocalData;
    using Services.Navigation;
    using Services.Share;
    using Services.Speech;
    using Services.Storage;
    using Services.Tile;
    using ViewModels;

    public class ViewModelLocator
    {
        readonly IUnityContainer _container;

        public ViewModelLocator()
        {
            _container = new UnityContainer();

            //ViewModels
            _container.RegisterType<MainViewModel>();
            _container.RegisterType<GroupDetailViewModel>();
            _container.RegisterType<RecipeDetailViewModel>();

            //Services
            _container.RegisterType<IDialogService, DialogService>();
            _container.RegisterType<ILocalDataService, LocalDataService>();
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IShareService, ShareService>();
            _container.RegisterType<ISpeechService, SpeechService>();
            _container.RegisterType<IStorageService, StorageService>();
            _container.RegisterType<ITileService, TileService>();
        }

        public MainViewModel MainViewModel
        {
            get { return _container.Resolve<MainViewModel>(); }
        }

        public GroupDetailViewModel GroupDetailViewModel
        {
            get { return _container.Resolve<GroupDetailViewModel>(); }
        }

        public RecipeDetailViewModel RecipeDetailViewModel
        {
            get { return _container.Resolve<RecipeDetailViewModel>(); }
        }
    }
}
