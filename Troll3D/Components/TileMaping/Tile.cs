using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D
{
    /// <summary>
    /// Représente une tuile de la tilemap. Sa valeur correspond à un indice qui sera utilisé
    /// pour identifier la tuile par rapport à son tileset
    /// </summary>
    public class Tile
    {
        /// <summary>
        /// Création d'une nouvelle tuile, de coordonnée x-y
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="tilesetval"></param>
        public Tile( int x, int y, int tilesetval = 0 )
        {
            X = x;
            Y = y;
            TilesetVal= tilesetval;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public float TilesetVal { get; set; }
    }
}
