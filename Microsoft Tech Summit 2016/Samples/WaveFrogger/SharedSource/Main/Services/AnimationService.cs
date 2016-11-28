using System;
using WaveEngine.Common;
using WaveEngine.Common.Math;
using WaveEngine.Components.GameActions;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace WaveFrogger.Services
{
    public class AnimationService : Service
    {
        public IGameAction CreateDeadAnimation(Entity entity, Transform3D transform, Vector3 to)
        {
            var actionAnimation = new Vector3AnimationGameAction(entity, Vector3.One, to, TimeSpan.FromMilliseconds(100), EaseFunction.CubicOutEase, (value) =>
            {
                transform.LocalScale = value;
            });

            return actionAnimation;
        }
            public IGameAction CreateFallAnimation(Entity entity, Transform3D transform)
        {
            var actionAnimation = new RotateTo3DGameAction(entity, Vector3.UnitX * MathHelper.PiOver2, TimeSpan.FromMilliseconds(550), EaseFunction.BounceOutEase);

            return actionAnimation;
        }

        public IGameAction CreatePulseAnimation(Entity entity, double lapse)
        {
            var actionAnimation = GameActionFactory.CreateLoopGameActionUntil(entity.Scene, () => new ScaleTo2DGameAction(entity, new Vector2(1.3f, 1.1f), TimeSpan.FromMilliseconds(lapse), EaseFunction.CubicOutEase)
                .ContinueWith(new ScaleTo2DGameAction(entity, Vector2.One, TimeSpan.FromMilliseconds(lapse), EaseFunction.CubicInEase)), () => false);

            return actionAnimation;
        }

        public IGameAction CreateIdleAnimation(Entity entity, double lapse)
        {
            var actionAnimation = GameActionFactory.CreateLoopGameActionUntil(entity.Scene, () => new ScaleTo3DGameAction(entity, new Vector3(1.0f, 0.9f, 1.0f), TimeSpan.FromMilliseconds(lapse), EaseFunction.CubicInEase)
                .ContinueWith(new ScaleTo3DGameAction(entity, Vector3.One, TimeSpan.FromMilliseconds(lapse), EaseFunction.CubicOutEase)), () => false);

            return actionAnimation;
        }
    }
}
