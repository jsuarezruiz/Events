#region Using Statements
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveFrogger.Scenes;
using WaveFrogger.Services;
#endregion

namespace WaveFrogger
{
    public class Game : WaveEngine.Framework.Game
    {
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);

            WaveServices.RegisterService(new ScoreService());
            WaveServices.RegisterService(new AnimationService());
            WaveServices.RegisterService(new AudioService());

            ScreenContext screenContext = new ScreenContext(new InitialScene());
            WaveServices.ScreenContextManager.To(screenContext);
        }
    }
}
