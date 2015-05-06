using System;
using System.Collections.Generic;
using SharpDX;

using Troll3D.Components;

namespace Troll3D.Components.Collisions
{

    public class SphereCollider : Collider
    {
        public SphereCollider(Vector3 position, float radius) 
        {
            ColliderType    = BoundingType.Bounding_Sphere;
            //transform.Translate(position);
            Radius         = radius;
            //modelrenderer_  = new MeshRenderer(
            //    CollisionManager.Instance.material_,
            //    Sphere.GetMesh(1.0f,10,10));
            //transform_.SetScale(radius, radius, radius);
            //modelrenderer_.SetFillMode(SharpDX.Direct3D11.FillMode.Wireframe);
        }

        public override bool Collide(OBB box) {
            return  CollisionManager.OBBtoBoundingSphere(box, this);
        }

        public override bool Collide(SphereCollider sphere) {
            return CollisionManager.BoundingSphereToBoundingSphere(this, sphere);
        }

        public float Radius { get; set; }
    }
}
