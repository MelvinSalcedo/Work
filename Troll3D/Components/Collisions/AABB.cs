using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D
{
    /// <summary>
    /// AABB signigie Axis aligned bounding box. Il s'agit tout simplement de boite alignés sur les axes x,y,z
    /// </summary>
    public class AABB
    {
        /// <summary>
        /// Construit une boite englobante pour un contexte 3D, X Y Z correspond au point inférieur gauche
        /// </summary>
        public AABB(float x, float y, float z, float width, float height, float depth)
        {
            X       = x;
            Y       = y;
            Z       = z;
            Width   = width;
            Height  = height;
            Depth   = depth;
        }

        /// <summary>
        /// Construit une boite englobante pour un contexte 2D, XY correspond au point inférieur gauche
        /// </summary>
        public AABB(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Z = 1.0f;
            Width   = width;
            Height  = height;
            Depth   = 1.0f;
        }

        public float X;
        public float Y;
        public float Z;

        public float Width;
        public float Height;
        public float Depth;

    }
}
