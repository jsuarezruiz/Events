using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using WaveEngine.Common;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Graphics;
using WaveEngine.Framework.Managers;
using WaveEngine.Framework.Services;
using System.Linq;

namespace WaveFrogger.Services
{
    [DataContract]
    public class EntityPoolComponent : Component
    {
        private string[] treeLimitPrefabs;

        private string[] treePrefabs;

        private string[] carPrefabs;

        private string[] terrainPrefabs;

        private Queue<Entity> treePool;

        private Queue<Entity> treeTerrainLimitsPool;

        private Queue<Entity> carPool;

        private Dictionary<string, Queue<Entity>> terrainPool;

        [IgnoreDataMember]
        public int HalfExtendLimits { get; set; }

        [DataMember]
        public int CarPoolSize { get; set; }

        [DataMember]
        public int TreePoolSize { get; set; }

        [DataMember]
        public int TerrainLimitsPoolSize { get; set; }

        [DataMember]
        public int TerrainPoolSize { get; set; }

        protected override void ResolveDependencies()
        {
            base.ResolveDependencies();

            this.treePool = new Queue<Entity>();
            this.treeTerrainLimitsPool = new Queue<Entity>();
            this.carPool = new Queue<Entity>();
            this.terrainPool = new Dictionary<string, Queue<Entity>>();

            this.treeLimitPrefabs = new string[]
            {
                WaveContent.Prefabs.treelimits,
            };

            this.treePrefabs = new string[]
            {
                WaveContent.Prefabs.Tree01,
                WaveContent.Prefabs.Tree02,
                WaveContent.Prefabs.Tree03,
                WaveContent.Prefabs.Tree04,
            };

            this.carPrefabs = new string[]
            {
                WaveContent.Prefabs.car01,
                WaveContent.Prefabs.car02,
                WaveContent.Prefabs.car03,
            };

            this.terrainPrefabs = new string[]
            {
                WaveContent.Prefabs.fieldRow,
                WaveContent.Prefabs.roadRow
            };

            this.FillPool();
        }

        private void AddToPool(string prefab, string tag, Queue<Entity> pool)
        {
            var entity = this.EntityManager.Instantiate(prefab);
            entity.Tag = tag;
            pool.Enqueue(entity);
        }

        public void FillPool()
        {
            for (int i = 0; i < this.TreePoolSize; i++)
            {
                var prefab = this.treePrefabs[WaveServices.Random.Next(this.treePrefabs.Length)];
                this.AddToPool(prefab, Constants.TAG_OBSTACLES, this.treePool);
            }

            for (int i = 0; i < this.CarPoolSize; i++)
            {
                var prefab = this.carPrefabs[WaveServices.Random.Next(this.carPrefabs.Length)];
                this.AddToPool(prefab, Constants.TAG_VEHICLE, this.carPool);
            }

            for (int i = 0; i < this.TerrainLimitsPoolSize; i++)
            {
                var prefab = this.treeLimitPrefabs[WaveServices.Random.Next(this.treeLimitPrefabs.Length)];
                this.AddToPool(prefab, Constants.TAG_LIMITS, this.treeTerrainLimitsPool);
            }

            foreach (var prefab in this.terrainPrefabs)
            {
                this.terrainPool.Add(prefab, new Queue<Entity>());

                for (int i = 0; i < this.TerrainPoolSize; i++)
                {
                    this.AddToPool(prefab, prefab, this.terrainPool[prefab]);
                }
            }
        }

        public Entity RetrieveTerrainLimitEntity()
        {
            return this.Retrieve(this.treeTerrainLimitsPool, this.treeLimitPrefabs, Constants.TAG_LIMITS);
        }

        public Entity RetrieveTreeEntity()
        {
            return this.Retrieve(this.treePool, this.treePrefabs, Constants.TAG_OBSTACLES);
        }

        public Entity RetrieveCarEntity()
        {
            return this.Retrieve(this.carPool, this.carPrefabs, Constants.TAG_VEHICLE);
        }

        public void FreeTreeEntity(IEnumerable<Entity> collection)
        {
            this.FreeEntity(collection, this.treePool);
        }

        public void FreeTreeLimitEntity(IEnumerable<Entity> collection)
        {
            this.FreeEntity(collection, this.treeTerrainLimitsPool);
        }

        public void FreeCarEntity(IEnumerable<Entity> collection)
        {
            this.FreeEntity(collection, this.carPool);
        }

        public Entity RetrieveTerrainEntity(int prefabIndex = -1)
        {
            int index;
            if (prefabIndex < 0)
            {
                index = WaveServices.Random.Next(this.terrainPrefabs.Length);
            }
            else
            {
                index = prefabIndex;
            }

            var prefab = this.terrainPrefabs[index];

            return this.RetrieveTerrainEntity(prefab);
        }

        private Entity RetrieveTerrainEntity(string prefab)
        {
            Entity entity = null;
            Queue<Entity> pool = this.terrainPool[prefab];

            if (pool.Count > 0)
            {
                entity = pool.Dequeue();
            }
            else
            {
                entity = this.EntityManager.Instantiate(prefab);
                entity.Tag = prefab;
            }

            if (prefab == WaveContent.Prefabs.fieldRow)
            {
                // Limit models
                this.AddTreeLimits("leftMargin", Vector3.UnitX * (this.HalfExtendLimits + 3), entity);
                this.AddTreeLimits("rightMargin", -Vector3.UnitX * (this.HalfExtendLimits + 3), entity);

                // Obstacles
                this.AddRandomTrees(6, entity);
            }

            return entity;
        }

        public Entity Retrieve(Queue<Entity> pool, string[] prefabs, string tag)
        {
            Entity entity;

            if (pool.Count > 0)
            {
                entity = pool.Dequeue();
            }
            else
            {
                var prefab = prefabs[WaveServices.Random.Next(prefabs.Length)];
                entity = this.EntityManager.Instantiate(prefab);
                entity.Tag = tag;
            }

            return entity;
        }

        public void FreeTerrain(IEnumerable<Entity> collection)
        {
            foreach (var entity in collection)
            {
                this.FreeTreeEntity(entity.FindAllChildrenByTag(Constants.TAG_OBSTACLES));
                this.FreeTreeLimitEntity(entity.FindAllChildrenByTag(Constants.TAG_LIMITS));
                this.FreeCarEntity(entity.FindAllChildrenByTag(Constants.TAG_VEHICLE));

                this.EntityManager.Detach(entity);
                this.terrainPool[entity.Tag].Enqueue(entity);
            }
        }

        private void FreeEntity(IEnumerable<Entity> collection, Queue<Entity> pool)
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

        private void AddTreeLimits(string name, Vector3 position, Entity parent)
        {
            Entity limitEntity = this.RetrieveTerrainLimitEntity();
            if (limitEntity != null)
            {
                limitEntity.Name = name;
                limitEntity.FindComponent<Transform3D>().LocalPosition = position;
                parent.AddChild(limitEntity);
            }
        }

        private void AddRandomTrees(int count, Entity nextTerrain)
        {
            for (int i = 0; i < count; i++)
            {
                Entity tree = this.RetrieveTreeEntity();
                if (tree != null)
                {
                    tree.Name += i.ToString();

                    Vector3 pos = Vector3.Zero;
                    pos.X = WaveServices.Random.Next(-this.HalfExtendLimits, this.HalfExtendLimits + 1);
                    pos.Z = WaveServices.Random.Next(4);

                    tree.FindComponent<Transform3D>().LocalPosition = pos;

                    nextTerrain.AddChild(tree);
                }
            }
        }
    }
}
