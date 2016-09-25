using System;
using System.Runtime.Serialization;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Services;

namespace WaveFrogger.Behaviors
{
    [DataContract]
    public class PlayerInputBehavior : Behavior
    {
        [RequiredComponent]
        private PlayerBehavior playerBehavior = null;

        private bool tapping;

        private Vector2 initialTapPosition;
        private Vector2 finalTapPosition;

        public bool IgnoreInput { get; internal set; }

        private Vector2 touchPosition;

        protected override void Update(TimeSpan gameTime)
        {
            if (this.IgnoreInput)
            {
                return;
            }
            var touch = WaveServices.Input.TouchPanelState;

            if (touch.Count > 0)
            {
                this.touchPosition = touch[0].Position;
            }

            if (!this.tapping && touch.Count > 0)
            {
                this.tapping = true;
                this.initialTapPosition = touch[0].Position;

                this.playerBehavior.AnimatedScaleY(0.8f, 15f);
            }
            else if (this.tapping && touch.Count == 0)
            {
                this.playerBehavior.AnimatedScaleY(1f, 30f);

                this.tapping = false;
                this.finalTapPosition = this.touchPosition;

                var vector = this.finalTapPosition - this.initialTapPosition;
                vector.Normalize();

                if (Math.Abs(vector.X) > Math.Abs(vector.Y))
                {
                    if (vector.X > 0)
                    {
                        this.playerBehavior.MoveRight();
                    }
                    else
                    {
                        this.playerBehavior.MoveLeft();
                    }
                }
                else
                {
                    if (vector.Y > 0)
                    {
                        this.playerBehavior.MoveDown();
                    }
                    else
                    {
                        this.playerBehavior.MoveUp();
                    }
                }
            }
        }
    }
}

