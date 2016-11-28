package mono.com.microsoft.band.sensors;


public class BandUVEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandUVEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandUVChanged:(Lcom/microsoft/band/sensors/BandUVEvent;)V:GetOnBandUVChanged_Lcom_microsoft_band_sensors_BandUVEvent_Handler:Microsoft.Band.Sensors.IBandUVEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandUVEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandUVEventListenerImplementor.class, __md_methods);
	}


	public BandUVEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandUVEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandUVEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandUVChanged (com.microsoft.band.sensors.BandUVEvent p0)
	{
		n_onBandUVChanged (p0);
	}

	private native void n_onBandUVChanged (com.microsoft.band.sensors.BandUVEvent p0);

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
