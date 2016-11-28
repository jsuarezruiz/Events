using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using dotnetspain2015.CustomControls;
using dotnetspain2015.WinPhone.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(RoundedBoxView), typeof(RoundedBoxViewRenderer))]
namespace dotnetspain2015.WinPhone.Controls
{
    public class RoundedBoxViewRenderer : ViewRenderer<RoundedBoxView, Border>
    {
        Border _view;

        protected override void OnElementChanged(ElementChangedEventArgs<RoundedBoxView> e)
        {
            base.OnElementChanged(e);

            var rbv = e.NewElement;
            if (rbv != null)
            {
                _view = new Border
                {
                    Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                        (byte)(rbv.Color.A * 255),
                        (byte)(rbv.Color.R * 255),
                        (byte)(rbv.Color.G * 255),
                        (byte)(rbv.Color.B * 255))),
                    CornerRadius = new CornerRadius((float)rbv.CornerRadius),
                    BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                        (byte)(rbv.Stroke.A * 255),
                        (byte)(rbv.Stroke.R * 255),
                        (byte)(rbv.Stroke.G * 255),
                        (byte)(rbv.Stroke.B * 255))),
                    BorderThickness = new System.Windows.Thickness((float)rbv.StrokeThickness)
                };

                SetNativeControl(_view);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == RoundedBoxView.CornerRadiusProperty.PropertyName)
                _view.CornerRadius = new CornerRadius((float) this.Element.CornerRadius);
            else if (e.PropertyName == RoundedBoxView.StrokeProperty.PropertyName)
                _view.BorderBrush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                    (byte)(Element.Stroke.A * 255),
                    (byte)(Element.Stroke.R * 255),
                    (byte)(Element.Stroke.G * 255),
                    (byte)(Element.Stroke.B * 255)));
            else if (e.PropertyName == RoundedBoxView.StrokeThicknessProperty.PropertyName)
                _view.BorderThickness = new System.Windows.Thickness((float)this.Element.StrokeThickness);
            else if (e.PropertyName == BoxView.ColorProperty.PropertyName)
                _view.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(
                    (byte)(Element.BackgroundColor.A * 255),
                    (byte)(Element.BackgroundColor.R * 255),
                    (byte)(Element.BackgroundColor.G * 255),
                    (byte)(Element.BackgroundColor.B * 255)));
        }
    }
}
