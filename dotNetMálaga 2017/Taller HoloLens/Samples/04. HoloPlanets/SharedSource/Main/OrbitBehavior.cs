namespace HoloPlanets
{
    using System;
    using System.Runtime.Serialization;
    using WaveEngine.Common.Math;
    using WaveEngine.Framework;
    using WaveEngine.Framework.Graphics;

    [DataContract]
    public class OrbitBehavior : Behavior
    {
        [RequiredComponent]
        private Transform3D _transform;

        [DataMember]
        public float RotationSpeed { get; set; }

        [DataMember]
        public float TranslationSpeed { get; set; }

        protected override void DefaultValues()
        {
            base.DefaultValues();

            this.RotationSpeed = 1.0f;
            this.TranslationSpeed = 1.0f;
        }

        protected override void Update(TimeSpan gameTime)
        {            
            _transform.RotateAround(Vector3.Zero, Quaternion.CreateFromAxisAngle(Vector3.Up, this.TranslationSpeed * (float)gameTime.TotalSeconds));
            _transform.Orientation *= Quaternion.CreateFromYawPitchRoll((float)gameTime.TotalSeconds, 0,0);
        }
    }
}
