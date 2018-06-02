using System;
using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.OS;
using WaveEngine.Common;
using WaveEngine.Adapter;
using WaveEngine.Common.Input;
using XamarinForms3DCarSample.Helpers;
using Xamarin.Forms;
using DLToolkit.Forms.Controls;

namespace XamarinForms3DCarSample.Droid
{
    [Activity(
        Label = "XamarinForms3DCarSample",
        Icon = "@drawable/icon", MainLauncher = true,
        ScreenOrientation = ScreenOrientation.Landscape,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsApplicationActivity, IAndroidApplication
    {
        private IGame game;
        private GLView view;
        private bool isGameInitialized = false;

        public GLView View
        {
            get
            {
                return view;
            }

            set
            {
                view = value;
            }
        }

        public Activity Activity
        {
            get
            {
                return this;
            }
        }

        public int LayoutId { get; set; }

        private bool fullScreen;

        public IAdapter Adapter
        {
            get
            {
                if (View == null)
                {
                    return null;
                }
                else
                {
                    return View.Adapter;
                }
            }
        }

        public string WindowTitle
        {
            get { return string.Empty; }
        }

        public bool FullScreen
        {
            get
            {
                return fullScreen;
            }

            set
            {
                if (fullScreen != value)
                {
                    fullScreen = value;

                    if (Window != null)
                    {
                        if (value)
                        {
                            Window.AddFlags(WindowManagerFlags.Fullscreen);
                        }
                        else
                        {
                            Window.ClearFlags(WindowManagerFlags.Fullscreen);
                        }
                    }
                }
            }
        }

        public int Width
        {
            get { return Adapter.Width; }
        }

        public int Height
        {
            get { return Adapter.Height; }
        }

        private DisplayOrientation defaultOrientation;

        public DisplayOrientation SupportedOrientations { get; set; }

        public DisplayOrientation DefaultOrientation
        {
            get
            {
                return defaultOrientation;
            }

            set
            {
                if (defaultOrientation != value)
                {
                    defaultOrientation = value;
                }
            }
        }

        public bool SkipDefaultSplash
        {
            get; set;
        }

        public bool IsEditor => false;

        public ExecutionMode ExecutionMode => ExecutionMode.Standalone;

        public void Initialize()
        {
        }

        public void Initialize(IGame theGame)
        {
            MessagingCenter.Subscribe<MyScene>(this, MessengerKeys.SceneInitialized, OnSceneLoaded);

            game = theGame;
            isGameInitialized = false;
        }

        public void Update(TimeSpan elapsedTime)
        {
            if (game != null)
            {
                if (!isGameInitialized)
                {
                    game.Initialize(this);
                    isGameInitialized = true;
                }

                game.UpdateFrame(elapsedTime);
            }
        }

        public void OnSceneLoaded(MyScene scene)
        {
            WaveEngineFacade.Initialize(scene);
        }

        public void Draw(TimeSpan elapsedTime)
        {
            if (game != null)
            {
                game.DrawFrame(elapsedTime);
            }
        }

        public virtual void Exit()
        {
            Finish();
            Java.Lang.JavaSystem.Exit(0);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            this.VolumeControlStream = Android.Media.Stream.Music;

            Window.AddFlags(WindowManagerFlags.Fullscreen);

            Forms.Init(this, savedInstanceState);

            FlowListView.Init();

            LoadApplication(new App());
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (View != null)
            {
                View.Pause();
            }
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (View != null)
            {
                View.Resume();
            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            bool handled = false;

            if (View != null)
            {
                handled = View.OnKeyDown(keyCode, e);
            }

            return handled;
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            bool handled = false;

            if (View != null)
            {
                handled = View.OnKeyUp(keyCode, e);
            }

            return handled;
        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
        }

        public static ScreenOrientation GetNativeOrientation(DisplayOrientation orientation)
        {
            ScreenOrientation nativeOrientation = ScreenOrientation.Sensor;

            switch (orientation)
            {
                case DisplayOrientation.Default:
                case DisplayOrientation.LandscapeLeft:
                case DisplayOrientation.LandscapeRight:
                    nativeOrientation = ScreenOrientation.SensorLandscape;
                    break;
                case DisplayOrientation.Portrait:
                    nativeOrientation = ScreenOrientation.SensorPortrait;
                    break;
            }

            return nativeOrientation;
        }
    }
}