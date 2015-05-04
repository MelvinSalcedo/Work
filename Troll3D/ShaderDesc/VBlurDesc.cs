using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D{

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    public struct VBlurDesc {

        // Public

            // Lifecycle

                public VBlurDesc(int imageHeight, int blurSize) {
                    ImageHeight =   imageHeight;
                    BlurSize    =   blurSize;
                }

            // Datas

                [FieldOffset(0)]
                public int ImageHeight;

                [FieldOffset(4)]
                public int BlurSize;
    }
}
