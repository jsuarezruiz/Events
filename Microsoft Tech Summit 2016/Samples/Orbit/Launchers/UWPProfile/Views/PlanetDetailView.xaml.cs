using Microsoft.Toolkit.Uwp.UI.Animations;
using Orbit.Models;
using Orbit.ViewModels;
using Orbit.Views.Base;
using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Media.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Orbit.Views
{
    public sealed partial class PlanetDetailView : PageBase
    {
        #region Fields

        private bool section3FirstTime = true;
        private List<AnimatableSection> _animatableSections = new List<AnimatableSection>();
        private WaveEngine.Adapter.Application _application;
        private DispatcherTimer _videoControlsTimer;

        #endregion

        #region Constructor

        public PlanetDetailView()
        {
            this.InitializeComponent();

            this.Loaded += this.OnLoaded;
            RootElement.ViewChanged += RootElement_ViewChanged;
            Window.Current.SizeChanged += Current_SizeChanged;

            UpdateSize(new Size(Window.Current.Bounds.Width, Window.Current.Bounds.Height));

            _animatableSections.Add(new AnimatableSection(Section1, Section1Animate));
            _animatableSections.Add(new AnimatableSection(Section2, Section2Animate));
            _animatableSections.Add(new AnimatableSection(Section3, Section3Animate));
            _animatableSections.Add(new AnimatableSection(Section4, Section4Animate));
            _animatableSections.Add(new AnimatableSection(VideoSection, VideoSectionAnimate));
        }

        #endregion

        #region Methods

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var item = e.Parameter as Planet;
            if (item != null)
            {
                var vm = DataContext as PlanetDetailViewModel;

                if (vm != null)
                {
                    vm.Planet = item;
                }
            }

            var animationService = ConnectedAnimationService.GetForCurrentView();

            var titleAnimation = animationService.GetAnimation("Title");
            if (titleAnimation != null)
            {
                titleAnimation.TryStart(TitleLine);
            }

            var summaryAnimation = animationService.GetAnimation("Summary");
            if (summaryAnimation != null)
            {
                summaryAnimation.TryStart(SummaryText);
            }

            _application = new GameRenderer(this.SwapChainPanel);

            _application.Initialize();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            _application.Exit();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AnimateSections();
        }

        private void RootElement_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            AnimateSections();
        }

        private void Current_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            UpdateSize(e.Size);
            AnimateSections();
        }

        private void UpdateSize(Size size)
        {
            Section1.Height = size.Height - 100;
        }

        private void AnimateSections()
        {
            foreach (var section in _animatableSections)
            {
                if (IsVisibileToUser(section.Element, RootElement))
                {
                    section.Animate();
                }
            }
        }

        private bool IsVisibileToUser(FrameworkElement element, FrameworkElement container)
        {
            if (element == null || container == null)
                return false;

            if (element.Visibility != Visibility.Visible)
                return false;

            Rect elementBounds = element.TransformToVisual(container).TransformBounds(new Rect(0.0, 0.0, element.ActualWidth, element.ActualHeight));
            Rect containerBounds = new Rect(0.0, 0.0, container.ActualWidth, container.ActualHeight);

            return (elementBounds.Height - (containerBounds.Height - elementBounds.Top)) < (elementBounds.Height / 2);
        }

        private void Section1Animate()
        {
            GalaxyLine.Offset(offsetY: 20, duration: 0).Then().Fade(1).Offset().Start();
        }

        private void Section2Animate()
        {
            Section2Border.Offset(offsetY: 40, duration: 0).Then().Fade(1).Offset().SetDelay(100).Start();
            SunStack.Offset(offsetY: 40, duration: 0).Then().Fade(1).Offset().SetDelay(200).Start();
        }

        private void Section3Animate()
        {
            Section3Image.Offset(offsetY: 40, duration: 0).Then().Fade(1).Offset().Start();
            Section3Text1.Offset(offsetY: 40, duration: 0).Then().Fade(1).Offset().SetDelay(100).Start();
            var anim = Section3Text2.Offset(offsetY: 20, duration: 0).Then().Fade(1).Offset();

            anim.SetDelay(section3FirstTime ? 1000 : 200);
            section3FirstTime = false;

            anim.Start();
        }

        private void Section4Animate()
        {
            SwapChainPanel.Offset(offsetY: 20, duration: 0).Then().Fade(1).Offset().Start();
        }

        private void VideoSectionAnimate()
        {
            VideoButton.Offset(offsetY: 40, duration: 0).Then().Fade(1).Offset().Start();
            VideoDescription.Offset(offsetY: 40, duration: 0).Then().Fade(1).Offset().Start();
        }

        #endregion

        #region Player

        private void VideoButton_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Source = MediaSource.CreateFromUri(
                new Uri("http://s3.amazonaws.com/akamai.netstorage/HD_downloads/earth_night_rotate_1080.mov"));

            VideoPlayer.Visibility = Visibility.Visible;
            VideoPlayerShadow.Visibility = Visibility.Visible;

            VideoPlayer.AutoPlay = true;
            VideoPlayer.MediaPlayer.CurrentStateChanged += MediaPlayer_CurrentStateChanged;
            VideoPlayer.TransportControls.IsFullWindowButtonVisible = false;
            VideoPlayer.TransportControls.IsFastForwardButtonVisible = true;
            VideoPlayer.TransportControls.IsFastForwardEnabled = true;
            VideoPlayer.TransportControls.IsFastRewindButtonVisible = true;
            VideoPlayer.TransportControls.IsFastRewindEnabled = true;

            VideoTransportControls.Visibility = Visibility.Visible;

            (sender as Button).Fade(0).Start();
            VideoPlayer.Fade(1).Start();
            VideoTransportControls.Fade(1).SetDelay(200).Start();

            Section3.XYFocusDown = PlayButton;
            Section4.XYFocusUp = PlayButton;

            (sender as Button).IsEnabled = false;
        }

        private void MediaPlayer_CurrentStateChanged(Windows.Media.Playback.MediaPlayer sender, object args)
        {
            var t = PlayButton.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                switch (sender.PlaybackSession.PlaybackState)
                {
                    case Windows.Media.Playback.MediaPlaybackState.Playing:
                        PlayPauseIcon.Symbol = Symbol.Pause;
                        PlayButton.IsEnabled = true;
                        break;
                    case Windows.Media.Playback.MediaPlaybackState.Paused:
                        PlayPauseIcon.Symbol = Symbol.Play;
                        PlayButton.IsEnabled = true;
                        break;
                    default:
                        PlayButton.IsEnabled = true;
                        break;
                }
            });
        }
        private void VideoTransportControls_GotFocus(object sender, RoutedEventArgs e)
        {
            VideoTransportControls.Fade(1).Start();

            if (_videoControlsTimer == null)
            {
                _videoControlsTimer = new DispatcherTimer();
                _videoControlsTimer.Tick += VideoControlsTimer_Tick;
                _videoControlsTimer.Interval = TimeSpan.FromSeconds(2);
            }

            _videoControlsTimer.Start();
        }

        private void VideoControlsTimer_Tick(object sender, object e)
        {
            VideoTransportControls.Fade(0).Start();
        }

        private void VideoTransportControls_LostFocus(object sender, RoutedEventArgs e)
        {
            VideoTransportControls.Fade(0).Start();

            if (_videoControlsTimer != null)
            {
                _videoControlsTimer.Stop();
            }
        }

        private void PlayButton_Clicked(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (VideoPlayer.MediaPlayer.PlaybackSession.PlaybackState == Windows.Media.Playback.MediaPlaybackState.Playing)
            {
                VideoPlayer.MediaPlayer.Pause();
            }
            else if (VideoPlayer.MediaPlayer.PlaybackSession.PlaybackState == Windows.Media.Playback.MediaPlaybackState.Paused)
            {
                VideoPlayer.MediaPlayer.Play();
            }
        }

        private void FullScreenButton_Clicked(object sender, RoutedEventArgs e)
        {
            VideoPlayer.AreTransportControlsEnabled = true;
            VideoPlayer.IsFullWindow = true;
        }

        private void VideoTransportControls_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            VideoTransportControls.Fade(1).Start();
        }

        private void VideoTransportControls_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            VideoTransportControls.Fade(0).Start();
        }

        #endregion
    }
}
