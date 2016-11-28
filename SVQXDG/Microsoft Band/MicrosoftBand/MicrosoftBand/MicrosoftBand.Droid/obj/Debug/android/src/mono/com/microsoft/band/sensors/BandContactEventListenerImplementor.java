package mono.com.microsoft.band.sensors;


public class BandContactEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandContactEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandContactChanged:(Lcom/microsoft/band/sensors/BandContactEvent;)V:GetOnBandContactChanged_Lcom_microsoft_band_sensors_BandContactEvent_Handler:Microsoft.Band.Sensors.IBandContactEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandContactEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandContactEventListenerImplementor.class, __md_methods);
	}


	public BandContactEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandContactEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandContactEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandContactChanged (com.microsoft.band.sensors.BandContactEvent p0)
	{
		n_onBandContactChanged (p0);
	}

	private native void n_onBandContactChanged (com.microsoft.band.sensors.BandContactEvent p0);

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
