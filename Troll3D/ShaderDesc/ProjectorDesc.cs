using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpDX;

namespace Troll3D
{

    // On va dire qu'il y a 10 lumière max pour l'instant
    [StructLayout(LayoutKind.Explicit, Size = 192)]
    public struct ProjectorDesc
    {

        // Public

        // Datas

        [FieldOffset(0)]
        public Matrix Projection;

        [FieldOffset(64)]
        public Matrix Transformation;

        [FieldOffset(128)]
        public Matrix Inverse;

    }
}
