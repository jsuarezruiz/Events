package mono.com.microsoft.band.sensors;


public class BandHeartRateEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandHeartRateEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandHeartRateChanged:(Lcom/microsoft/band/sensors/BandHeartRateEvent;)V:GetOnBandHeartRateChanged_Lcom_microsoft_band_sensors_BandHeartRateEvent_Handler:Microsoft.Band.Sensors.IBandHeartRateEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandHeartRateEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandHeartRateEventListenerImplementor.class, __md_methods);
	}


	public BandHeartRateEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandHeartRateEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandHeartRateEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandHeartRateChanged (com.microsoft.band.sensors.BandHeartRateEvent p0)
	{
		n_onBandHeartRateChanged (p0);
	}

	private native void n_onBandHeartRateChanged (com.microsoft.band.sensors.BandHeartRateEvent p0);

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
