#region Using Statements
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Hololens.Interaction;
using WaveEngine.Hololens.SpatialMapping;
using WaveEngine.Hololens.Speech;
#endregion

namespace HoloPlanets
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.MyScene);

            WaveServices.RegisterService(new SpatialMappingService() { TrianglesPerCubicMeter = 500, ObtainNormals = false });
            WaveServices.RegisterService(new SpatialInputService());
            WaveServices.RegisterService(new KeywordRecognizerService());
        }
    }
}
