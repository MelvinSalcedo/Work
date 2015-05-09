using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D;
using Troll3D.Components;
using SharpDX;
using SharpDX.Direct3D11;
using Troll3D.Common.Maths;

namespace NoiseGeneration
{
    public class PerlinBehaviour : Behaviour
    {
        public void Init(int width, int height)
        {
            Width = width;
            Height = width;

            m_renderer = Entity.AddComponent<MeshRenderer>();
            m_renderer.model_       = Quad.GetMesh();
            m_renderer.material_    = new MaterialDX11();

            m_image = new TImage( Width, Height);
            SamplerState state = new SamplerState( ApplicationDX11.Instance.device_, new SamplerStateDescription()
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

            for ( int i = 0; i < m_image.Width; i++ )
            {
                for ( int j = 0; j < m_image.Height; j++ )
                {
                    m_image.SetPixel( j, i, 1.0f, 0.0f, 0.0f, 1.0f );
                }
            }
            m_renderer.material_.samplers.Add( state );
            m_renderer.material_.AddTexture( m_image.GetTexture2D() );

            GeneratePerlinNoise();

        }

        public override void OnKeyDown( Troll3D.KeyboardEvent e )
        {
            if ( e.keycode_ == Troll3D.KeyCode.Key_S )
            {
                GeneratePerlinNoise();
            }
        }

        public void GeneratePerlinNoise()
        {
            Random seed = new Random();
            m_image.SetData(PerlinNoise.GetDatas( 4, 2, seed.Next(), Height ));

            m_renderer.material_.SetTexture( 0, m_image.GetTexture2D() );

        }

        public override void Update()
        {
            base.Update();
        }

        public int Width    { get; private set; }
        public int Height   { get; private set; }

        MeshRenderer    m_renderer;
        TImage          m_image;
    }
}
