using SharpDX;
using System;
using System.Runtime.InteropServices;

namespace Troll3D{

    [StructLayout(LayoutKind.Explicit, Size = 272)]
    public struct WorldViewProj {

        // Public

            // Datas

                [FieldOffset(0)]
                public Matrix World;

                [FieldOffset(64)]
                public Matrix View;

                [FieldOffset(128)]
                public Matrix Projection;

                [FieldOffset(192)]
                public Matrix WorldInverse;

                [FieldOffset(256)]
                public Vector3 CameraPosition;

    }
}
