package md5901e742fdf284ed0df9a9602f1963168;


public class AndroidAccountStore_SecretAccount
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		javax.crypto.SecretKey,
		java.security.Key,
		java.io.Serializable
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_getAlgorithm:()Ljava/lang/String;:GetGetAlgorithmHandler:Java.Security.IKeyInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getFormat:()Ljava/lang/String;:GetGetFormatHandler:Java.Security.IKeyInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_getEncoded:()[B:GetGetEncodedHandler:Java.Security.IKeyInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Auth._MobileServices.AndroidAccountStore+SecretAccount, Microsoft.WindowsAzure.Mobile.Ext, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", AndroidAccountStore_SecretAccount.class, __md_methods);
	}


	public AndroidAccountStore_SecretAccount () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AndroidAccountStore_SecretAccount.class)
			mono.android.TypeManager.Activate ("Xamarin.Auth._MobileServices.AndroidAccountStore+SecretAccount, Microsoft.WindowsAzure.Mobile.Ext, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "", this, new java.lang.Object[] {  });
	}


	public java.lang.String getAlgorithm ()
	{
		return n_getAlgorithm ();
	}

	private native java.lang.String n_getAlgorithm ();


	public java.lang.String getFormat ()
	{
		return n_getFormat ();
	}

	private native java.lang.String n_getFormat ();


	public byte[] getEncoded ()
	{
		return n_getEncoded ();
	}

	private native byte[] n_getEncoded ();

	private java.util.ArrayList refList;
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
