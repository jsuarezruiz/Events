package md55aa83cb61847c4249bb0dd44fd0a56e6;


public class BackgroundManager
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.app.Application.ActivityLifecycleCallbacks,
		android.content.ComponentCallbacks2,
		android.content.ComponentCallbacks
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onActivityCreated:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivityCreated_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onActivityDestroyed:(Landroid/app/Activity;)V:GetOnActivityDestroyed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onActivityPaused:(Landroid/app/Activity;)V:GetOnActivityPaused_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onActivityResumed:(Landroid/app/Activity;)V:GetOnActivityResumed_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onActivitySaveInstanceState:(Landroid/app/Activity;Landroid/os/Bundle;)V:GetOnActivitySaveInstanceState_Landroid_app_Activity_Landroid_os_Bundle_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onActivityStarted:(Landroid/app/Activity;)V:GetOnActivityStarted_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onActivityStopped:(Landroid/app/Activity;)V:GetOnActivityStopped_Landroid_app_Activity_Handler:Android.App.Application/IActivityLifecycleCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onTrimMemory:(I)V:GetOnTrimMemory_IHandler:Android.Content.IComponentCallbacks2Invoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onConfigurationChanged:(Landroid/content/res/Configuration;)V:GetOnConfigurationChanged_Landroid_content_res_Configuration_Handler:Android.Content.IComponentCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onLowMemory:()V:GetOnLowMemoryHandler:Android.Content.IComponentCallbacksInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Xamarin.InsightsCore.BackgroundManager, Xamarin.Insights, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null", BackgroundManager.class, __md_methods);
	}


	public BackgroundManager () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BackgroundManager.class)
			mono.android.TypeManager.Activate ("Xamarin.InsightsCore.BackgroundManager, Xamarin.Insights, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public BackgroundManager (android.content.Context p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == BackgroundManager.class)
			mono.android.TypeManager.Activate ("Xamarin.InsightsCore.BackgroundManager, Xamarin.Insights, Version=1.10.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onActivityCreated (android.app.Activity p0, android.os.Bundle p1)
	{
		n_onActivityCreated (p0, p1);
	}

	private native void n_onActivityCreated (android.app.Activity p0, android.os.Bundle p1);


	public void onActivityDestroyed (android.app.Activity p0)
	{
		n_onActivityDestroyed (p0);
	}

	private native void n_onActivityDestroyed (android.app.Activity p0);


	public void onActivityPaused (android.app.Activity p0)
	{
		n_onActivityPaused (p0);
	}

	private native void n_onActivityPaused (android.app.Activity p0);


	public void onActivityResumed (android.app.Activity p0)
	{
		n_onActivityResumed (p0);
	}

	private native void n_onActivityResumed (android.app.Activity p0);


	public void onActivitySaveInstanceState (android.app.Activity p0, android.os.Bundle p1)
	{
		n_onActivitySaveInstanceState (p0, p1);
	}

	private native void n_onActivitySaveInstanceState (android.app.Activity p0, android.os.Bundle p1);


	public void onActivityStarted (android.app.Activity p0)
	{
		n_onActivityStarted (p0);
	}

	private native void n_onActivityStarted (android.app.Activity p0);


	public void onActivityStopped (android.app.Activity p0)
	{
		n_onActivityStopped (p0);
	}

	private native void n_onActivityStopped (android.app.Activity p0);


	public void onTrimMemory (int p0)
	{
		n_onTrimMemory (p0);
	}

	private native void n_onTrimMemory (int p0);


	public void onConfigurationChanged (android.content.res.Configuration p0)
	{
		n_onConfigurationChanged (p0);
	}

	private native void n_onConfigurationChanged (android.content.res.Configuration p0);


	public void onLowMemory ()
	{
		n_onLowMemory ();
	}

	private native void n_onLowMemory ();

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
