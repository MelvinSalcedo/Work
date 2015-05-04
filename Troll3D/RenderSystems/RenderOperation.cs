using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.RenderSystems
{
    public class RenderOperation
    {
        public enum RenderOperationType
        {
            TRIANGLE_LIST,
            LINE_List
        }

        
        //List<vertices> vertices
        //List<indexer> index; // Si index == null dans ce cas on affiche rien    
        public RenderOperation() { }

    }
}
