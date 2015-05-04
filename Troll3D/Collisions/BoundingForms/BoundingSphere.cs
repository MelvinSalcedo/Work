using System;
using System.Collections.Generic;
using SharpDX;

using Troll3D.Components;

namespace Troll3D{

    public class BoundingSphere : BoundingForm{

        // Public

            // Lifecycles

        public BoundingSphere(Vector3 position, float radius, Entity entity)
            : base(entity) {
                    type_           = BoundingType.Bounding_Sphere;
                    transform_.Translate(position);
                    radius_         = radius;
                    //modelrenderer_  = new MeshRenderer(
                    //    CollisionManager.Instance.material_,
                    //    Sphere.GetMesh(1.0f,10,10));
                    //transform_.SetScale(radius, radius, radius);
                    //modelrenderer_.SetFillMode(SharpDX.Direct3D11.FillMode.Wireframe);
                }

        
            // Methods

            
            // Virtual Methods

                public override bool Collide(OBB box) {
                    return  CollisionManager.OBBtoBoundingSphere(box, this);
                }

                public override bool Collide(BoundingSphere sphere) {
                    return CollisionManager.BoundingSphereToBoundingSphere(this, sphere);
                }

            // Datas
                public float radius_;
    }
}
