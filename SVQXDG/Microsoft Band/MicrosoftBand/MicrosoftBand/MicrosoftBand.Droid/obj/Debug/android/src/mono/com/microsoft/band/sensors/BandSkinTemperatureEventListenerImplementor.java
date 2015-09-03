package mono.com.microsoft.band.sensors;


public class BandSkinTemperatureEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandSkinTemperatureEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandSkinTemperatureChanged:(Lcom/microsoft/band/sensors/BandSkinTemperatureEvent;)V:GetOnBandSkinTemperatureChanged_Lcom_microsoft_band_sensors_BandSkinTemperatureEvent_Handler:Microsoft.Band.Sensors.IBandSkinTemperatureEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandSkinTemperatureEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandSkinTemperatureEventListenerImplementor.class, __md_methods);
	}


	public BandSkinTemperatureEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandSkinTemperatureEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandSkinTemperatureEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandSkinTemperatureChanged (com.microsoft.band.sensors.BandSkinTemperatureEvent p0)
	{
		n_onBandSkinTemperatureChanged (p0);
	}

	private native void n_onBandSkinTemperatureChanged (com.microsoft.band.sensors.BandSkinTemperatureEvent p0);

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
