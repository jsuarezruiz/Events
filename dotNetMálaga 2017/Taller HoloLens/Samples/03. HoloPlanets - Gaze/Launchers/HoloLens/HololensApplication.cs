using System;
using WaveEngine.Hololens;

namespace HoloPlanets
{
    internal class HololensApplication : BaseHololensApplication
    {
        private HoloPlanets.Game game;

        public HololensApplication() : base()
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            this.game = new Game();
            this.game.Initialize(this);
        }

        public override void Update(TimeSpan gameTime)
        {
            this.game.UpdateFrame(gameTime);
        }

        public override void Draw(TimeSpan gameTime)
        {
            this.game.DrawFrame(gameTime);
        }

        public override void OnResuming()
        {
            base.OnResuming();

            this.game.OnActivated();
        }

        public override void OnSuspending()
        {
            base.OnSuspending();

            this.game.OnDeactivated();
        }
    }
}
