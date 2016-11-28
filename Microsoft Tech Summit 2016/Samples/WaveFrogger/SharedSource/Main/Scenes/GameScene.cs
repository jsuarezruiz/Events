#region Using Statements
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveFrogger.Services;
#endregion

namespace WaveFrogger.Scenes
{
    public class GameScene : Scene
    {
        // private float previousMusicVolume = 1.0f;

        // private AudioService audioService;

        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.GameScene);

            // this.audioService = WaveServices.GetService<AudioService>();
        }

        protected override void Start()
        {
            base.Start();

            // this.previousMusicVolume = this.audioService.MusicVolume;
            // this.audioService.MusicVolume = 0.2f;
        }

        protected override void End()
        {
            base.End();
            // this.audioService.MusicVolume = this.previousMusicVolume;
        }
    }
}
