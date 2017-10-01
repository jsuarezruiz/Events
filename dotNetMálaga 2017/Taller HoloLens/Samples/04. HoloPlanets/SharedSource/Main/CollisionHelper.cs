#region File Description
//-----------------------------------------------------------------------------
// CollisionHelper
//
// Copyright © 2017 Plainconcepts S.L. rights reserved.
// Use is subject to license terms.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using WaveEngine.Common.Math;
using WaveEngine.Framework;
using WaveEngine.Framework.Physics3D;
#endregion


namespace HoloPlanets
{
    public static class CollisionHelper
    {

        public struct RayCastResult
        {
            public Entity HitBody;
            public RayHit3D HitData;
        }

        public static void RayCast3D(Scene scene, ref Ray ray, out RayCastResult result)
        {
            foreach (Entity e in scene.EntityManager.AllEntities)
            {
                var sphereCollider = e.FindComponent<SphereCollider3D>();
                if (sphereCollider != null)
                {
                    CollisionHelper.CalculateCollision(ref ray, ref sphereCollider, out result);
                    if (result.HitBody != null)
                    {
                        return;
                    }
                }
            }

            result = new RayCastResult();
        }

        /// <summary>
        /// Calculate collision
        /// </summary>
        /// <param name="ray"></param>
        /// <param name="sphereCollider"></param>
        /// <param name="result"></param>
        private static void CalculateCollision(ref Ray ray, ref SphereCollider3D sphereCollider, out RayCastResult result)
        {
            Vector3 sphereCenter = sphereCollider.Transform3D.Position;
            float sphereRadius = sphereCollider.Radius * sphereCollider.Transform3D.Scale.X / 2;

            Vector3 m = ray.Position - sphereCenter;
            float b = Vector3.Dot(m, ray.Direction);
            float c = Vector3.Dot(m, m) - sphereRadius * sphereRadius;

            // Exit if r’s origin outside s (c > 0) and r pointing away from s (b > 0) 
            if (c > 0.0f && b > 0.0f)
            {
                result = new RayCastResult();
                return;
            }

            float discr = b * b - c;

            // A negative discriminant corresponds to ray missing sphere 
            if (discr < 0.0f)
            {
                result = new RayCastResult();
                return;
            }

            // Ray now found to intersect sphere, compute smallest t value of intersection
            float t = -b - (float)Math.Sqrt(discr);

            // If t is negative, ray started inside sphere so clamp t to zero 
            if (t < 0.0f)
            {
                t = 0.0f;
            }

            Vector3 q = ray.Position + t * ray.Direction;
            Vector3 normal = (q - sphereCenter);
            normal.Normalize();

            result = new RayCastResult()
            {
                HitBody = sphereCollider.Owner,
                HitData = new RayHit3D()
                {
                    Location = q,
                    Normal = normal,
                    T = t,
                }
            };
        }
    }
}
