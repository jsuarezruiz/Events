using Xamarin.Forms;

namespace dotnetspain2015.CustomControls
{
    /// <summary>
    /// Class SeparatorControl.
    /// </summary>
    public class SeparatorControl : View
    {
        /**
         * Orientation property
         */
        /// <summary>
        /// The orientation property
        /// </summary>
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create("Orientation", typeof(SeparatorOrientation), typeof(SeparatorControl), SeparatorOrientation.Horizontal, BindingMode.OneWay, null, null, null, null);

        /**
         * Orientation of the separator. Only
         */
        /// <summary>
        /// Gets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
        public SeparatorOrientation Orientation
        {
            get
            {
                return (SeparatorOrientation)base.GetValue(SeparatorControl.OrientationProperty);
            }

            private set
            {
                base.SetValue(SeparatorControl.OrientationProperty, value);
            }
        }

        /**
         * Color property
         */
        /// <summary>
        /// The color property
        /// </summary>
        public static readonly BindableProperty ColorProperty =
            BindableProperty.Create("Color", typeof(Color), typeof(SeparatorControl), Color.Default, BindingMode.OneWay, null, null, null, null);

        /**
         * Color of the separator. Black is a default color
         */
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color
        {
            get
            {
                return (Color)base.GetValue(SeparatorControl.ColorProperty);
            }
            set
            {
                base.SetValue(SeparatorControl.ColorProperty, value);
            }
        }

        /**
         * Thickness property
         */
        /// <summary>
        /// The thickness property
        /// </summary>
        public static readonly BindableProperty ThicknessProperty =
            BindableProperty.Create("Thickness", typeof(double), typeof(SeparatorControl), (double)1, BindingMode.OneWay, null, null, null, null);


        /**
         * How thick should the separator be. Default is 1
         */

        /// <summary>
        /// Gets or sets the thickness.
        /// </summary>
        /// <value>The thickness.</value>
        public double Thickness
        {
            get
            {
                return (double)base.GetValue(SeparatorControl.ThicknessProperty);
            }
            set
            {
                base.SetValue(SeparatorControl.ThicknessProperty, value);
            }
        }


        /**
         * Stroke type property
         */
        /// <summary>
        /// The stroke type property
        /// </summary>
        public static readonly BindableProperty StrokeTypeProperty =
            BindableProperty.Create("StrokeType", typeof(StrokeType), typeof(SeparatorControl), StrokeType.Solid, BindingMode.OneWay, null, null, null, null);

        /**
         * Stroke style of the separator. Default is Solid.
         */
        /// <summary>
        /// Gets or sets the type of the stroke.
        /// </summary>
        /// <value>The type of the stroke.</value>
        public StrokeType StrokeType
        {
            get
            {
                return (StrokeType)base.GetValue(SeparatorControl.StrokeTypeProperty);
            }
            set
            {
                base.SetValue(SeparatorControl.StrokeTypeProperty, value);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeparatorControl"/> class.
        /// </summary>
        public SeparatorControl()
        {
            UpdateRequestedSize();
        }

        /// <summary>
        /// Call this method from a child class to notify that a change happened on a property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        /// <remarks>A <see cref="T:Xamarin.Forms.BindableProperty" /> triggers this by itself. An inheritor only needs to call this for properties without <see cref="T:Xamarin.Forms.BindableProperty" /> as the backend store.</remarks>
        protected override void OnPropertyChanged(string propertyName)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == ThicknessProperty.PropertyName ||
               propertyName == ColorProperty.PropertyName ||
               propertyName == StrokeTypeProperty.PropertyName ||
               propertyName == OrientationProperty.PropertyName)
            {
                UpdateRequestedSize();
            }
        }


        /// <summary>
        /// Updates the size of the requested.
        /// </summary>
        private void UpdateRequestedSize()
        {
            var minSize = Thickness;
            var optimalSize = Thickness;
            if (Orientation == SeparatorOrientation.Horizontal)
            {
                MinimumHeightRequest = minSize;
                HeightRequest = optimalSize;
                HorizontalOptions = LayoutOptions.FillAndExpand;
            }
            else
            {
                MinimumWidthRequest = minSize;
                WidthRequest = optimalSize;
                VerticalOptions = LayoutOptions.FillAndExpand;
            }
        }
    }

    	/// <summary>
	/// Enum StrokeType
	/// </summary>
	public enum StrokeType{
		/// <summary>
		/// The solid
		/// </summary>
		Solid,
		/// <summary>
		/// The dotted
		/// </summary>
		Dotted,
		/// <summary>
		/// The dashed
		/// </summary>
		Dashed
	}
	/// <summary>
	/// Enum SeparatorOrientation
	/// </summary>
	public enum SeparatorOrientation{
		/// <summary>
		/// The vertical
		/// </summary>
		Vertical,
		/// <summary>
		/// The horizontal
		/// </summary>
		Horizontal
	}
}
