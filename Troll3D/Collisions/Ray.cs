using System;
using System.Collections.Generic;
using SharpDX;

using Troll3D.Components;
using Troll3D.Components.Collisions;

namespace Troll3D
{
    /// <summary>
    /// Représente un segment, droite ou demi droite, qui peut être utilisé pour détecter des collisions avec
    /// différents objets ( colliders principalement)
    /// </summary>
    public class TrollRay
    {

        /// <summary>
        /// Construit un nouveau segment [A,B]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public TrollRay( Vector3 a, Vector3 b)
        {
            direction_ = ( b- a);
            direction_.Normalize();
            start_ = a;
            Length = ( b- a).Length();
        }

        /// <summary>
        /// Construit une demi droite de direction u
        /// </summary>
        /// <param name="startpoint"></param>
        /// <param name="direction"></param>
        /// <param name="length"></param>
        public TrollRay( Vector3 startpoint, Vector3 direction, float length = 1000.0f )
        {
            start_ = startpoint;
            direction_ = direction;
            Length = length;
        }

        public RaycastResult Fire()
        {
            Vector3 closestpoint = new Vector3( 0.0f, 0.0f, 0.0f );
            Entity closestEntity = null;
            CheckColliders(ref closestpoint, ref closestEntity );
            return new RaycastResult( closestEntity, closestpoint );
        }

        public Vector3 direction_;
        public Vector3 start_;
        public float Length;

        /// <summary>
        /// Vérifie si une collision a lieu entre le rayon et les colliders présent dans la scène
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="closestpoint"></param>
        /// <param name="closestEntity"></param>
        private void CheckColliders( ref Vector3 closestpoint, ref Entity closestEntity )
        {
            foreach ( Collider collider in CollisionManager.Instance.m_colliders )
            {
                Vector3 intersection = new Vector3( 0.0f, 0.0f, 0.0f );
                Vector3 normal = new Vector3();
                // Si le trait rentre en collision avec le mesh, on récupère le point d'intersection
                if ( TRaycast.IntersectWithMesh( this, collider, ref intersection, ref normal ) )
                {
                    if ( closestpoint == Vector3.Zero )
                    {
                        closestpoint = intersection;
                        closestEntity = collider.Entity;
                    }
                    else
                    {
                        if ( ( start_ - intersection ).Length() < ( start_ - closestpoint ).Length() )
                        {
                            closestpoint = intersection;
                            closestEntity = collider.Entity;
                        }
                    }
                }
            }
        }
    }
}

//this.m_Direction = (endPoint.Minus(startPoint)).Normalize();
//this.m_StartPosition = startPoint;
//this.m_Length = length;