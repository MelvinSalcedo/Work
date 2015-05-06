using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components.Collisions;
using Troll3D.Components;

namespace Troll3D
{
    public enum CollisionType 
    {
        CollisionEnter,
        CollisionExit,
        Colliding
    }

    // Contient les informations permettant de traiter une collisions
    // entre 2 boites englobantes
    public class CollisionEvent {

        public CollisionEvent( Collider a, Collider b, CollisionType type ) 
        {
            type_ = type;
            a_ = a;
            b_ = b; 
        }

        /// <summary> Première forme </summary>
        public Collider a_;

        /// <summary> Deuxième forme</summary>
        public Collider b_;

        public CollisionType type_;
    }
}
