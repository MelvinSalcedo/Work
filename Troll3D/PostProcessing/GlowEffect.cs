using System;
using System.Collections.Generic;
using SharpDX;
using SharpDX.Direct3D11;

namespace Troll3D{

    public class GlowEffect{

        // Public

            // Lifecycle

                public GlowEffect(){
                    GlowTexture = null;
                    Description = new GlowDesc(false, false, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
                }

                public GlowEffect(Vector4 glowcolor){
                    GlowTexture = null;
                    Description = new GlowDesc(true, false, glowcolor);
                }

                public GlowEffect(Texture2D tex){
                    GlowTexture = tex;
                    Description = new GlowDesc(true, true, new Vector4(0.0f, 0.0f, 0.0f, 1.0f));
                }

                

            // Method


                public Vector4 GlowColor{
                    get{    return Description.GlowColor ;}
                    set{
                        Description.HasTexture = false;
                        Description.IsGlowing = true;
                        Description.GlowColor = value;
                    }
                }

            // Datas

                public Texture2D    GlowTexture;
                public GlowDesc     Description;

                public float widthTiling = 1.0f;
                public float heightTiling= 1.0f;
    }
}
