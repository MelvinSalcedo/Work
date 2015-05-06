using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelaunayTriangularisation.WingedEdge
{
    /// <summary>
    /// Représente une arrête d'un maillage de type Winged Edge
    /// </summary>
    public class EdgeWE
    {
        public EdgeWE( VertexWE v1, VertexWE v2 )
        {
            vertex1 = v1;
            vertex2 = v2;
        }

        public VertexWE vertex1;
        public VertexWE vertex2;

        public FaceWE faceA;
        public FaceWE faceB;

        // Clockwise ordering
        public EdgeWE previousA;
        public EdgeWE previousB;

        public EdgeWE nextA;
        public EdgeWE nextB;
    }
}
