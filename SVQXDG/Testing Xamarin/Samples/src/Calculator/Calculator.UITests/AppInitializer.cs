using System;
using Xamarin.UITest;
using Xamarin.UITest.Utils;

namespace Calculator.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../../Calculator.Droid/bin/Release/Calculator.Droid.apk")
                    .WaitTimes(new WaitTimes())
                    .EnableLocalScreenshots()
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }

    public class WaitTimes : IWaitTimes
    {
        public TimeSpan GestureCompletionTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }

        public TimeSpan GestureWaitTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }

        public TimeSpan WaitForTimeout
        {
            get { return TimeSpan.FromMinutes(1); }
        }
    }
}

