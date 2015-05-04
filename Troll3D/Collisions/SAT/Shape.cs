using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D{

    // La classe Shape est principalement utilisé en corelation avec le SAT. Elle contient les sommets et les arrêtes 
    // à tester pour la classe SAT
    public class Shape{

        // Public

            // Lifecycle

                public Shape() { }
                public Shape(Vector3[] vertices, Vector3[] axes) {
                    vertices_   = vertices;
                    axes_       = axes;
                }

            // Methods

            // Datas

                public Vector3[]    vertices_;
                public Vector3[]    axes_;
    }
}
