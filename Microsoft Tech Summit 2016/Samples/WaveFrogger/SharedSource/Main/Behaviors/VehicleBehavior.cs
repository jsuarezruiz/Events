using System;
using System.Runtime.Serialization;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;
using WaveFrogger.Services;

namespace WaveFrogger.Behaviors
{
    [DataContract]
    public class VehicleBehavior : Behavior
    {
        [IgnoreDataMember]
        public BoundingBox AABBBoundingBox;

        [RequiredComponent]
        private Transform3D transform = null;

        [RequiredComponent]
        private BoxCollider3D collider = null;

        [IgnoreDataMember]
        public float Speed { get; set; }

        private AudioService audioService;

        private bool hadHorn;

        protected override void DefaultValues()
        {
            base.DefaultValues();
            this.Speed = 0.1f;
            this.hadHorn = false;
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();

            this.audioService = WaveServices.GetService<AudioService>();
            this.hadHorn = false;
        }

        public void MoveCar(float amount)
        {
            var pos = this.transform.LocalPosition;

            var nextPos = pos.X + amount;

            if (!hadHorn && (Math.Sign(pos.X) != Math.Sign(nextPos)))
            {
                var mustHorn = WaveServices.Random.NextBool(0.3f);
                if (mustHorn)
                {
                    if (this.audioService != null)
                    {
                        this.audioService.PlayRandom(Audio.Sfx.horn1_wav, Audio.Sfx.horn2_wav, Audio.Sfx.horn3_wav, Audio.Sfx.horn4_wav);
                    }

                    this.hadHorn = true;
                }
            }

            pos.X = nextPos;
            this.transform.LocalPosition = pos;

            // Bounding box must rotate (or swap X-Z
            var halfExtend = this.collider.BoundingBox.HalfExtent;
            var temp = halfExtend.X;
            halfExtend.X = halfExtend.Z;
            halfExtend.Z = temp;

            // Update aabbCollider
            this.AABBBoundingBox.Min = this.transform.Position - halfExtend;
            this.AABBBoundingBox.Max = this.transform.Position + halfExtend;
        }

        protected override void Update(TimeSpan gameTime)
        {
            this.MoveCar(this.Speed * (float)gameTime.TotalSeconds);

            // Killvehicle out of the screen
            if (this.transform.Position.X > 12 || this.transform.Position.X < -12)
            {
                this.Owner.Parent.FindComponent<VehicleGeneratorBehavior>().RemoveCar(this.Owner);
            }
        }
    }
}
