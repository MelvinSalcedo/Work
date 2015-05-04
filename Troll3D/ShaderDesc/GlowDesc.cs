using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D{

    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct GlowDesc {

        // Public

            // Lifecycle

                public GlowDesc(bool isGlowing, bool hasTexture, Vector4 glowcolor) {
                    GlowColor   = glowcolor;
                    IsGlowing   = isGlowing;
                    HasTexture  = hasTexture;
                }

            // Datas

                [FieldOffset(0)]
                public bool IsGlowing;

                [FieldOffset(4)]
                public bool HasTexture;

                [FieldOffset(16)]
                public Vector4 GlowColor;

                
    
    }

}
