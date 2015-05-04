using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D
{

    /// <summary>
    /// Représente une texture stocké dans l'atlas de texture.
    /// Permet de retrouver ses coordonnées (offset et taille)
    /// </summary>
    public class AtlasNode
    {
        public AtlasNode( int xoffset, int yoffset, int width, int height, int x, int y, int id, int xadvance )
        {
            XOffset     = xoffset;
            YOffset     = yoffset;
            Width       = width;
            Height      = height;
            X           = x;
            Y           = y;
            XAdvance    = xadvance;
        }

        public int XOffset  { get; private set; }
        public int YOffset  { get; private set; }
        public int Width    { get; private set; }
        public int Height   { get; private set; }
        public int Id       { get; private set; }
        public int X        { get; private set; }
        public int Y        { get; private set; }
        public int XAdvance { get; private set; }

    }
}
