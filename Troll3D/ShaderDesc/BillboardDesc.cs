using SharpDX;
using System.Runtime.InteropServices;

namespace Troll3D
{

    [StructLayout(LayoutKind.Explicit, Size = 48)]
    public struct BillboardDesc{

        // Public

        // Datas	
        

            [FieldOffset(0)]
            public Vector3 CameraUpVector;

            [FieldOffset(16)]
            public Vector3 CameraRightVector;

            [FieldOffset(32)]
            public Vector3 ScaleValue;
    }
}
