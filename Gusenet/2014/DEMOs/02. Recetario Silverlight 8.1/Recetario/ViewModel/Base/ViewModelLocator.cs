namespace Recetario.ViewModel.Base
{
    using Microsoft.Practices.Unity;
    using Services;
    using Services.Interfaces;
    using Services.Navigation;
    using Services.Reminder;
    using Services.Share;
    using Services.Speech;
    using Services.Storage;
    using Services.Tile;

    public class ViewModelLocator
    {
        // IoC container
        private readonly IUnityContainer _container;

        // Constructor
        public ViewModelLocator()
        {
            _container = new UnityContainer();

            // Añade los Services de los que dependen los ViewModel
            _container.RegisterType<ILiveTileServiceWP8, LiveTileServiceWP8>();
            _container.RegisterType<IStorageService, StorageService>();
            _container.RegisterType<IDialogService, DialogService>();
            _container.RegisterType<ILocalDataService, LocalDataService>();
            _container.RegisterType<INavigationService, NavigationService>();
            _container.RegisterType<IReminderService, ReminderService>();
            _container.RegisterType<IShareService, ShareService>();
            _container.RegisterType<ISpeechService, SpeechService>();

            // Añade los ViewModels y asociales los Services que le correspondan (especificados en el constructor de cada ViewModel)
            _container.RegisterType<MainViewModel, MainViewModel>(new ContainerControlledLifetimeManager());
            _container.RegisterType<GroupDetailViewModel, GroupDetailViewModel>(
                new ContainerControlledLifetimeManager());
            _container.RegisterType<RecipeDetailViewModel, RecipeDetailViewModel>(
                new ContainerControlledLifetimeManager());
        }

        // ViewModel principal para la colección de cartas
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