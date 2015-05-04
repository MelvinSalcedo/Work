using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D{


    public enum CollisionType {
        CollisionEnter,
        CollisionExit,
        Colliding
    }

    // Contient les informations permettant de traiter une collisions
    // entre 2 boites englobantes
    public class CollisionEvent {

        public CollisionEvent(BoundingForm a, BoundingForm b, CollisionType type) {
            type_ = type;
            a_ = a;
            b_ = b; 
        }

        /// <summary> Première forme </summary>
        public BoundingForm a_;

        /// <summary> Deuxième forme</summary>
        public BoundingForm b_;

        public CollisionType type_;
    }
}
