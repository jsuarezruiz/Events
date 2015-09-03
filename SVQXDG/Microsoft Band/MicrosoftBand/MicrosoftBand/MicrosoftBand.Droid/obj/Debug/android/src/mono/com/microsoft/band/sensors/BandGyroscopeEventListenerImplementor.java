package mono.com.microsoft.band.sensors;


public class BandGyroscopeEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandGyroscopeEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandGyroscopeChanged:(Lcom/microsoft/band/sensors/BandGyroscopeEvent;)V:GetOnBandGyroscopeChanged_Lcom_microsoft_band_sensors_BandGyroscopeEvent_Handler:Microsoft.Band.Sensors.IBandGyroscopeEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandGyroscopeEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandGyroscopeEventListenerImplementor.class, __md_methods);
	}


	public BandGyroscopeEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandGyroscopeEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandGyroscopeEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandGyroscopeChanged (com.microsoft.band.sensors.BandGyroscopeEvent p0)
	{
		n_onBandGyroscopeChanged (p0);
	}

	private native void n_onBandGyroscopeChanged (com.microsoft.band.sensors.BandGyroscopeEvent p0);

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
