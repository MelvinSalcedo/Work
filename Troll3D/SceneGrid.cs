using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;
using Troll3D.Components;


namespace Troll3D
{
    /// <summary>La Scene Grid est tout simplement la grille positionné sur le plan P(0,0,0) et de vecteur directeur (1,0,0) et (0,0,1)
    /// Elle permettra de repérer le plan "de base" et de mieux mettre en perspective les objets 3D à l'aide d'une grille
    /// dont les dimensions évolues en fonction de la distance de la caméra principale
    /// </summary>
    public class SceneGrid : Entity
    {
        public SceneGrid()
        {
            MaterialDX11 mat = new MaterialDX11();
            TImage timage = new TImage( 401, 401 );

            for ( int i = 0; i < timage.Height; i++ )
            {
                for ( int j = 0; j < timage.Width; j++ )
                {
                    if ( i % 40 == 0 || j % 40 == 0 || i == 1 || i == 399 || j == 1 || j == 399 )
                    {
                        timage.SetPixel( j, i, 1.0f, 1.0f, 1.0f, 1.0f );
                    }
                    else
                    {
                        timage.SetPixel( j, i, 0.0f, 0.0f, 0.0f, 0.0f );
                    }
                }
            }

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


            float scaleValue = 40.0f;

            mat.AddTexture( timage.GetTexture2D() );
            mat.SetTextureHeight( 0.1f * scaleValue );
            mat.SetTextureWidth( 0.1f * scaleValue );
            mat.samplers.Add( state );
            //modelrenderer_ = new MeshRenderer( mat, Quad.GetMesh() );
            transform_.RotateEuler( 0.0f, 3.141592f / 2.0f, 0.0f );
            transform_.SetScale( scaleValue, scaleValue, 1.0f );

            IsPickingActivated = false;
        }
    }
}
