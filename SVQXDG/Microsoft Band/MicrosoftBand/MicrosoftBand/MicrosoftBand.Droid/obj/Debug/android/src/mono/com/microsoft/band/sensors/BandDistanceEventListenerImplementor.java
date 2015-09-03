package mono.com.microsoft.band.sensors;


public class BandDistanceEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandDistanceEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandDistanceChanged:(Lcom/microsoft/band/sensors/BandDistanceEvent;)V:GetOnBandDistanceChanged_Lcom_microsoft_band_sensors_BandDistanceEvent_Handler:Microsoft.Band.Sensors.IBandDistanceEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandDistanceEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandDistanceEventListenerImplementor.class, __md_methods);
	}


	public BandDistanceEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandDistanceEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandDistanceEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandDistanceChanged (com.microsoft.band.sensors.BandDistanceEvent p0)
	{
		n_onBandDistanceChanged (p0);
	}

	private native void n_onBandDistanceChanged (com.microsoft.band.sensors.BandDistanceEvent p0);

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
