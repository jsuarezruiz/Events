using System;
using System.Runtime.Serialization;
using WaveEngine.Common.Math;
using WaveEngine.Components.Toolkit;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;
using WaveEngine.Hololens.Toolkit;

namespace HoloPlanets
{
    [DataContract]
    public class InfoComponent : Component
    {
        [RequiredComponent]
        public GazeBehavior _gaze;

        private Ray _ray;

        protected override void Initialize()
        {
            base.Initialize();

            _gaze.AirTapEvent += OnAirTapEvent;
        }

        private void OnAirTapEvent(object sender, EventArgs e)
        {
            var cameraTransform = this.RenderManager.ActiveCamera3D.Transform;
            _ray.Position = cameraTransform.Position;
            _ray.Direction = cameraTransform.WorldTransform.Forward;

            CollisionHelper.RayCastResult result;
            CollisionHelper.RayCast3D(this.Owner.Scene, ref _ray, out result);

            if (result.HitBody != null)
            {
                ShowInfo(result);
            }
        }

        private void ShowInfo(CollisionHelper.RayCastResult result)
        {
            Camera3D camera = this.RenderManager.ActiveCamera3D;

            if (camera != null)
            {
                // Show Planet Info
                var panel = EntityManager.Find("pane");
                panel.IsVisible = true;
                var panelBackground = panel.FindChild("paneBackground");
                panelBackground.IsVisible = true;
                var text = panel.FindChild("text");
                text.IsVisible = true;

                var panelTransform = panel.FindComponent<Transform3D>();
                panelTransform.Position = result.HitData.Location + new Vector3(0, 2f, 0);
                panelTransform.LookAt(camera.Position);

                var textComponent = text.FindComponent<TextComponent>();
                textComponent.Text = result.HitBody.Name;
            }
        }
    }
}