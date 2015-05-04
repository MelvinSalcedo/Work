using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D{

    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public struct TextureConstantBufferDesc{

        // Public

            // Datas

            [FieldOffset(0)]
            public float XOffset;

            [FieldOffset(4)]
            public float YOffset;

            [FieldOffset(8)]
            public float TilingWidth;

            [FieldOffset(12)]
            public float TilingHeight;

            [FieldOffset(16)]
            public Vector4 MainColor;

            [FieldOffset(32)]
            public bool HasTexture;
    }

}
