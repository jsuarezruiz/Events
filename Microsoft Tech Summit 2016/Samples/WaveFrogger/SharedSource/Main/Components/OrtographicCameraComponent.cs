using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace WaveFrogger.Components
{
    [DataContract]
    public class OrtographicCameraComponent : Component
    {
        [RequiredComponent]
        protected Transform3D Transform3D;

        [RequiredComponent]
        protected Camera3D Camera3D;

        public OrtographicCameraComponent() : base()
        { }

        protected OrtographicCameraComponent(string name) : base(name)
        {
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();

            var angle = MathHelper.PiOver2 * (this.Camera3D.FieldOfView / 2);
            var orthogonalWidth = (float)Math.Tan(angle) * this.Transform3D.Position.Length() * 2f;
            var orthogonalHeight = orthogonalWidth / this.Camera3D.AspectRatio;
            var projection = Matrix.CreateOrthographic(orthogonalWidth,
                orthogonalHeight,
                this.Camera3D.NearPlane,
                this.Camera3D.FarPlane);
            this.Camera3D.SetCustomProjection(projection);
        }
    }
}
