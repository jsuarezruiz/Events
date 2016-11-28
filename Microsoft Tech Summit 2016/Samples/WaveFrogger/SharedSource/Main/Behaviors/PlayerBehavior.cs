using System;
using System.Runtime.Serialization;
using WaveEngine.Common.Math;
using WaveEngine.Components.GameActions;
using WaveEngine.Components.Toolkit;
using WaveEngine.Components.Transitions;
using WaveEngine.Components.UI;
using WaveEngine.Framework;
using WaveEngine.Framework.Animation;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Physics3D;
using WaveEngine.Framework.Services;
using WaveFrogger.Scenes;
using WaveFrogger.Services;

namespace WaveFrogger.Behaviors
{
    [DataContract]
    public class PlayerBehavior : Behavior
    {
        [RequiredComponent]
        private Transform3D transform = null;

        [RequiredComponent]
        private BoxCollider3D collider = null;

        private AnimationService animationService;
        private AudioService audioService;

        private BoundingBox aabbBoundingBox;

        private Quaternion leftOrientation;
        private Quaternion rightOrientation;
        private Quaternion upOrientation;
        private Quaternion downOrientation;

        private Vector3 desiredPosition;
        private Vector3 currentPosition;
        private Quaternion desiredOrientation;
        private Quaternion currentOrientation;

        private Vector3 currentScale;
        private Vector3 desiredScale;
        private float scaleAnimationSpeed;

        private bool firstUpdate;

        private TextComponent scoreControl;

        private TerrainGeneratorBehavior terrainGeneratorBehavior;

        private int currentScore;
        private bool isDeadAnimation;

        private Audio.Sfx[] jumpSounds;

        [DataMember]
        public float YOffset { get; set; }

        [DataMember]
        public float JumpSmoothStep { get; set; }

        public PlayerBehavior()
        {
        }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();
            this.firstUpdate = true;

            this.animationService = WaveServices.GetService<AnimationService>();
            this.audioService = WaveServices.GetService<AudioService>();

            var gui = this.EntityManager.Find<Entity>("GUI");
            this.scoreControl = gui.FindChild("currentscore").FindComponent<TextComponent>();

            var gameController = this.EntityManager.Find("GameController");
            this.terrainGeneratorBehavior = gameController.FindComponent<TerrainGeneratorBehavior>();

            this.leftOrientation = Quaternion.CreateFromYawPitchRoll(MathHelper.PiOver2, 0f, 0f);
            this.rightOrientation = Quaternion.CreateFromYawPitchRoll(-MathHelper.PiOver2, 0f, 0f);
            this.upOrientation = Quaternion.CreateFromYawPitchRoll(0f, 0f, 0f);
            this.downOrientation = Quaternion.CreateFromYawPitchRoll(MathHelper.Pi, 0f, 0f);

            this.desiredOrientation = upOrientation;
            this.currentOrientation = this.desiredOrientation;

            this.desiredScale = Vector3.One;
            this.currentScale = this.desiredScale;

            this.jumpSounds = new Audio.Sfx[] { Audio.Sfx.jump1_wav, Audio.Sfx.jump2_wav, Audio.Sfx.jump3_wav };
        }

        public void MoveUp()
        {
            var targetPosition = this.desiredPosition + Vector3.UnitZ;
            if (!this.CheckObstacledPosition(targetPosition))
            {
                this.desiredOrientation = this.upOrientation;
                this.desiredPosition = targetPosition;
                if (this.audioService != null)
                {
                    this.audioService.PlayRandom(jumpSounds);
                }
            }
        }

        public void MoveRight()
        {
            var targetPosition = this.desiredPosition - Vector3.UnitX;
            if (!this.CheckObstacledPosition(targetPosition))
            {
                this.desiredOrientation = this.rightOrientation;
                this.desiredPosition = targetPosition;
                if (this.audioService != null)
                {
                    this.audioService.PlayRandom(jumpSounds);
                }
            }
        }

        public void MoveLeft()
        {
            var targetPosition = this.desiredPosition + Vector3.UnitX;
            if (!this.CheckObstacledPosition(targetPosition))
            {
                this.desiredOrientation = this.leftOrientation;
                this.desiredPosition = targetPosition;
                if (this.audioService != null)
                {
                    this.audioService.PlayRandom(jumpSounds);
                }
            }
        }

        public void MoveDown()
        {
            var targetPosition = this.desiredPosition - Vector3.UnitZ;
            if (!this.CheckObstacledPosition(targetPosition))
            {
                this.desiredOrientation = this.downOrientation;
                this.desiredPosition = targetPosition;
                if (this.audioService != null)
                {
                    this.audioService.PlayRandom(jumpSounds);
                }
            }
        }

        private bool CheckObstacledPosition(Vector3 targetPosition)
        {
            if (targetPosition.X < -this.terrainGeneratorBehavior.HalfExtendLimits
                || targetPosition.X > this.terrainGeneratorBehavior.HalfExtendLimits
                || targetPosition.Z < 0
                || targetPosition.Z < this.terrainGeneratorBehavior.BottomTerrainZ)
            {
                return true;
            }

            bool hasCollision = false;

            var obstacles = this.EntityManager.FindAllByTag(Constants.TAG_OBSTACLES);
            foreach (Entity obstacle in obstacles)
            {
                var obstacleTransform = obstacle.FindComponent<Transform3D>();
                if (obstacleTransform.Position.X == targetPosition.X &&
                   obstacleTransform.Position.Z == targetPosition.Z)
                {
                    hasCollision = true;
                    break;
                }
            }

            return hasCollision;
        }

        private void BackToInitial()
        {
            WaveServices.GetService<ScoreService>().RegisterScore(this.currentScore);

            InitialScene intialScene = null;

            ScreenTransition transition = new CoverTransition(TimeSpan.FromMilliseconds(500), CoverTransition.EffectOptions.FromTop);
            transition.EaseFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };

            WaveServices.TaskScheduler.CreateTask(() =>
            {
                intialScene = new InitialScene();
                intialScene.Initialize(WaveServices.GraphicsDevice);
            })
            .ContinueWith(() =>
            {
                WaveServices.ScreenContextManager.To(new ScreenContext(intialScene), transition);
            });
        }

        protected override void Update(TimeSpan gameTime)
        {
            if (this.isDeadAnimation)
            {
                return;
            }

            if (this.firstUpdate)
            {
                this.aabbBoundingBox.Min = this.currentPosition - this.collider.BoundingBox.HalfExtent;
                this.aabbBoundingBox.Max = this.currentPosition + this.collider.BoundingBox.HalfExtent;

                this.firstUpdate = false;
            }

            if (this.currentPosition != this.desiredPosition)
            {
                this.transform.Position = Vector3.SmoothStep(this.currentPosition, this.desiredPosition, this.JumpSmoothStep * (float)gameTime.TotalSeconds);
                this.currentPosition = this.transform.Position;

                this.aabbBoundingBox.Min = this.currentPosition - this.collider.BoundingBox.HalfExtent;
                this.aabbBoundingBox.Max = this.currentPosition + this.collider.BoundingBox.HalfExtent;
            }


            if (this.currentOrientation != this.desiredOrientation)
            {
                this.currentOrientation = Quaternion.Slerp(this.currentOrientation, this.desiredOrientation, this.JumpSmoothStep * (float)gameTime.TotalSeconds);
                this.transform.Orientation = this.currentOrientation;
            }

            if (this.currentScale != this.desiredScale)
            {
                this.currentScale = Vector3.SmoothStep(this.currentScale, this.desiredScale, this.scaleAnimationSpeed * (float)gameTime.TotalSeconds);
                this.transform.Scale = this.currentScale;
            }


            var vehicles = this.EntityManager.FindAllByTag(Constants.TAG_VEHICLE);
            foreach (Entity vehicle in vehicles)
            {
                var vehicleBehavior = vehicle.FindComponent<VehicleBehavior>();

                // TODO: Workaround: check if position Z >0 cause vehicle boundingboxe create in <0,0,0> and collides
                if (this.currentPosition.Z > 2)
                {
                    if (this.aabbBoundingBox.Intersects(vehicleBehavior.AABBBoundingBox))
                    {
                        // Input stop
                        var inputBehavior = this.Owner.FindComponent<PlayerInputBehavior>();
                        inputBehavior.IgnoreInput = true;

                        var vector = this.currentPosition - vehicle.FindComponent<Transform3D>().Position;
                        var angle = Vector2.Angle(Vector2.UnitX, new Vector2(vector.X, vector.Z));

                        if (this.audioService != null)
                        {
                            this.audioService.Play(Audio.Sfx.chicken1_wav);
                        }

                        if (this.animationService != null)
                        {
                            IGameAction animation = null;
                            if (angle > MathHelper.PiOver4 && angle < 3 * MathHelper.PiOver4)
                            {
                                // look up on this dead
                                this.desiredOrientation = this.upOrientation;
                                this.currentOrientation = this.desiredOrientation;
                                this.transform.Orientation = this.currentOrientation;

                                animation = animationService.CreateDeadAnimation(this.Owner, transform, new Vector3(2f, 1.2f, 0.2f));

                                animation.Delay(TimeSpan.FromMilliseconds(250))
                                    .ContinueWithAction(() =>
                                    {
                                        if (this.audioService != null)
                                        {
                                            this.audioService.Play(Audio.Sfx.deadDrop1_wav);
                                        }
                                    })
                                    .ContinueWith(animationService.CreateFallAnimation(this.Owner, transform));
                            }
                            else
                            {
                                animation = animationService.CreateDeadAnimation(this.Owner, transform, new Vector3(2f, 0.2f, 1.2f));
                            }

                            animation
                                .Delay(TimeSpan.FromMilliseconds(1500))
                                .ContinueWithAction(() =>
                            {
                                this.BackToInitial();
                            });

                            animation.Run();
                        }

                        this.isDeadAnimation = true;
                    }
                }
            }

            this.UpdateScore();
        }

        private void UpdateScore()
        {
            // Only Scores on Z positive, do not score on back movement
            this.currentScore = Math.Max((int)Math.Round(this.transform.Position.Z), this.currentScore);
            this.scoreControl.Text = this.currentScore.ToString();
        }

        public void AnimatedScaleY(float desiredScale, float speed)
        {
            this.desiredScale.Y = desiredScale;
            this.scaleAnimationSpeed = speed;
        }
    }
}
