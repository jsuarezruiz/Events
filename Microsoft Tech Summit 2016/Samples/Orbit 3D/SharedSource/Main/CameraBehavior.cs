namespace Orbit
{
    using System;
    using System.Runtime.Serialization;
    using WaveEngine.Common.Math;
    using WaveEngine.Framework;
    using WaveEngine.Framework.Graphics;

    namespace Orbit
    {
        [DataContract]
        public class CameraBehavior : Behavior
        {
            private string _planet;

            [RequiredComponent]
            private Transform3D _transform = null;

            private Transform3D _followEntityTransform;

            [DataMember]
            public Vector3 Offset { get; set; }

            [DataMember]
            public string Planet
            {
                get { return _planet; }
                set
                {
                    _planet = value;
                    if (this.isInitialized)
                    {
                        this.RefreshFollowEntity(_planet);
                    }
                }
            }

            protected override void DefaultValues()
            {
                base.DefaultValues();

                this.Offset = new Vector3(5, 2, 0);
                this._planet = "earth";
            }

            protected override void ResolveDependencies()
            {
                base.ResolveDependencies();

                this.RefreshFollowEntity(this._planet);
            }

            private void RefreshFollowEntity(string name)
            {
                var entity = this.EntityManager?.Find(name);
                _followEntityTransform = entity?.FindComponent<Transform3D>();
            }

            protected override void Update(TimeSpan gameTime)
            {
                if (_followEntityTransform != null)
                {
                    Vector3 followEntityPosition = _followEntityTransform.LocalPosition;
                    Vector3 followEntityPositionNormalize = _followEntityTransform.LocalPosition;
                    followEntityPositionNormalize.Normalize();

                    Quaternion quaternion = Quaternion.CreateFromTwoVectors(Vector3.UnitX, followEntityPositionNormalize);
                    Vector3 offsetTransformed = Vector3.Transform(this.Offset, quaternion);

                    _transform.LocalPosition = followEntityPosition + offsetTransformed;
                    _transform.LocalLookAt(followEntityPosition, Vector3.Up);
                }
            }
        }
    }
}
