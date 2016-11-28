package md5413511a2045ce8736c74fdfc5bb05c1c;


public class ManagedImageView
	extends android.widget.ImageView
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_setImageDrawable:(Landroid/graphics/drawable/Drawable;)V:GetSetImageDrawable_Landroid_graphics_drawable_Drawable_Handler\n" +
			"n_setImageResource:(I)V:GetSetImageResource_IHandler\n" +
			"n_setImageURI:(Landroid/net/Uri;)V:GetSetImageURI_Landroid_net_Uri_Handler\n" +
			"";
		mono.android.Runtime.register ("FFImageLoading.Views.ManagedImageView, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", ManagedImageView.class, __md_methods);
	}


	public ManagedImageView (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == ManagedImageView.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Views.ManagedImageView, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public ManagedImageView (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == ManagedImageView.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Views.ManagedImageView, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public ManagedImageView (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == ManagedImageView.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Views.ManagedImageView, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public ManagedImageView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3) throws java.lang.Throwable
	{
		super (p0, p1, p2, p3);
		if (getClass () == ManagedImageView.class)
			mono.android.TypeManager.Activate ("FFImageLoading.Views.ManagedImageView, FFImageLoading.Platform, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void setImageDrawable (android.graphics.drawable.Drawable p0)
	{
		n_setImageDrawable (p0);
	}

	private native void n_setImageDrawable (android.graphics.drawable.Drawable p0);


	public void setImageResource (int p0)
	{
		n_setImageResource (p0);
	}

	private native void n_setImageResource (int p0);


	public void setImageURI (android.net.Uri p0)
	{
		n_setImageURI (p0);
	}

	private native void n_setImageURI (android.net.Uri p0);

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
