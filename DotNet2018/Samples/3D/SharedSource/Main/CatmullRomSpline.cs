using System.Runtime.Serialization;
using WaveEngine.Common.Math;
using WaveEngine.Framework.Graphics;

namespace XamarinForms3DCarSample
{
    [DataContract]
    public class CatmullRomSpline
    {
        Vector3[] internalControlPoints;
        float[] lerpCP;

        public CatmullRomSpline(Transform3D[] controlPoints)
        {
            Vector3 startAnchor = controlPoints[0].Position + (controlPoints[0].Position - controlPoints[1].Position);
            Vector3 endAnchor = controlPoints[controlPoints.Length - 1].Position - (controlPoints[controlPoints.Length - 2].Position - controlPoints[controlPoints.Length - 1].Position);

            this.internalControlPoints = new Vector3[controlPoints.Length + 2];

            this.internalControlPoints[0] = startAnchor;
            this.internalControlPoints[this.internalControlPoints.Length - 1] = endAnchor;

            for (int i = 0; i < controlPoints.Length; i++)
            {
                this.internalControlPoints[i + 1] = controlPoints[i].Position;
            }

            float length = 0;

            this.lerpCP = new float[controlPoints.Length];

            for (int i = 1; i < controlPoints.Length; i++)
            {
                length += Vector3.Distance(controlPoints[i].Position, controlPoints[i - 1].Position);
                this.lerpCP[i] = length;
            }

            for (int i = 1; i < controlPoints.Length; i++)
            {
                this.lerpCP[i] /= length;
            }
        }

        protected CatmullRomSpline()
        {
        }

        public Vector3 GetPosition(float lerp)
        {
            lerp = MathHelper.Clamp(lerp, 0, 1);

            int index = 0;

            for (int i = this.lerpCP.Length - 1; i >= 0; i--)
            {
                index = i;
                if (this.lerpCP[i] < lerp)
                {
                    break;
                }
            }

            lerp = (lerp - this.lerpCP[index]) / (this.lerpCP[index + 1] - this.lerpCP[index]);

            Vector3 p0 = this.internalControlPoints[index + 0];
            Vector3 p1 = this.internalControlPoints[index + 1];
            Vector3 p2 = this.internalControlPoints[index + 2];
            Vector3 p3 = this.internalControlPoints[index + 3];

            return Vector3.CatmullRom(p0, p1, p2, p3, lerp);
        }
    }
}