using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components;

namespace Troll3D.Rendering
{
    /// <summary> Une fois qu'un render target à été défini, le viewport permet de déterminer "ou" l'on dessine à l'intérieur de ce dernier
    ///  Par conséquence, il est possible (pour faire du "splitscreen" par exemple), d'avoir plusieurs viewport pour un même
    ///  RenderTarget
    /// </summary>
    public class Viewport
    {
        public Viewport( int x, int y, int width, int height )
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public virtual void Render() { }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Camera Camera;
    }
}
