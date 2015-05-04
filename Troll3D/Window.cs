using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Troll3D.Rendering;

namespace Troll3D
{
    public class Window
    {
        public void CreateNewWindow( int type )
        {
            throw new NotImplementedException();
        }

        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public RenderTarget RenderTarget { get; protected set; }
    }
}
