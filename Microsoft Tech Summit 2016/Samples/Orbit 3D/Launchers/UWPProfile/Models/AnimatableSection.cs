using System;
using Windows.UI.Xaml;

namespace Orbit.Models
{
    internal class AnimatableSection
    {
        public FrameworkElement Element { get; set; }
        private Action Animation { get; set; }

        private bool _alreadyAnimated = false;

        public AnimatableSection(FrameworkElement element, Action animation)
        {
            Element = element;
            Animation = animation;
        }

        public void Animate()
        {
            if (_alreadyAnimated) return;

            _alreadyAnimated = true;
            Animation?.Invoke();
        }
    }
}
