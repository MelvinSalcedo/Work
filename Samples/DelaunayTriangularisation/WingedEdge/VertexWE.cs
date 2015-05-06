using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

namespace DelaunayTriangularisation.WingedEdge
{
    /// <summary>
    /// Représente un sommet d'un maillage de type Winged Edge
    /// </summary>
    public class VertexWE
    {
        public VertexWE( float x, float y, float z )
        {
            Position = new Vector3( x, y, z );
        }

        public VertexWE( Vector3 v )
        {
            Position = v;
        }

        public int Id;
        public List<EdgeWE> Edges = new List<EdgeWE>();
        public Vector3 Position;
    }
}
