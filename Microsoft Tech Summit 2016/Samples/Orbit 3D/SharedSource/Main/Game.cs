#region Using Statements
using Orbit.Orbit;
using System;
using WaveEngine.Common;
using WaveEngine.Common.Graphics;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
#endregion

namespace Orbit
{
    public class Game : WaveEngine.Framework.Game
    {
        private MyScene _scene;
        public override void Initialize(IApplication application)
        {
            base.Initialize(application);

            _scene = new MyScene();
            ScreenContext screenContext = new ScreenContext(_scene);	
			WaveServices.ScreenContextManager.To(screenContext);
        }

        public void FollowPlanet(string planet)
        {
            var cameraEntity = _scene.EntityManager.Find("defaultCamera3D");
            var cameraBehavior = cameraEntity?.FindComponent<CameraBehavior>();
            cameraBehavior.Planet = planet;
        }
    }
}
