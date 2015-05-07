using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelaunayTriangularisation.WingedEdge
{
    /// <summary>
    /// Représente une arrête d'un maillage de type Winged Edge
    /// Dans un maillage dit "manifold", une arrête ne peut être connecté qu'à 1 ou 2 faces
    /// En conséquence, l'arrete enregistre 2 faces, la face "left" et la face "right
    /// De plus, l'arrête enregistre également l'arrête qui suit et précède pour chacune des 2 faces
    /// </summary>
    public class EdgeWE
    {
        public EdgeWE( VertexWE v1, VertexWE v2 )
        {
            Vertex1 = v1;
            Vertex2 = v2;
        }

        public VertexWE Vertex1;
        public VertexWE Vertex2;

        public FaceWE LeftFace;
        public FaceWE RightFace;

        // Clockwise ordering
        public EdgeWE PreviousLeft;
        public EdgeWE PreviousRight;

        public EdgeWE NextLeft;
        public EdgeWE NextRight;
    }
}
