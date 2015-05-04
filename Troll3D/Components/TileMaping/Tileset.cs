using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D
{

    public class TileSet
    {
        public TileSet( Texture2D texture, int tilewidth, int tileheight )
        {
            texture_ = texture;

            xratio_ = ( float )tilewidth / texture.Description.Width;
            yratio_ = ( float )tileheight / texture.Description.Height;

            Width       = texture.Description.Width;
            Height      = texture.Description.Height;
            tilewidth_  = tilewidth;
            tileheight_ = tileheight;
        }

        public float Width { get; private set; }
        public float Height { get; private set; }

        public float OffetX( int x )
        {
            return ( float )x * xratio_;
        }

        public float OffsetY( int y )
        {
            return ( float )y * yratio_;
        }

        public Texture2D texture_;

        public int countx_;
        public int county_;

        public int tilewidth_;
        public int tileheight_;

        public float xratio_;
        public float yratio_;


    }

}
