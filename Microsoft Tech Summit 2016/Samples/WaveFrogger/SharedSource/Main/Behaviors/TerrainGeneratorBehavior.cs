using System;
using System.Collections.Generic;
using System.Text;
using WaveEngine.Framework;
using System.Linq;
using WaveEngine.Framework.Services;
using WaveEngine.Framework.Graphics;
using System.Runtime.Serialization;
using WaveEngine.Common.Attributes;
using WaveEngine.Common.Math;
using WaveFrogger.Services;

namespace WaveFrogger.Behaviors
{
    [DataContract]
    public class TerrainGeneratorBehavior : Behavior
    {
        private float currentZ;

        public float BottomTerrainZ { get; set; }

        public List<Entity> visibleTerrain;
        private bool firstUpdate;

        [RequiredComponent]
        private EntityPoolComponent entityPoolComponent = null;

        [DataMember]
        public int TerrainWindowSize { get; set; }

        [DataMember]
        public int BackwardTerrainSize { get; set; }

        [DataMember]
        public int HalfExtendLimits { get; set; }

        private Transform3D PlayerTransform { get; set; }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();

            this.visibleTerrain = new List<Entity>();

            this.firstUpdate = true;

            var player = this.EntityManager.Find("player");
            this.PlayerTransform = player.FindComponent<Transform3D>();

            this.entityPoolComponent.HalfExtendLimits = this.HalfExtendLimits;
        }

        private Entity AddNewRow(int prefabIndex = -1)
        {
            var nextTerrain = this.entityPoolComponent.RetrieveTerrainEntity(prefabIndex);
            nextTerrain.Name = "terrain" + this.currentZ;

            var pos = nextTerrain.FindComponent<Transform3D>().Position;
            pos.Z = this.currentZ;
            nextTerrain.FindComponent<Transform3D>().Position = pos;

            this.visibleTerrain.Add(nextTerrain);
            this.currentZ += 3;

            return nextTerrain;
        }

        private Entity RemoveBottom()
        {
            Entity toRemove = null;
            if (this.visibleTerrain.Count > 0)
            {
                toRemove = this.visibleTerrain[0];
                this.visibleTerrain.RemoveAt(0);
            }

            return toRemove;
        }

        protected override void Update(TimeSpan gameTime)
        {
            if (this.firstUpdate)
            {
                // intial rows
                this.AddNewRow(0);
                this.AddNewRow(0);

                for (int i = 0; i < this.TerrainWindowSize - 2; i++)
                {
                    this.AddNewRow();
                }

                foreach (var terrain in this.visibleTerrain)
                {
                    this.EntityManager.Add(terrain);
                }

                this.firstUpdate = false;
            }

            this.CheckTerraFormer();
        }

        private void CheckTerraFormer()
        {
            var playerZ = this.PlayerTransform.Position.Z;
            this.BottomTerrainZ = this.visibleTerrain[0].FindComponent<Transform3D>().Position.Z;
            var topTerrainZ = this.visibleTerrain.Last().FindComponent<Transform3D>().Position.Z;

            if (playerZ - BottomTerrainZ > this.BackwardTerrainSize)
            {
                var rowToRemove = this.RemoveBottom();

                var trees = rowToRemove.FindAllChildrenByTag(Constants.TAG_OBSTACLES);
                var limits = rowToRemove.FindAllChildrenByTag(Constants.TAG_LIMITS);
                var cars = rowToRemove.FindAllChildrenByTag(Constants.TAG_VEHICLE);

                this.entityPoolComponent.FreeTreeEntity(trees);
                this.entityPoolComponent.FreeTreeLimitEntity(limits);
                this.entityPoolComponent.FreeCarEntity(cars);

                this.entityPoolComponent.FreeTerrain(new List<Entity>() { rowToRemove });
            }

            if (playerZ + (this.TerrainWindowSize * 3) > topTerrainZ)
            {
                this.EntityManager.Add(this.AddNewRow());
            }
        }

        private void RemoveChildrenFromParents(IEnumerable<Entity> collection, Queue<Entity> pool)
        {
            foreach (var entity in collection)
            {
                if (entity.Parent != null)
                {
                    entity.Parent.DetachChild(entity.Name);
                    pool.Enqueue(entity);
                }
            }
        }
    }
}
