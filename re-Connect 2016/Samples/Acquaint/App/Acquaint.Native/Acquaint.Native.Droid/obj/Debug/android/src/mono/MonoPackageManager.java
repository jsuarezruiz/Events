package mono;

import java.io.*;
import java.lang.String;
import java.util.Locale;
import java.util.HashSet;
import java.util.zip.*;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.res.AssetManager;
import android.util.Log;
import mono.android.Runtime;

public class MonoPackageManager {

	static Object lock = new Object ();
	static boolean initialized;

	static android.content.Context Context;

	public static void LoadApplication (Context context, ApplicationInfo runtimePackage, String[] apks)
	{
		synchronized (lock) {
			if (context instanceof android.app.Application) {
				Context = context;
			}
			if (!initialized) {
				android.content.IntentFilter timezoneChangedFilter  = new android.content.IntentFilter (
						android.content.Intent.ACTION_TIMEZONE_CHANGED
				);
				context.registerReceiver (new mono.android.app.NotifyTimeZoneChanges (), timezoneChangedFilter);
				
				System.loadLibrary("monodroid");
				Locale locale       = Locale.getDefault ();
				String language     = locale.getLanguage () + "-" + locale.getCountry ();
				String filesDir     = context.getFilesDir ().getAbsolutePath ();
				String cacheDir     = context.getCacheDir ().getAbsolutePath ();
				String dataDir      = getNativeLibraryPath (context);
				ClassLoader loader  = context.getClassLoader ();

				Runtime.init (
						language,
						apks,
						getNativeLibraryPath (runtimePackage),
						new String[]{
							filesDir,
							cacheDir,
							dataDir,
						},
						loader,
						new java.io.File (
							android.os.Environment.getExternalStorageDirectory (),
							"Android/data/" + context.getPackageName () + "/files/.__override__").getAbsolutePath (),
						MonoPackageManager_Resources.Assemblies,
						context.getPackageName ());
				
				mono.android.app.ApplicationRegistration.registerApplications ();
				
				initialized = true;
			}
		}
	}

	public static void setContext (Context context)
	{
		// Ignore; vestigial
	}

	static String getNativeLibraryPath (Context context)
	{
	    return getNativeLibraryPath (context.getApplicationInfo ());
	}

	static String getNativeLibraryPath (ApplicationInfo ainfo)
	{
		if (android.os.Build.VERSION.SDK_INT >= 9)
			return ainfo.nativeLibraryDir;
		return ainfo.dataDir + "/lib";
	}

	public static String[] getAssemblies ()
	{
		return MonoPackageManager_Resources.Assemblies;
	}

	public static String[] getDependencies ()
	{
		return MonoPackageManager_Resources.Dependencies;
	}

	public static String getApiPackageName ()
	{
		return MonoPackageManager_Resources.ApiPackageName;
	}
}

class MonoPackageManager_Resources {
	public static final String[] Assemblies = new String[]{
		/* We need to ensure that "Acquaint.Native.Droid.dll" comes first in this list. */
		"Acquaint.Native.Droid.dll",
		"Acquaint.Abstractions.dll",
		"Acquaint.Common.Droid.dll",
		"Acquaint.Data.dll",
		"Acquaint.ModelContracts.dll",
		"Acquaint.Models.dll",
		"Acquaint.Util.dll",
		"Autofac.dll",
		"Autofac.Extras.CommonServiceLocator.dll",
		"FFImageLoading.dll",
		"FFImageLoading.Platform.dll",
		"FFImageLoading.Transformations.dll",
		"HockeySDK.AndroidBindings.dll",
		"HockeySDK.dll",
		"Microsoft.Practices.ServiceLocation.dll",
		"Microsoft.WindowsAzure.Mobile.dll",
		"Microsoft.WindowsAzure.Mobile.Ext.dll",
		"Microsoft.WindowsAzure.Mobile.SQLiteStore.dll",
		"MvvmHelpers.dll",
		"Newtonsoft.Json.dll",
		"PCLStorage.Abstractions.dll",
		"PCLStorage.dll",
		"Plugin.CurrentActivity.dll",
		"Plugin.ExternalMaps.Abstractions.dll",
		"Plugin.ExternalMaps.dll",
		"Plugin.Messaging.Abstractions.dll",
		"Plugin.Messaging.dll",
		"Plugin.Settings.Abstractions.dll",
		"Plugin.Settings.dll",
		"SQLitePCLRaw.batteries_green.dll",
		"SQLitePCLRaw.batteries_v2.dll",
		"SQLitePCLRaw.core.dll",
		"SQLitePCLRaw.lib.e_sqlite3.dll",
		"SQLitePCLRaw.provider.e_sqlite3.dll",
		"System.Net.Http.Extensions.dll",
		"System.Net.Http.Primitives.dll",
		"Xamarin.Android.Support.Animated.Vector.Drawable.dll",
		"Xamarin.Android.Support.Design.dll",
		"Xamarin.Android.Support.v4.dll",
		"Xamarin.Android.Support.v7.AppCompat.dll",
		"Xamarin.Android.Support.v7.RecyclerView.dll",
		"Xamarin.Android.Support.Vector.Drawable.dll",
		"Xamarin.GooglePlayServices.Base.dll",
		"Xamarin.GooglePlayServices.Basement.dll",
		"Xamarin.GooglePlayServices.Maps.dll",
		"System.Runtime.dll",
		"System.Threading.Tasks.dll",
		"System.Diagnostics.Debug.dll",
		"System.Linq.Expressions.dll",
		"System.Collections.dll",
		"System.Linq.dll",
		"System.Threading.dll",
		"System.Reflection.dll",
		"crypto.dll",
		"System.Text.Encoding.dll",
		"System.Runtime.Extensions.dll",
		"System.IO.dll",
		"System.Resources.ResourceManager.dll",
		"System.ObjectModel.dll",
		"System.Text.RegularExpressions.dll",
		"System.Net.Primitives.dll",
		"System.Diagnostics.Tools.dll",
		"System.Globalization.dll",
		"System.Runtime.Serialization.Primitives.dll",
		"System.Reflection.Extensions.dll",
		"System.Linq.Queryable.dll",
		"System.Xml.XDocument.dll",
		"System.Dynamic.Runtime.dll",
		"System.Xml.ReaderWriter.dll",
		"System.Text.Encoding.Extensions.dll",
		"System.Runtime.InteropServices.dll",
		"System.Collections.Concurrent.dll",
	};
	public static final String[] Dependencies = new String[]{
	};
	public static final String ApiPackageName = "Mono.Android.Platform.ApiLevel_24";
}
