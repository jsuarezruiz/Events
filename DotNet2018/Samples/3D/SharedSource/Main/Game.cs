#region Using Statements
using WaveEngine.Common;
using WaveEngine.Framework.Services;
#endregion

namespace XamarinForms3DCarSample
{
    public class Game : WaveEngine.Framework.Game
    {
        private LoadingScene _scene;

        public override void Initialize(IApplication application)
        {
            base.Initialize(application);

            this.Load(WaveContent.GameInfo);

            _scene = new LoadingScene();

            ScreenContext screenContext = new ScreenContext(_scene);
            WaveServices.ScreenContextManager.To(screenContext);
        }
    }
}