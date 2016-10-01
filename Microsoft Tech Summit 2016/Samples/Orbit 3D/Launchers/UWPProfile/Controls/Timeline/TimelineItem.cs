using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Orbit.Controls
{
    public sealed class TimelineItem : ContentControl
    {
        public TimelineItem()
        {
            this.DefaultStyleKey = typeof(TimelineItem);
        }

        public bool IsActionable
        {
            get { return (bool)GetValue(IsActionableProperty); }
            set { SetValue(IsActionableProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsActionable.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActionableProperty =
            DependencyProperty.Register("IsActionable", typeof(bool), typeof(TimelineItem), new PropertyMetadata(true));

        public bool IsInFocus
        {
            get { return (bool)GetValue(IsInFocusProperty); }
            set
            {
                if (value == IsInFocus)
                {
                    return;
                }

                if (value)
                {
                    ElementSoundPlayer.Play(ElementSoundKind.Focus);
                    ItemGotFocus?.Invoke(this, null);
                }
                else
                {
                    ItemLostFocus?.Invoke(this, null);
                }

                SetValue(IsInFocusProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for IsInFocus.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsInFocusProperty =
            DependencyProperty.Register("IsInFocus", typeof(bool), typeof(TimelineItem), new PropertyMetadata(false));

        public event EventHandler ItemGotFocus;
        public event EventHandler ItemLostFocus;
    }
}
