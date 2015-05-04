using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Common.Graphs;
using Troll3D.Common.Maths;

namespace Troll3D.Common.IA.PathFinding
{
    public class AStarNode : Node
    {
        public AStarNode(short id, int x, int y) : base(id)
        {
            Position = new Vec2( x, y );
        }

        public Vec2 Position { get; private set; }
    }
}
