using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

using Troll3D.Components;

namespace Troll3D
{

    /// <summary>
    /// Cette classe permettra d'afficher du texte sur un ou plusieurs quad (à voir )
    /// </summary>
    public class TextQuad : Entity
    {

        public TextQuad( FontAtlas font, string text, Entity parent = null )
            : base( parent )
        {
            atlasfont = font;
            SetText( text );
        }

        public bool IsOnScreenSpace;

        public void SetOnScreenSpace()
        {
            Width = 100;
            Height = 100;
            IsOnScreenSpace = true;
            ResizeForScreenSize();
        }

        public void DrawLetter( Entity ent )
        {
            WorldViewProj wvp = new WorldViewProj()
            {
                World = transform_.localmatrix_,
                View = Matrix.Identity,
                Projection = Matrix.Identity
            };

            transform_.SetWVP( wvp );
            //ent.modelrenderer_.Draw();
        }

        //public override void Draw()
        //{
        //    if ( IsOnScreenSpace )
        //    {
        //        WorldViewProj wvp = new WorldViewProj()
        //        {
        //            World = transform_.localmatrix_,
        //            View = Matrix.Identity,
        //            Projection = Matrix.Identity
        //        };

        //        transform_.SetWVP( wvp );

        //        for ( int i = 0; i < sons_.Count; i++ )
        //        {
        //            DrawLetter( sons_[i] );
        //        }

        //    }
        //    else
        //    {
        //        base.Draw();
        //    }
        //}

        public void ResizeForScreenSize()
        {
            transform_.SetPosition(
                -( ( ( float )Screen.Instance.Width/ 2.0f - ( float )Width / 2.0f ) ) / ( float )Screen.Instance.Width* 2.0f,
                -( ( ( float )Screen.Instance.Height/ 2.0f - ( float )Height / 2.0f ) ) / ( float )Screen.Instance.Height* 2.0f,
                0.31f
                );
            // transform_.RotationEuler(3.141592f / 5.0f, 3.141592f / 5.0f, 3.141592f / 5.0f);
            transform_.SetScale( ( float )Width / ( float )Screen.Instance.Width* 2.0f, ( float )Height / ( float )Screen.Instance.Height* 2.0f, 1.0f );
            transform_.Update();
        }


        public void SetText( string text )
        {

            float offset = 0.0f;

            Width = 0.0f;
            Height = 0.0f;

            for ( int i = 0; i < text.Length; i++ )
            {

                float coef = 0.1f;

                AtlasNode atlasnode = atlasfont.atlas.GetNode( text[i] );

                MaterialDX11 mat = new MaterialDX11( "vDefault.cso", "pText.cso" );
                mat.SetMainColor( 1.0f, 0.0f, 0.0f, 1.0f );

                mat.AddShaderResourceView( atlasfont.atlas.SRV );

                Entity entity = new Entity();

                // On utilise XOffset et xadvance pour déterminer la position des caractères dans la ligne
                offset += atlasnode.XOffset * coef;

                entity.transform_.Translate(
                    offset + ( ( float )atlasnode.Width / 2.0f * coef ),
                    -( atlasnode.Height * coef / 2.0f ) - atlasnode.YOffset * coef,
                    0.0f );

                offset += atlasnode.XAdvance * coef;

                entity.transform_.SetScale(
                    atlasnode.Width * coef,
                    atlasnode.Height * coef,
                    1.0f );

                Width += atlasnode.Width * coef + atlasnode.XOffset * coef + atlasnode.XAdvance * coef;
                Height = atlasnode.Height * coef + atlasnode.YOffset * coef;

                Append( entity );

                //entity.modelrenderer_ = new MeshRenderer( mat, Quad.GetMesh() );

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
                mat.samplers.Add( state );
                mat.SetTextureXOffset( ( float )atlasnode.X / ( float )atlasfont.atlas.Width );
                mat.SetTextureYOffset( ( float )atlasnode.Y / ( float )atlasfont.atlas.Height );
                mat.SetTextureWidth( ( float )atlasnode.Width / ( float )atlasfont.atlas.Width );
                mat.SetTextureHeight( ( float )atlasnode.Height / ( float )atlasfont.atlas.Height );

            }

            if ( CenterText )
            {
                transform_.Translate( -Width / 4.0f, Height / 2.0f, 0.0f );
            }
        }

        public FontAtlas atlasfont;

        public bool CenterText = true;

        public float Width { get; private set; }
        public float Height { get; private set; }
    }
}
