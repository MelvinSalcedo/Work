using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    // Un "Ray", et un segment qui peut être utilisé pour détecter les collisions avec les différents objets de la scène
    public class TrollRay {

        public TrollRay(Vector3 startpoint, Vector3 endpoint) 
        {
            direction_ = (endpoint - startpoint);
            direction_.Normalize();
            start_ = startpoint;
            length_ = (endpoint-startpoint).Length();
        }

        public TrollRay(Vector3 startpoint, Vector3 direction, float length=1000.0f)
        {
            start_          = startpoint;
            direction_      = direction;
            length_         = length;
        }

        public void CheckEntity(Entity entity, ref Vector3 closestpoint, ref Entity closestEntity)
        {
            //if (entity.modelrenderer_ != null && entity.IsPickingActivated && entity.modelrenderer_.model_.GetType()== MeshType.TRIANGLE_MESH)
            //{
            //    Vector3 intersection = new Vector3(0.0f, 0.0f, 0.0f);
            //    Vector3 normal = new Vector3() ;
            //    // Si le trait rentre en collision avec le mesh, on récupère le point d'intersection
            //    if (TRaycast.IntersectWithMesh(this, entity, ref intersection, ref normal)){

            //        if (closestpoint == Vector3.Zero) 
            //        {
            //            closestpoint = intersection;
            //            closestEntity   = entity;
            //        }else
            //        {
            //            if ((start_ - intersection).Length() < (start_ - closestpoint).Length())
            //            {
            //                closestpoint= intersection;
            //                closestEntity   = entity;
            //            }
            //        }
            //    }
            //}

            for (int i = 0; i < entity.sons_.Count; i++)
            {
                CheckEntity(entity.sons_[i], ref closestpoint, ref closestEntity);
            }
        }

        public RaycastResult Fire()
        {
            Vector3 closestpoint    =   new Vector3(0.0f,0.0f,0.0f);
            Entity  closestEntity   =   null;

            for (int i = 0; i < ApplicationDX11.Instance.scene_.AllEntities.Count; i++)
            {
                CheckEntity( ApplicationDX11.Instance.scene_.AllEntities[i], ref closestpoint, ref closestEntity );
            }

            return new RaycastResult(closestEntity, closestpoint);
        }

        public Vector3 direction_;
        public Vector3 start_;
        public float length_;

    }
}

        //this.m_Direction = (endPoint.Minus(startPoint)).Normalize();
        //this.m_StartPosition = startPoint;
        //this.m_Length = length;