using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

namespace Calculator.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			Xamarin.Forms.Forms.ViewInitialized += (sender, e) =>
			{
				if (null != e.View.StyleId) 
				{
					e.NativeView.AccessibilityIdentifier = e.View.StyleId;
				}
			};

			Appearance.Configure ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

