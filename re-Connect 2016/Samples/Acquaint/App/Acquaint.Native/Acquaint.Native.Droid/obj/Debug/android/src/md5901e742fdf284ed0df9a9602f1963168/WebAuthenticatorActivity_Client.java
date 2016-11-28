package md5901e742fdf284ed0df9a9602f1963168;


public class WebAuthenticatorActivity_Client
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_shouldOverrideUrlLoading:(Landroid/webkit/WebView;Ljava/lang/String;)Z:GetShouldOverrideUrlLoading_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"n_onPageStarted:(Landroid/webkit/WebView;Ljava/lang/String;Landroid/graphics/Bitmap;)V:GetOnPageStarted_Landroid_webkit_WebView_Ljava_lang_String_Landroid_graphics_Bitmap_Handler\n" +
			"n_onPageFinished:(Landroid/webkit/WebView;Ljava/lang/String;)V:GetOnPageFinished_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"n_onReceivedSslError:(Landroid/webkit/WebView;Landroid/webkit/SslErrorHandler;Landroid/net/http/SslError;)V:GetOnReceivedSslError_Landroid_webkit_WebView_Landroid_webkit_SslErrorHandler_Landroid_net_http_SslError_Handler\n" +
			"";
		mono.android.Runtime.register ("Xamarin.Auth._MobileServices.WebAuthenticatorActivity+Client, Microsoft.WindowsAzure.Mobile.Ext, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", WebAuthenticatorActivity_Client.class, __md_methods);
	}


	public WebAuthenticatorActivity_Client () throws java.lang.Throwable
	{
		super ();
		if (getClass () == WebAuthenticatorActivity_Client.class)
			mono.android.TypeManager.Activate ("Xamarin.Auth._MobileServices.WebAuthenticatorActivity+Client, Microsoft.WindowsAzure.Mobile.Ext, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "", this, new java.lang.Object[] {  });
	}

	public WebAuthenticatorActivity_Client (md5901e742fdf284ed0df9a9602f1963168.WebAuthenticatorActivity p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == WebAuthenticatorActivity_Client.class)
			mono.android.TypeManager.Activate ("Xamarin.Auth._MobileServices.WebAuthenticatorActivity+Client, Microsoft.WindowsAzure.Mobile.Ext, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", "Xamarin.Auth._MobileServices.WebAuthenticatorActivity, Microsoft.WindowsAzure.Mobile.Ext, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35", this, new java.lang.Object[] { p0 });
	}


	public boolean shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1)
	{
		return n_shouldOverrideUrlLoading (p0, p1);
	}

	private native boolean n_shouldOverrideUrlLoading (android.webkit.WebView p0, java.lang.String p1);


	public void onPageStarted (android.webkit.WebView p0, java.lang.String p1, android.graphics.Bitmap p2)
	{
		n_onPageStarted (p0, p1, p2);
	}

	private native void n_onPageStarted (android.webkit.WebView p0, java.lang.String p1, android.graphics.Bitmap p2);


	public void onPageFinished (android.webkit.WebView p0, java.lang.String p1)
	{
		n_onPageFinished (p0, p1);
	}

	private native void n_onPageFinished (android.webkit.WebView p0, java.lang.String p1);


	public void onReceivedSslError (android.webkit.WebView p0, android.webkit.SslErrorHandler p1, android.net.http.SslError p2)
	{
		n_onReceivedSslError (p0, p1, p2);
	}

	private native void n_onReceivedSslError (android.webkit.WebView p0, android.webkit.SslErrorHandler p1, android.net.http.SslError p2);

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
