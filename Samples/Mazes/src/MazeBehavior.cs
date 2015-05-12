using System;
using System.Collections.Generic;
using Troll3D;
using Troll3D.Common.Mazes;
using SharpDX;
using SharpDX.Direct3D11;

using Troll3D.Components;

namespace Mazes
{
    public class MazeBehavior : Behaviour
    {

        RecursiveBacktracer rec;
        Maze maze;

        int mazeheight = 27;
        int mazewidth = 48;

        bool start = false;

        public override void OnKeyDown( KeyboardEvent e )
        {
            if ( e.keycode_ == KeyCode.Key_S )
            {
                start = true;
            }
        }

        public override void Initialize()
        {
            maze = new Maze( mazewidth, mazeheight );
            rec = new RecursiveBacktracer( maze );

            TImage image = new TImage( maze.Texture, mazewidth * 2 + 1, mazeheight * 2 + 1 );

            MaterialDX11 material = new MaterialDX11( "vDefault.cso", "pUnlit.cso" );

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

            material.samplers.Add( state );
            material.AddTexture( image.GetTexture2D() );

            
            Entity.AddComponent(new MeshRenderer( material, Quad.GetMesh() ));
        }

        public override void Update()
        {
            if ( start )
            {
                rec.DoOneStep();
                TImage image = new TImage( maze.Texture, mazewidth * 2 + 1, mazeheight * 2 + 1 );
                Resource tex = image.GetTexture2D();
                ((MeshRenderer)Entity.GetComponent(ComponentType.MeshRenderer)).material_.SetTexture( 0, tex );
                Utilities.Dispose<Resource>( ref tex );
            }
        }
    }
}
