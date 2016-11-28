package mono.com.microsoft.band.sensors;


public class HeartRateConsentListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.HeartRateConsentListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_userAccepted:(Z)V:GetUserAccepted_ZHandler:Microsoft.Band.Sensors.IHeartRateConsentListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IHeartRateConsentListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", HeartRateConsentListenerImplementor.class, __md_methods);
	}


	public HeartRateConsentListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == HeartRateConsentListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IHeartRateConsentListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void userAccepted (boolean p0)
	{
		n_userAccepted (p0);
	}

	private native void n_userAccepted (boolean p0);

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
