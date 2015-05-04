using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D{

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct HBlurDesc{

        // Public

            // Lifecycle

                public HBlurDesc(int imageWidth, int blurSize){
                    ImageWidth  = imageWidth;
                    BlurSize    = blurSize;
                }

        // Datas

            [FieldOffset(0)]
            public int ImageWidth;

            [FieldOffset(4)]
            public int BlurSize;
    }
}
