package md54ada4fbe4a5955f6151fa282d30cfc48;


public class SelfDisposingBitmapDrawable
	extends android.graphics.drawable.BitmapDrawable
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", SelfDisposingBitmapDrawable.class, __md_methods);
	}


	public SelfDisposingBitmapDrawable () throws java.lang.Throwable
	{
		super ();
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public SelfDisposingBitmapDrawable (android.content.res.Resources p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Res.Resources, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public SelfDisposingBitmapDrawable (android.content.res.Resources p0, android.graphics.Bitmap p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Res.Resources, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Graphics.Bitmap, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public SelfDisposingBitmapDrawable (android.content.res.Resources p0, java.io.InputStream p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Res.Resources, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.IO.Stream, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public SelfDisposingBitmapDrawable (android.content.res.Resources p0, java.lang.String p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Res.Resources, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1 });
	}


	public SelfDisposingBitmapDrawable (android.graphics.Bitmap p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Graphics.Bitmap, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public SelfDisposingBitmapDrawable (java.io.InputStream p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.IO.Stream, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}


	public SelfDisposingBitmapDrawable (java.lang.String p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == SelfDisposingBitmapDrawable.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Drawables.SelfDisposingBitmapDrawable, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "System.String, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0 });
	}

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
