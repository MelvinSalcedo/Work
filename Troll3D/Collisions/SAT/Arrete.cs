using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{
    // Une arrete représente tout simplement 2 point d'une forme géométrique
    public class Arrete {

        // Public

            // Lifecycle

                public Arrete(Vector3 u, Vector3 v) {
                    u_ = u;
                    v_ = v;
                }

            // Datas

                public Vector3 u_;
                public Vector3 v_;
    }
}
