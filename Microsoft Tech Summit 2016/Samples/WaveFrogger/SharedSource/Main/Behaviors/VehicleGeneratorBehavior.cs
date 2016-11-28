using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Services;
using WaveFrogger.Services;

namespace WaveFrogger.Behaviors
{
    [DataContract]
    public class VehicleGeneratorBehavior : Behavior
    {
        private int count;

        private TimeSpan currentTime;

        private EntityPoolComponent entityPoolComponent;

        [DataMember]
        public TimeSpan MinSpawnTimeInSeconds { get; set; }

        [DataMember]
        public TimeSpan MaxSpawnTimeInSeconds { get; set; }

        [DataMember]
        public int SpawnDistante { get; set; }

        [DataMember]
        public float MinSpeed { get; set; }

        [DataMember]
        public float MaxSpeed { get; set; }

        [DataMember]
        public bool IsLeft { get; set; }

        private TimeSpan spawnTime;

        private float speed;

        private List<Entity> carsToRemove;

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();

            var gameController = this.EntityManager.Find("GameController");
            if (gameController != null)
            {
                this.entityPoolComponent = gameController.FindComponent<EntityPoolComponent>();
            }

            var randomPercent1 = WaveServices.Random.NextDouble();
            var randomSpawn = this.MinSpawnTimeInSeconds.TotalSeconds + ((this.MaxSpawnTimeInSeconds.TotalSeconds - this.MinSpawnTimeInSeconds.TotalSeconds) * randomPercent1);
            this.spawnTime = TimeSpan.FromSeconds(randomSpawn);

            var randomPercent2 = WaveServices.Random.NextDouble();
            this.speed = (float)(this.MinSpeed + ((this.MaxSpeed - this.MinSpeed) * randomPercent2));

            this.carsToRemove = new List<Entity>();
        }

        protected override void Update(TimeSpan gameTime)
        {
            this.currentTime += gameTime;

            if (this.currentTime > this.spawnTime)
            {
                this.GenerateCar();
                this.currentTime = TimeSpan.Zero;
            }

            if (this.carsToRemove.Count > 0)
            {
                this.entityPoolComponent.FreeCarEntity(this.carsToRemove);
                this.carsToRemove.Clear();
            }
        }

        private void GenerateCar()
        {
            float direction = this.IsLeft ? 1 : -1;

            var carEntity = this.entityPoolComponent.RetrieveCarEntity();
            carEntity.Name = "car" + count++;

            var transform = carEntity.FindComponent<Transform3D>();
            var position = transform.LocalPosition;
            position.X = this.SpawnDistante * direction;

            transform.LocalPosition = position;

            Vector3 rotation = Vector3.UnitY * MathHelper.PiOver2;
            if (this.IsLeft)
            {
                rotation *= -1;
            }
            carEntity.FindComponent<Transform3D>().LocalRotation = rotation;

            var behavior = carEntity.FindComponent<VehicleBehavior>();
            behavior.Speed = this.speed * -direction;
            
            carEntity.Enabled = true;
            carEntity.IsActive = true;
            behavior.IsActive = true;

            this.Owner.AddChild(carEntity);
        }

        public void RemoveCar(Entity car)
        {
            if (!this.carsToRemove.Contains(car))
            {
                this.carsToRemove.Add(car);
            }
        }
    }
}
