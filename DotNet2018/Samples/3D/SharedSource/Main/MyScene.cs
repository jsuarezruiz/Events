#region Using Statements
using WaveEngine.Framework;
#endregion

namespace XamarinForms3DCarSample
{
    public class MyScene : Scene
    {
        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.MyScene);           
        }
    }
}
