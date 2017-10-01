#region Using Statements
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
#endregion

namespace HoloPlanets
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.MyScene);

            WaveServices.RegisterService(new WaveEngine.Hololens.SpatialMapping.SpatialMappingService() { TrianglesPerCubicMeter = 500, ObtainNormals = false });
            WaveServices.RegisterService(new WaveEngine.Hololens.Interaction.SpatialInputService());
            WaveServices.RegisterService(new WaveEngine.Hololens.Speech.KeywordRecognizerService());
        }
    }
}
