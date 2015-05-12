using System.Collections.Generic;
using SharpDX;
using Troll3D;
using Troll3D.Components;
using SharpDX.Direct3D11;

namespace Troll3D
{
    public class TileMap : TComponent
    {
        public TileMap()
        {
            Type = ComponentType.TileMap;
        }

        public override void Attach( Entity entity )
        {
            m_entity = entity;
        }

        public void SetTileMap( int width, int height, TileSet tileset, float tilewidth = 1.0f, float tileheight = 1.0f )
        {
            tilemapdesc_ = new TilemapDesc();

            Width       = width;
            Height      = height;

            tileset_ = tileset;

            TileSetWidth    = tileset.Width;
            TileSetHeight   = tileset.Height;

            tilemapdesc_.TileWidth  = tileset.tilewidth_;
            tilemapdesc_.TileHeight = tileset.tileheight_;

            tilewidth_  = tilewidth;
            tileheight_ = tileheight;

            UpdateSize();

            InitializeTiles();
            InitializeTileMapTexture();

            // Ne pas oublier d'utiliser un sampler "pixel perfect" pour la tilemap
            SamplerState state = new SamplerState( ApplicationDX11.Instance.Device, new SamplerStateDescription()
            {
                AddressU = TextureAddressMode.Wrap,
                AddressV = TextureAddressMode.Wrap,
                AddressW = TextureAddressMode.Wrap,
                BorderColor = new Color4( 0.0f, 1.0f, 0.0f, 1.0f ),
                ComparisonFunction = Comparison.LessEqual,
                Filter = Filter.MinLinearMagMipPoint,
                MaximumAnisotropy = 0,
                MaximumLod = 0,
                MinimumLod = 0,
                MipLodBias = 0
            } );

            tilemapmaterial_ = new MaterialDX11( "vDefault.cso", "pTileMapping.cso", "gDefault.cso" );
            tilemapmaterial_.samplers.Add( state );
            tilemapmaterial_.AddConstantBuffer<TilemapDesc>( tilemapdesc_ );
            tilemapmaterial_.AddTexture( tileset_.texture_ );
            tilemapmaterial_.AddTexture( tilemaptex_.GetTexture2D() );

            if ( m_meshRenderer == null )
            {
                m_meshRenderer = new MeshRenderer( tilemapmaterial_, Quad.GetMesh() );
                m_entity.AddComponent( m_meshRenderer );
            }
            else
            {
                m_meshRenderer.material_ = tilemapmaterial_;
                m_meshRenderer.model_ = Quad.GetMesh();
            }
        }

        public override void Update()
        {
            UpdateTiles();
            ( ( CBuffer<TilemapDesc> )( tilemapmaterial_.constantBuffer ) ).UpdateStruct( tilemapdesc_ );
            tilemapmaterial_.SetTexture( 1, tilemaptex_.GetTexture2D() );
        }

        public void SetTileVal( int x, int y, int val )
        {
            Tile tile = GetTile( x, y );
            if ( tile != null )
            {
                tile.TilesetVal = val;
                updatedTiles_.Push( tile );
            }
        }

        public void SetTileVal( float x, float y, int val )
        {
            Tile tile = GetTile( x, y );
            if ( tile != null )
            {
                tile.TilesetVal = val;
                updatedTiles_.Push( tile );
            }
        }

        public Tile GetTile( float x, float y )
        {
            Vector2 offset = Offset();

            x = x - offset.X;
            y = y - offset.Y;

            int intx = ( int )( x / tilewidth_ );
            int inty = ( int )( y / tileheight_ );

            return GetTile( intx, inty );
        }

        public Vector2 Offset()
        {
            return new Vector2(
                m_entity.transform_.WorldPosition().X - ( Width * tilewidth_ ) / 2.0f,
                m_entity.transform_.WorldPosition().Y - ( Height * tileheight_ ) / 2.0f );
        }

        public Tile GetTile( int x, int y )
        {
            if ( x < Width && x >= 0 && y >= 0 && y < Height )
            {
                return tiles_[( y * Width ) + x];
            }
            return null;
        }

        /// <summary>
        /// TileWidth correspond à la "taille" d'une tuile sur l'axe X
        /// </summary>
        /// <returns></returns>
        public float TileWidth
        {
            get { return tilewidth_; }
            set { tilewidth_ = value; UpdateSize(); }
        }

        /// <summary> TileHeight correspond à la "taille" d'une tuile sur l'axe y </summary>
        /// <returns></returns>
        public float TileHeight
        {
            get { return tileheight_; }
            set { tileheight_ = value; UpdateSize(); }
        }

        /// <summary>
        ///  Width correspond au nombre de tuiles sur l'axe X
        /// </summary>
        public int Width
        {
            get { return ( int )tilemapdesc_.TilemapWidth; }
            set { tilemapdesc_.TilemapWidth = value; }
        }

        /// <summary>
        ///  Height correspond au nombre de tuiles sur l'axe Y
        /// </summary>
        public int Height
        {
            get { return ( int )tilemapdesc_.TilemapHeight; }
            set { tilemapdesc_.TilemapHeight = value; }
        }

        public float TileSetWidth
        {
            get { return tilemapdesc_.TilesetWidth; }
            set { tilemapdesc_.TilesetWidth = value; }
        }

        public float TileSetHeight
        {
            get { return tilemapdesc_.TilesetHeight; }
            set { tilemapdesc_.TilesetHeight = value; }
        }

        private void InitializeTiles()
        {
            tiles_ = new List<Tile>();
            updatedTiles_ = new Stack<Tile>();

            for ( int i = 0; i < Height; i++ )
            {
                for ( int j = 0; j < Width; j++ )
                {
                    tiles_.Add( new Tile( j, i ) );
                }
            }
        }

        /// <summary>
        /// Initialise la texture qui contient les indices correspondant aux "sprites" que l'on souhaite afficher
        /// </summary>
        private void InitializeTileMapTexture()
        {
            tilemaptex_ = new TImage( Width, Height );

            for ( int i = 0; i < Height; i++ )
            {
                for ( int j = 0; j < Width; j++ )
                {
                    tilemaptex_.SetPixel( j, i, 0.0f/1000.0f, 0.0f, 0.0f, 0.0f );
                }
            }
        }

        private void UpdateTiles()
        {
            if ( updatedTiles_.Count > 0 )
            {
                while ( updatedTiles_.Count > 0 )
                {
                    Tile tile = updatedTiles_.Pop();
                    tilemaptex_.SetPixel(
                        tile.X,
                        tile.Y,
                        tile.TilesetVal / 1000.0f, // je vais arbitrairement partir du principe qu'il n'y a pas plus de 1000
                        0.0f,                       // valeurs pour le tileset
                        0.0f,
                        1.0f );
                }
            }
        }

        private void UpdateSize()
        {
            m_entity.transform_.SetScale( new Vector3( tilewidth_ * Width, tileheight_ * Height, 1.0f ) );
        }

        private Entity m_entity;
        private MeshRenderer m_meshRenderer;

        public TilemapDesc tilemapdesc_;
        private TileSet tileset_;
        private MaterialDX11 tilemapmaterial_;

        private Stack<Tile> updatedTiles_;
        public List<Tile> tiles_;

        private TImage tilemaptex_;

        private float tilewidth_;
        private float tileheight_;
    }
}