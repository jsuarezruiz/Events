using System;
using Acquaint.Abstractions;
using Acquaint.Common.iOS;
using Acquaint.Data;
using Acquaint.Models;
using Acquaint.Util;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using FFImageLoading.Forms.Touch;
using Foundation;
using HockeyApp.iOS;
using Microsoft.Practices.ServiceLocation;
using UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Acquaint.XForms.iOS
{
	[Register("AppDelegate")]
    public partial class AppDelegate : FormsApplicationDelegate
    {
		// an IoC Container
		IContainer _IoCContainer;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure(Settings.HockeyAppId);
			manager.StartManager();

			RegisterDependencies();

			Settings.OnDataPartitionPhraseChanged += (sender, e) => {
				UpdateDataSourceIfNecessary();
			};

			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

            Forms.Init();

			Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();

            FormsMaps.Init();

			CachedImageRenderer.Init();

            LoadApplication(new App());

            ConfigureTheming();

            return base.FinishedLaunching(app, options);
        }

        void ConfigureTheming()
        {
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = Color.FromHex("547799").ToUIColor();
            UINavigationBar.Appearance.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };
            UIBarButtonItem.Appearance.SetTitleTextAttributes (new UITextAttributes { TextColor = UIColor.White }, UIControlState.Normal);
        }

        /// <summary>
        /// Registers dependencies with an IoC container.
        /// </summary>
        /// <remarks>
        /// Since some of our libraries are shared between the Forms and Native versions 
        /// of this app, we're using an IoC/DI framework to provide access across implementations.
        /// </remarks>
        void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(new EnvironmentService()).As<IEnvironmentService>();

            builder.RegisterInstance(new HttpClientHandlerFactory()).As<IHttpClientHandlerFactory>();

            builder.RegisterInstance(new DatastoreFolderPathProvider()).As<IDatastoreFolderPathProvider>();

			// Set the data source dependent on whether or not the data parition phrase is "UseLocalDataSource".
			// The local data source is mainly for use in TestCloud test runs, but the app can be used in local-only data mode if desired.
			if (Settings.IsUsingLocalDataSource)
                builder.RegisterInstance(_LazyFilesystemOnlyAcquaintanceDataSource.Value).As<IDataSource<Acquaintance>>();
            else
                builder.RegisterInstance(_LazyAzureAcquaintanceSource.Value).As<IDataSource<Acquaintance>>();

            _IoCContainer = builder.Build();

            var csl = new AutofacServiceLocator(_IoCContainer);
            ServiceLocator.SetLocatorProvider(() => csl);
        }

        /// <summary>
		/// Updates the data source if necessary.
		/// </summary>
		void UpdateDataSourceIfNecessary()
        {
            var dataSource = ServiceLocator.Current.GetInstance<IDataSource<Acquaintance>>();

			// Set the data source dependent on whether or not the data parition phrase is "UseLocalDataSource".
			// The local data source is mainly for use in TestCloud test runs, but the app can be used in local-only data mode if desired.

			// if the settings dictate that a local data source should be used, then register the local data provider and update the IoC container
			if (Settings.IsUsingLocalDataSource && !(dataSource is FilesystemOnlyAcquaintanceDataSource))
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(_LazyFilesystemOnlyAcquaintanceDataSource.Value).As<IDataSource<Acquaintance>>();
                builder.Update(_IoCContainer);
                return;
            }

            // if the settings dictate that a local data souce should not be used, then register the remote data source and update the IoC container
            if (!Settings.IsUsingLocalDataSource && !(dataSource is AzureAcquaintanceSource))
            {
                var builder = new ContainerBuilder();
                builder.RegisterInstance(_LazyAzureAcquaintanceSource.Value).As<IDataSource<Acquaintance>>();
                builder.Update(_IoCContainer);
            }
        }

        // we need lazy loaded instances of these two types hanging around because if the registration on IoC container changes at runtime, we want the same instances
        static Lazy<FilesystemOnlyAcquaintanceDataSource> _LazyFilesystemOnlyAcquaintanceDataSource = new Lazy<FilesystemOnlyAcquaintanceDataSource>(() => new FilesystemOnlyAcquaintanceDataSource());
        static Lazy<AzureAcquaintanceSource> _LazyAzureAcquaintanceSource = new Lazy<AzureAcquaintanceSource>(() => new AzureAcquaintanceSource());
    }
}

