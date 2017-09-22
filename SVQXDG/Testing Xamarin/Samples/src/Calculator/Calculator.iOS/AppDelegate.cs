using Foundation;
using UIKit;
using Xamarin.Forms;

namespace Calculator.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // Newer version of Xamarin Studio and Visual Studio provide the
            // ENABLE_TEST_CLOUD compiler directive in the Debug configuration,
            // but not the Release configuration.

#if ENABLE_TEST_CLOUD
            Xamarin.Calabash.Start();
#endif

            global::Xamarin.Forms.Forms.Init();

            Forms.ViewInitialized += (object sender, ViewInitializedEventArgs e) =>
            {
                // http://developer.xamarin.com/recipes/testcloud/set-accessibilityidentifier-ios/
                if (null != e.View.AutomationId)
                {
                    e.NativeView.AccessibilityIdentifier = e.View.AutomationId;
                }
            };

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
