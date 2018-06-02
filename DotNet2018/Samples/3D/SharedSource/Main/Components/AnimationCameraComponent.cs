using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using WaveEngine.Common.Math;
using WaveEngine.Components.GameActions;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using XamarinForms3DCarSample.Behaviors;

namespace XamarinForms3DCarSample.Components
{
    [DataContract]
    public class AnimationCameraComponent : Component
    {
        [DataMember]
        public float TranslationDuration { get; set; }

        [DataMember]
        public float RotationDuration { get; set; }

        public event EventHandler<EventArgs> AnimationCompleted;

        protected override void DefaultValues()
        {
            TranslationDuration = 2f;
            RotationDuration = 2f;
        }

        public void MoveTo(Entity destinationEntity)
        {
            var scene = this.Owner.Scene;

            var cameraPoint = FindCameraPosition(this.Owner);

            List<Transform3D> path = calculatePath(this.Owner, cameraPoint, destinationEntity);

            var limitCameraBehavior = Owner.FindComponent<LimitCameraBehavior>();
            limitCameraBehavior.IsActive = false;

            IGameAction action = null;

            if (path.Count() > 1)
            {
                // rotation action
                IGameAction rotationAction = null;
                for (int i = 0; i < path.Count; i++)
                {
                    if (i == 0)
                    {
                        rotationAction = scene.CreateGameAction(
                            new RotateToShortest3DGameAction(
                                Owner,
                                path[i].Rotation,
                                TimeSpan.FromSeconds(TranslationDuration / path.Count),
                                EaseFunction.None,
                                false,
                                true));
                    }
                    else
                    {
                        rotationAction =
                          rotationAction.ContinueWith(
                             scene.CreateGameAction(
                             new RotateToShortest3DGameAction(
                                 Owner,
                                 path[i].Rotation,
                                 TimeSpan.FromSeconds(TranslationDuration / path.Count),
                                 (i == path.Count - 1) ? EaseFunction.SineOutEase : EaseFunction.None,
                                 false,
                                 true)));
                    }
                }

                // init movement rotation
                var transform = this.Owner.FindComponent<Transform3D>();
                path.Insert(0, transform);
                CatmullRomSpline catmullRomSpline = new CatmullRomSpline(path.ToArray());

                action = scene.CreateParallelGameActions(
                        new FloatAnimationGameAction(
                            this.Owner,
                            0,
                            1,
                            TimeSpan.FromSeconds(TranslationDuration),
                            EaseFunction.SineInOutEase,
                            (lerp) =>
                            {
                                transform.Position = catmullRomSpline.GetPosition(lerp);
                            }),
                        rotationAction
                        )
                        .WaitAll();
            }
            else
            {
                action = scene.CreateParallelGameActions(
                        new MoveTo3DGameAction(
                            this.Owner,
                            path.Last().Position,
                            TimeSpan.FromSeconds(TranslationDuration),
                            EaseFunction.SineInOutEase),
                        new RotateToShortest3DGameAction(
                            this.Owner,
                            path.Last().Rotation,
                            TimeSpan.FromSeconds(TranslationDuration),
                            EaseFunction.SineOutEase,
                            false,
                            true))
                        .WaitAll();
            }

            if (action != null)
            {
                action.Run();
                action.Completed += (obj) =>
                {
                    limitCameraBehavior.OriginalRotation = path.Last().Rotation;
                    limitCameraBehavior.IsActive = true;
                    var destinationLimitCameraBehavior = destinationEntity.FindComponent<LimitCameraBehavior>();
                    limitCameraBehavior.MaxPitch = destinationLimitCameraBehavior?.MaxPitch ?? LimitCameraBehavior.DefaultMaxPitch;
                    limitCameraBehavior.MaxYaw = destinationLimitCameraBehavior?.MaxYaw ?? LimitCameraBehavior.DefaultMaxYaw;
                  
                    // Invoke subscribed
                    AnimationCompleted?.Invoke(this, new EventArgs());
                };
            }
        }

        private Entity FindCameraPosition(Entity camera)
        {
            var cameraPosition = camera.FindComponent<Transform3D>().Position;

            return (Entity)this.EntityManager.FindAllByTag("camera")
                                                .Where(e => (e as Entity).FindComponent<Transform3D>().Position.Equals(cameraPosition))
                                                .FirstOrDefault();
        }

        public List<Transform3D> calculatePath(Entity camera, Entity cameraPoint, Entity endEntity)
        {
            var cameraPosition = camera.FindComponent<Transform3D>().Position;

            if (cameraPoint != null)
            {
                var path = ((Entity)cameraPoint)?.Parent?.ChildEntities?.Where(e => e.Name.Equals("path")).FirstOrDefault();

                var result = path?.ChildEntities?.Where(e => e.Name.StartsWith(endEntity.Name))
                                                         .Select(e => e.FindComponent<Transform3D>())
                                                         .OrderBy(p => Vector3.Distance(cameraPosition, p.Position)).ToList();
                if (result != null)
                {
                    // Add last point
                    result.Add(endEntity.FindComponent<Transform3D>());
                    return result;
                }
            }

            return new List<Transform3D> { endEntity.FindComponent<Transform3D>() };
        }
    }
}