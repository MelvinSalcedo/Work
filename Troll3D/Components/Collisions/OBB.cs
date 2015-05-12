using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace Troll3D.Components.Collisions
{
    /// <summary>
    /// Représente une boite de collision englobante de type Oriented bouding box
    /// </summary>
    public class OBB : Collider
    {
        public OBB()
        {
            ColliderType = BoundingType.OBB;
        }

        public override void Update()
        {
            base.Update();
            transform_.Update();
        }

        public void Init( float width, float height, float depth )
        {
            Mesh = Cube.Mesh;

            transform_ = new Transform();

            transform_.Parent = ( Transform )Entity.GetComponent( ComponentType.Transform );
            
            min_ = new Vector3( -width / 2.0f, -height / 2.0f, -depth / 2.0f );
            max_ = new Vector3( width / 2.0f, height / 2.0f, depth / 2.0f );

            //modelrenderer_ = new MeshRenderer(CollisionManager.Instance.material_, Cube.GetMesh());
            //modelrenderer_.SetFillMode(SharpDX.Direct3D11.FillMode.Wireframe);

            transform_.SetScale( width, height, depth );
            transform_.Update();
            min_ = new Vector3( -0.5f, -0.5f, -0.5f );
            max_ = new Vector3( 0.5f, 0.5f, 0.5f );
        }

        public override void Attach( Entity entity ) 
        {
            base.Attach( entity );

            if ( entity.GetComponent( ComponentType.MeshRenderer ) == null )
            {
                Init( 1.0f, 1.0f, 1.0f );
            }
        }

        //public override bool Collide(BoundingForm b) {
        //    Console.WriteLine("OBB Collide BoundingFOrm");
        //    return true;
        //}

        public override bool Collide(OBB obb) 
        {
            return CollisionManager.OBBtoOBB(this, obb);
        }

        public override bool Collide(SphereCollider sphere) 
        {
            return CollisionManager.OBBtoBoundingSphere(this, sphere);
        }

        public Shape GetShape()
        {
            Vector3 maxmin = max_ - min_;

            // On récupère les sommets dans l'espace réel
            Vector3 [] vertices = new Vector3[8];

            vertices[0] = ( Vector3 )Vector3.Transform( min_, transform_.worldmatrix_ );
            vertices[1] = ( Vector3 )Vector3.Transform( min_ + new Vector3( maxmin.X, 0.0f, 0.0f ), transform_.worldmatrix_ );
            vertices[2] = ( Vector3 )Vector3.Transform( min_ + new Vector3( 0.0f, maxmin.Y, 0.0f ), transform_.worldmatrix_ );
            vertices[3] = ( Vector3 )Vector3.Transform( min_ + new Vector3( maxmin.X, maxmin.Y, 0.0f ), transform_.worldmatrix_ );

            vertices[4] = ( Vector3 )Vector3.Transform( min_ + new Vector3( 0.0f, 0.0f, maxmin.Z ), transform_.worldmatrix_ );
            vertices[5] = ( Vector3 )Vector3.Transform( min_ + new Vector3( maxmin.X, 0.0f, maxmin.Z ), transform_.worldmatrix_ );
            vertices[6] = ( Vector3 )Vector3.Transform( min_ + new Vector3( 0.0f, maxmin.Y, maxmin.Z ), transform_.worldmatrix_ );
            vertices[7] = ( Vector3 )Vector3.Transform( max_, transform_.worldmatrix_ );
                    
            // On récupère les arrêtes

            Vector3[] axes= new Vector3[3];

            axes[0] = transform_.GetUpVector();
            axes[1] = transform_.GetRightVector();
            axes[2] = transform_.GetForwardVector();

            return new Shape(vertices, axes);
        }

        // Position du point "minimun"
        public Vector3 min_;

        // Position du point "Maximum"
        public Vector3 max_;

        public Shape shape_;

        
    }
}
