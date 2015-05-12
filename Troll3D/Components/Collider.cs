using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components.Collisions;

namespace Troll3D.Components
{

    public enum BoundingType
    {
        OBB,
        Bounding_Sphere
    }

    /// <summary>
    /// Un Collider se charge d'envoyé des informations (de collision) à l'entité auquel il est attaché
    /// </summary>
    public abstract class Collider : TComponent
    {
        public Collider()
        {
            Type = ComponentType.Collider;
            CollisionManager.Instance.m_colliders.Add( this );
            IsActive = true;
        }


        public abstract bool Collide( OBB box );
        public abstract bool Collide( SphereCollider sphere );

        public override void Update(){}

        public override void Attach( Entity entity )
        {
            Entity = entity;
        }

        public int            layer_;
        public BoundingType     ColliderType { get; protected set; }
        public Entity           Entity { get; private set; }
        public bool             IsActive;
        public Entity Link { get; private set; }
        public Transform transform_;

        public Mesh Mesh { get; protected set; }

    }

}
