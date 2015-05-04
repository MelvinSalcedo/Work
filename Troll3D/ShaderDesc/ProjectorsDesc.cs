using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpDX;

namespace Troll3D
{

    // On va dire qu'il y a 10 lumière max pour l'instant
    [StructLayout(LayoutKind.Explicit, Size = 16 + (192) * 10)]
    public struct ProjectorsDesc{

        // Public

            // Datas

                [FieldOffset(0)]
                public int ProjectorCount;

                [FieldOffset(16), MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
                public ProjectorDesc[] Projectors;

    }
}
