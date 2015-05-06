using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelaunayTriangularisation.WingedEdge
{
    /// <summary>
    /// Représente une face d'un maillage Winged Edge
    /// </summary>
    public class FaceWE
    {
        public List<EdgeWE> Edges = new List<EdgeWE>();
    }
}
