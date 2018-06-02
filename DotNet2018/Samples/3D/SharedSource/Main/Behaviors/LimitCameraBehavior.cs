using System;
using System.Linq;
using System.Runtime.Serialization;
using WaveEngine.Common.Input;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;

namespace XamarinForms3DCarSample.Behaviors
{
    [DataContract]
    public class LimitCameraBehavior : Behavior
    {
        public const float DefaultMaxYaw = MathHelper.PiOver2 * 0.15f;

        public const float DefaultMaxPitch = MathHelper.PiOver2 * 0.15f;

        private const float StickThreshold = 0.1f;

        [RequiredComponent]
        private Transform3D Transform = null;

        private TouchLocation currentTouchLocation;

        private TouchPanelState currentTouchPanelState;

        [RequiredService]
        private Input input = null;

        private bool isDragging = false;

        private bool isTouchPanelConnected;

        private TouchLocation lastTouchLocation;

        private bool moveBack;

        private bool moveForward;

        private bool moveLeft;

        private bool moveRight;

        private bool moveWithTouchPanel;

        private float rotationSpeed;

        private float maxYaw;

        private float maxPitch;

        private bool enabled;

        private float speed;

        private float timeDifference;

        private float xDifference;

        private float yDifference;

        public bool IsDragging { get; set; }

        public Vector3 OriginalRotation { get; set; }

        [DataMember]
        public float RotationSpeed
        {
            get
            {
                return this.rotationSpeed;
            }

            set
            {
                this.rotationSpeed = value;
            }
        }

        [DataMember]
        public float Speed
        {
            get
            {
                return this.speed;
            }

            set
            {
                this.speed = value;
            }
        }

        [DataMember]
        public float MaxPitch
        {
            get
            {
                return this.maxPitch;
            }

            set
            {
                this.maxPitch = value;
            }
        }

        [DataMember]
        public float MaxYaw
        {
            get
            {
                return this.maxYaw;
            }

            set
            {
                this.maxYaw = value;
            }
        }

        protected override void DefaultValues()
        {
            base.DefaultValues();

            this.RotationSpeed = 0.004f;
            this.Speed = 20.0f;
            this.MaxPitch = DefaultMaxPitch;
            this.MaxYaw = DefaultMaxYaw;
        }

        protected override void Initialize()
        {
            this.xDifference = 0f;
            this.yDifference = 0f;
            this.IsDragging = false;
        }

        protected override void Update(TimeSpan gameTime)
        {
            this.timeDifference = (float)gameTime.TotalSeconds;
            this.HandleInput(this.timeDifference);
        }

        private void HandleInput(float amount)
        {
            this.isTouchPanelConnected = this.input.TouchPanelState.IsConnected;

            if (this.isTouchPanelConnected)
            {
                if (this.isTouchPanelConnected)
                {
                    this.currentTouchPanelState = this.input.TouchPanelState;
                }

                // If there's a touch or right mouse button is pressed...
                if ((this.isTouchPanelConnected && this.currentTouchPanelState.Count == 1))
                {
                    // If there's a touch, capture its touch properties
                    if (this.isTouchPanelConnected && this.currentTouchPanelState.Count == 1)
                    {
                        this.currentTouchLocation = this.currentTouchPanelState.First();
                    }

                    // If current touch is pressed or moved
                    if ((this.isTouchPanelConnected
                         && (this.currentTouchLocation.State == TouchLocationState.Pressed
                             || this.currentTouchLocation.State == TouchLocationState.Moved)))
                    {
                        if (this.isDragging == false)
                        {
                            isDragging = true;
                        }
                        else
                        {
                            // Get the current different between x and Y
                            // From touchpad
                            if (this.currentTouchPanelState.IsConnected)
                            {
                                this.xDifference = this.currentTouchLocation.Position.X - this.lastTouchLocation.Position.X;
                                this.yDifference = this.currentTouchLocation.Position.Y - this.lastTouchLocation.Position.Y;
                            }

                            // Calculated yaw and pitch
                            float yaw = -this.xDifference * this.rotationSpeed;
                            float pitch = -this.yDifference * this.rotationSpeed;

                            this.UpdateOrientation(yaw, pitch);
                        }
                    }

                    this.lastTouchLocation = this.currentTouchLocation;
                }
                else
                {
                    this.IsDragging = this.isDragging = false;
                }
            }
        }

        private void UpdateCameraPosition(float amount)
        {
            Vector3 displacement = Vector3.Zero;
            if (this.moveForward)
            {
                Vector3 forward = this.Transform.WorldTransform.Forward;

                // Manual inline: position += speed * forward;
                displacement.X = displacement.X + (amount * this.speed * forward.X);
                displacement.Y = displacement.Y + (amount * this.speed * forward.Y);
                displacement.Z = displacement.Z + (amount * this.speed * forward.Z);
            }
            else if (this.moveBack)
            {
                Vector3 backward = this.Transform.WorldTransform.Backward;

                // Manual inline: position -= speed * forward;
                displacement.X = displacement.X + (amount * this.speed * backward.X);
                displacement.Y = displacement.Y + (amount * this.speed * backward.Y);
                displacement.Z = displacement.Z + (amount * this.speed * backward.Z);
            }

            if (this.moveLeft)
            {
                Vector3 left = this.Transform.WorldTransform.Left;

                // Manual inline: position -= speed * right;
                displacement.X = displacement.X + (amount * this.speed * left.X);
                displacement.Y = displacement.Y + (amount * this.speed * left.Y);
                displacement.Z = displacement.Z + (amount * this.speed * left.Z);
            }
            else if (this.moveRight)
            {
                Vector3 right = this.Transform.WorldTransform.Right;

                // Manual inline: position += speed * right;
                displacement.X = displacement.X + (amount * this.speed * right.X);
                displacement.Y = displacement.Y + (amount * this.speed * right.Y);
                displacement.Z = displacement.Z + (amount * this.speed * right.Z);
            }

            // Manual inline: camera.Position = position;
            this.Transform.Position += displacement;
        }

        private void UpdateOrientation(float yaw, float pitch)
        {

            if (yaw > 0 || pitch > 0)
            {
                this.IsDragging = true;
            }


            var rotation = this.Transform.Rotation;

            rotation.Y += yaw * Speed;
            rotation.Y = Math.Min(rotation.Y, OriginalRotation.Y + MaxYaw);
            rotation.Y = Math.Max(rotation.Y, OriginalRotation.Y - MaxYaw);

            rotation.X += pitch * Speed;
            rotation.X = Math.Max(rotation.X, OriginalRotation.X - MaxPitch);
            rotation.X = Math.Min(rotation.X, OriginalRotation.X + MaxPitch);

            this.Transform.Rotation = rotation;
        }
    }
}