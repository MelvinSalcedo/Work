using System;
using System.Collections.Generic;
using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D
{
    /// <summary>
    /// Utilisation d'une structure décrivant les données d'une tilemap. Structure qui est ensuite
    /// envoyé aux GPU
    /// </summary>
    [StructLayout( LayoutKind.Explicit, Size = 32 )]
    public struct TilemapDesc
    {

        [FieldOffset( 0 )]
        public float TilesetWidth;

        [FieldOffset( 4 )]
        public float TilesetHeight;

        [FieldOffset( 8 )]
        public float TileWidth;

        [FieldOffset( 12 )]
        public float TileHeight;

        [FieldOffset( 16 )]
        public float TilemapWidth;

        [FieldOffset( 20 )]
        public float TilemapHeight;
    }
}
