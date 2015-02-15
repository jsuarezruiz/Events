package dotnetspain2015.droid.controls;


public class RoundedBoxViewRenderer
	extends xamarin.forms.platform.android.BoxRenderer
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_draw:(Landroid/graphics/Canvas;)V:GetDraw_Landroid_graphics_Canvas_Handler\n" +
			"";
		mono.android.Runtime.register ("dotnetspain2015.Droid.Controls.RoundedBoxViewRenderer, dotnetspain2015.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", RoundedBoxViewRenderer.class, __md_methods);
	}


	public RoundedBoxViewRenderer (android.content.Context p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == RoundedBoxViewRenderer.class)
			mono.android.TypeManager.Activate ("dotnetspain2015.Droid.Controls.RoundedBoxViewRenderer, dotnetspain2015.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public RoundedBoxViewRenderer (android.content.Context p0, android.util.AttributeSet p1) throws java.lang.Throwable
	{
		super (p0, p1);
		if (getClass () == RoundedBoxViewRenderer.class)
			mono.android.TypeManager.Activate ("dotnetspain2015.Droid.Controls.RoundedBoxViewRenderer, dotnetspain2015.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0, p1 });
	}


	public RoundedBoxViewRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2) throws java.lang.Throwable
	{
		super (p0, p1, p2);
		if (getClass () == RoundedBoxViewRenderer.class)
			mono.android.TypeManager.Activate ("dotnetspain2015.Droid.Controls.RoundedBoxViewRenderer, dotnetspain2015.Droid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.Content.Context, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:Android.Util.IAttributeSet, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065:System.Int32, mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public void draw (android.graphics.Canvas p0)
	{
		n_draw (p0);
	}

	private native void n_draw (android.graphics.Canvas p0);

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
