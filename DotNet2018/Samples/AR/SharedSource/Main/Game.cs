#region Using Statements
using WaveEngine.Common;
using WaveEngine.Framework.Services;
#endregion

namespace XamarinFormsARCarSample
{
    public class Game : WaveEngine.Framework.Game
    {
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);

            this.Load(WaveContent.GameInfo);

            ScreenContext screenContext = new ScreenContext(new LoadingScene());
            WaveServices.ScreenContextManager.To(screenContext);
        }
    }
}