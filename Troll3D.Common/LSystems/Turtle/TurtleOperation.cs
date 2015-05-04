using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Common.LSystems
{

    public enum TurtleOperationType{
        DRAW_SEGMENT,
        POP_AND_TURN,
        PUSH_AND_TURN,
        TURN_NEGATIV,
        TURN_POSITIV
    }

    public class TurtleOperation{

        public TurtleOperationType GetType()
        {
            return m_type;
        }

        TurtleOperationType m_type;
    }
}
