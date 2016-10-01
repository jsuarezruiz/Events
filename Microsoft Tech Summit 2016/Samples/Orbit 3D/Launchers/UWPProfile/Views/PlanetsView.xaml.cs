using Microsoft.Toolkit.Uwp.UI;
using Microsoft.Toolkit.Uwp.UI.Animations;
using Orbit.Controls;
using Orbit.Helpers;
using Orbit.Helpers.Composition;
using Orbit.Helpers.Composition.ImageLoader;
using Orbit.Models;
using Orbit.ViewModels;
using Orbit.Views.Base;
using System;
using System.Linq;
using Windows.UI.Composition;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;

namespace Orbit.Views
{
    public sealed partial class PlanetsView : PageBase
    {
        #region Fields

        private ListViewItem _previousFocus;
        private bool _firstRun = true;

        #endregion

        #region Constructor

        public PlanetsView()
        {
            this.InitializeComponent();

            this.Loaded += PlanetsView_Loaded;
        }

        #endregion

        #region Methods

        private async void PlanetsView_Loaded(object sender, RoutedEventArgs e)
        {
            if (PlatformHelper.IsXbox() && _firstRun)
            {
                _firstRun = false;

                if (Timeline.TimelinePanel.Margin.Left != -180)
                    Timeline.TimelinePanel.Margin = new Thickness(-180, 0, 0, 0);

                this.SizeChanged += PlanetsView_SizeChanged;
            }
            else
            {
                var vm = DataContext as PlanetsViewModel;

                if (vm != null)
                {
                    await RootView.Current.UpdateBackground(vm.Planets.First().HeroImage, 0);
                }
            }

            if (PlatformHelper.IsXbox())
            {
                Timeline.Focus(FocusState.Programmatic);
                UpdateTimelineItemsSize();
            }
        }

        private void Timeline_Loaded(object sender, RoutedEventArgs e)
        {
            var timeline = sender as Controls.Timeline;

            if( timeline != null)
            {
                var vm = DataContext as PlanetsViewModel;
                timeline.ItemsSource = vm.Planets;
            }
        }

        private void PlanetsView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTimelineItemsSize();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            var shadow = (sender as FrameworkElement).FindChildren<Helpers.Composition.CompositionShadow>().FirstOrDefault();
            var image = (sender as FrameworkElement).FindChildren<CompositionImage>().FirstOrDefault();

            if (shadow == null || image == null)
                return;

            SetupCompositionImage(image, shadow);
        }

        private void Template_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateTemplateContentVisibility(sender);
        }

        private void Template_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateTemplateContentVisibility(sender);
        }

        private void SetupCompositionImage(CompositionImage image, Helpers.Composition.CompositionShadow shadow)
        {
            try
            {
                var imageVisual = image.SpriteVisual;
                var compositor = imageVisual.Compositor;

                var imageLoader = ImageLoaderFactory.CreateImageLoader(compositor);
                var imageMaskSurface = imageLoader.CreateManagedSurfaceFromUri(new Uri("ms-appx:///Helpers/Composition/CircleMask.png"));

                var mask = compositor.CreateSurfaceBrush();
                mask.Surface = imageMaskSurface.Surface;

                var source = image.SurfaceBrush as CompositionSurfaceBrush;

                var maskBrush = compositor.CreateMaskBrush();
                maskBrush.Mask = mask;
                maskBrush.Source = source;

                image.Brush = maskBrush;
                shadow.Mask = maskBrush.Mask;
            }
            catch
            {

            }
        }

        private void UpdateTemplateContentVisibility(object sender)
        {
            var grid = sender as Grid;
            var diameterStack = grid.FindName("DiameterStack") as StackPanel;
            var temperatureStack = grid.FindName("TemperatureStack") as StackPanel;

            var width = Window.Current.Bounds.Width;

            if (width > 1000)
            {
                temperatureStack.Visibility = Visibility.Visible;
                diameterStack.Visibility = Visibility.Visible;
            }
            else if (width > 700)
            {
                temperatureStack.Visibility = Visibility.Collapsed;
                diameterStack.Visibility = Visibility.Visible;
            }
            else
            {
                temperatureStack.Visibility = Visibility.Collapsed;
                diameterStack.Visibility = Visibility.Collapsed;
            }
        }

        private void UpdateTimelineItemsSize()
        {
            var storyItems = this.FindChildren<TimelineStory>();

            bool isSnapped = Window.Current.Bounds.Width < 960;

            foreach (var storyItem in storyItems)
            {
                storyItem.Width = isSnapped ? 600 : 750;
            }
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var vm = DataContext as PlanetsViewModel;

            if (vm != null)
            {
                var index = vm.Planets.IndexOf(e.ClickedItem as Planet);
                var children = (sender as ListView).FindChildren<ListViewItem>();

                var listViewItem = children.ElementAt(index);

                var titleLine = listViewItem.FindDescendantByName("TitleLine");
                var summaryLine = listViewItem.FindDescendantByName("SummaryLine");
                var sunStack = listViewItem.FindDescendantByName("SunStack");


                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Title", titleLine);
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Summary", summaryLine);
                ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("Sun", sunStack);

                var story = e.ClickedItem as Planet;

                await RootView.Current.UpdateBackground(story.HeroImage, 0);
                Frame.Navigate(typeof(PlanetDetailView), story);
            }
        }

        #endregion

        #region Xbox

        private void Timeline_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.OriginalKey == Windows.System.VirtualKey.GamepadDPadLeft ||
                e.OriginalKey == Windows.System.VirtualKey.GamepadLeftThumbstickLeft)
            {
                if (_previousFocus != null)
                {
                    _previousFocus?.Focus(FocusState.Keyboard);
                }

                e.Handled = true;
            }
        }

        private void Timeline_GotFocus(object sender, RoutedEventArgs e)
        {
            Timeline.Fade(1).Start();
        }

        private void Timeline_LostFocus(object sender, RoutedEventArgs e)
        {
            Timeline.Fade(0.6f).Start();
        }

        private async void TimelineItem_ItemGotFocus(object sender, EventArgs e)
        {
            var item = sender as TimelineItem;
            var stories = item.FindChildren<TimelineStory>();

            if (stories.Count() == 0)
                return;

            var storie = stories.First();
            storie.AnimateFocus();

            await RootView.Current.UpdateBackground(storie.Item.HeroImage, Timeline.TimelinePanel.TopItemIndex);
        }

        private void TimelineItem_ItemLostFocus(object sender, EventArgs e)
        {
            var item = sender as TimelineItem;
            var stories = item.FindChildren<TimelineStory>();

            if (stories.Count() == 0)
                return;

            stories.First().AnimateFocusLost();
        }

        private void Timeline_ItemInvoked(object sender, TimelineItemInvokedEventArgs e)
        {
            var container = e.Item.Content as TimelineStory;

            if (container != null)
            {
                container.PrepareForNavigation();
            }

            Planet item = e.Item.DataContext as Planet;

            Frame.Navigate(typeof(PlanetDetailView), item);
        }

        #endregion
    }
}