package mono.com.microsoft.band.sensors;


public class BandAccelerometerEventListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.microsoft.band.sensors.BandAccelerometerEventListener
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onBandAccelerometerChanged:(Lcom/microsoft/band/sensors/BandAccelerometerEvent;)V:GetOnBandAccelerometerChanged_Lcom_microsoft_band_sensors_BandAccelerometerEvent_Handler:Microsoft.Band.Sensors.IBandAccelerometerEventListenerInvoker, Microsoft.Band.Android\n" +
			"";
		mono.android.Runtime.register ("Microsoft.Band.Sensors.IBandAccelerometerEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", BandAccelerometerEventListenerImplementor.class, __md_methods);
	}


	public BandAccelerometerEventListenerImplementor () throws java.lang.Throwable
	{
		super ();
		if (getClass () == BandAccelerometerEventListenerImplementor.class)
			mono.android.TypeManager.Activate ("Microsoft.Band.Sensors.IBandAccelerometerEventListenerImplementor, Microsoft.Band.Android, Version=1.3.10622.3, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onBandAccelerometerChanged (com.microsoft.band.sensors.BandAccelerometerEvent p0)
	{
		n_onBandAccelerometerChanged (p0);
	}

	private native void n_onBandAccelerometerChanged (com.microsoft.band.sensors.BandAccelerometerEvent p0);

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
