using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using WaveEngine.Common.Attributes;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;

namespace WaveFrogger.Behaviors
{
    [DataContract]
    public class EntityFollowerCameraBehavior : Behavior
    {
        [RequiredComponent]
        private Transform3D transform = null;

        private Transform3D followedEntityTransform;

        private Vector3 desiredPosition;
        private Vector3 currentPosition;

        [DataMember]
        public Vector3 CameraOffset
        {
            get;
            set;
        }

        [DataMember]
        public Vector3 CameraLookatOffset
        {
            get;
            set;
        }

        [DataMember]
        [RenderPropertyAsEntity]
        public string Followed
        {
            get;
            set;
        }

        public Transform3D FollowedEntityTransform
        {
            get
            {
                if (this.followedEntityTransform == null)
                {
                    this.followedEntityTransform = this.EntityManager.Find(this.Followed).FindComponent<Transform3D>();
                }

                return this.followedEntityTransform;
            }
        }

        protected override void DefaultValues()
        {
            base.DefaultValues();

            this.CameraOffset = new Vector3(-10, 5, -5);
            this.CameraLookatOffset = new Vector3(0, 3, 2);
        }

        protected override void Update(TimeSpan gameTime)
        {
            this.desiredPosition = this.FollowedEntityTransform.Position + this.CameraOffset;

            var currentVector = Vector3.SmoothStep(this.currentPosition, this.desiredPosition, 15f * (float)gameTime.TotalSeconds);
            this.transform.Position = currentVector;
            
            this.currentPosition = this.transform.Position;
        }
    }
}
