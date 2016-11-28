using System;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;
using WaveEngine.Components.Transitions;
using WaveFrogger.Services;
using WaveEngine.Components.Toolkit;
using WaveEngine.Components.Gestures;
using WaveEngine.Framework.Animation;

namespace WaveFrogger.Scenes
{
    public class InitialScene : Scene
    {
        private TextComponent maxScore;
        private TextComponent currentScore;
        private Entity loadingEntity;

        protected override void CreateScene()
        {
            this.Load(WaveContent.Scenes.InitialScene);

            var animationService = WaveServices.GetService<AnimationService>();

            var gui = this.EntityManager.Find<Entity>("GUI");
            this.maxScore = gui.FindChild("maxscore").FindComponent<TextComponent>();
            this.currentScore = gui.FindChild("currentscore").FindComponent<TextComponent>();
            this.loadingEntity = gui.FindChild("loading");

            var tapToPlayEntity = gui.FindChild("taptoplay");
            var tapToPlay = tapToPlayEntity.FindComponent<TouchGestures>();
            tapToPlay.TouchTap += this.TapToPlayTouchTap;

            animationService.CreatePulseAnimation(tapToPlayEntity, 350).Run();

            var player = this.EntityManager.Find("sceneEntity.player");
            animationService.CreateIdleAnimation(player, 175).Run();

            WaveServices.GetService<AudioService>().Play(Audio.Music.MrJellyRolls_mp3, 0.3f);
        }

        int count;

        private void TapToPlayTouchTap(object sender, GestureEventArgs e)
        {
            if (count == 0)
            {
                count++;

                GameScene gameScene = null;

                ScreenTransition transition = new CoverTransition(TimeSpan.FromMilliseconds(500), CoverTransition.EffectOptions.FromTop);
                transition.EaseFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };

                this.loadingEntity.IsVisible = true;
                var animationService = WaveServices.GetService<AnimationService>();
                animationService.CreatePulseAnimation(this.loadingEntity, 1000).Run();

                WaveServices.TaskScheduler.CreateTask(() =>
                {
                    gameScene = new GameScene();
                    gameScene.Initialize(WaveServices.GraphicsDevice);
                })
                .ContinueWith(() =>
                {
                    WaveServices.ScreenContextManager.To(new ScreenContext(gameScene), transition);
                });
            }
        }

        protected override void Start()
        {
            base.Start();

            var scoreService = WaveServices.GetService<ScoreService>();
            this.maxScore.Text = scoreService.MaxScore.ToString();
            this.currentScore.Text = scoreService.CurrentScore.ToString();
        }
    }
}
