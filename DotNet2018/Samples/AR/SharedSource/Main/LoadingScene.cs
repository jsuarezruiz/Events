using System;
using WaveEngine.Components.Transitions;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
#if ANDROID
using Xamarin.Forms;
#endif

namespace XamarinFormsARCarSample
{
    public class LoadingScene : Scene
    {
        private MyScene myScene;

        protected override void CreateScene()
        {
            Load(WaveContent.Scenes.Loading);
        }

        protected override void Start()
        {
            base.Start();

            myScene = new MyScene();
            myScene.Initialize(WaveServices.GraphicsDevice);

            WaveServices.ScreenContextManager.To(new ScreenContext(myScene), new CrossFadeTransition(TimeSpan.FromSeconds(1)));
#if ANDROID
            MessagingCenter.Send(myScene, MessengerKeys.SceneInitialized);
#endif
        }
    }
}
