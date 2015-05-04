using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

using Troll3D.Components;

namespace Troll3D.src.Component.Text
{
    /// <summary> La différence avec TextQuad est qu'un TextQuad contient plusieurs fontQuad </summary>
    public class FontQuad : Entity
    {

        public FontQuad(FontAtlas fontatlas, char character, Entity parent=null) : base(parent) {
            m_fontAtlas = fontatlas;
            m_character = character; 
        }

            private void Initialize(){

                AtlasNode atlasnode = m_fontAtlas.atlas.GetNode(m_character);

                MaterialDX11 mat = new MaterialDX11("vDefault.cso", "pText.cso");
                mat.SetMainColor(1.0f, 0.0f, 0.0f, 1.0f);

                mat.AddShaderResourceView(m_fontAtlas.atlas.SRV);


                transform_.Translate(
                    ((float)atlasnode.Width / 2.0f ),
                    -(atlasnode.Height  / 2.0f) - atlasnode.YOffset ,
                    0.0f);

                transform_.SetScale(
                    atlasnode.Width,
                    atlasnode.Height,
                    1.0f);

                //modelrenderer_ = new MeshRenderer(mat, Quad.GetMesh());

                SamplerState state = new SamplerState(ApplicationDX11.Instance.device_, new SamplerStateDescription()
                {
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,
                    BorderColor = new Color4(0.0f, 1.0f, 0.0f, 1.0f),
                    ComparisonFunction = Comparison.LessEqual,
                    Filter = Filter.MinLinearMagMipPoint,
                    MaximumAnisotropy = 0,
                    MaximumLod = 0,
                    MinimumLod = 0,
                    MipLodBias = 0
                });
                mat.samplers.Add(state);
                mat.SetTextureXOffset((float)atlasnode.X / (float)m_fontAtlas.atlas.Width);
                mat.SetTextureYOffset((float)atlasnode.Y / (float)m_fontAtlas.atlas.Height);
                mat.SetTextureWidth((float)atlasnode.Width / (float)m_fontAtlas.atlas.Width);
                mat.SetTextureHeight((float)atlasnode.Height / (float)m_fontAtlas.atlas.Height);


            }

            private FontAtlas   m_fontAtlas;
            private char        m_character;
    }
}
