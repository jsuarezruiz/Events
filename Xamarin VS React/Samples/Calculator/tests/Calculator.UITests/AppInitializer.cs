using System;
using System.IO;
using System.Reflection;
using Xamarin.UITest;

namespace Calculator.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            string currentFile = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            FileInfo fi = new FileInfo(currentFile);
            string dir = fi.Directory.Parent.Parent.Parent.FullName;
            dir = dir.Replace("tests", "src");

            var pathToAPK = Path.Combine(dir, "Calculator.Android", "bin", "Release", "Calculator.Android.APK");

            return ConfigureApp
                .Android
                .ApkFile(pathToAPK)
                .StartApp();
        }
    }
}