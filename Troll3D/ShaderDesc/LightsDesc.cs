using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpDX;

namespace Troll3D{

    // On va dire qu'il y a 10 lumière max pour l'instant
    [StructLayout(LayoutKind.Explicit, Size = 32 + (272)*10)]
    public struct LightsDesc {

            // Public

                // Datas

                    [FieldOffset(0)]
                    public int LightCount;

                    [FieldOffset(4)]
                    public Vector3 CameraPosition;

                    [FieldOffset(16)]
                    public float AcneBias;

                    [FieldOffset(20)]
                    public bool ActivatePCF;

                    [FieldOffset(32) , MarshalAs( UnmanagedType.ByValArray, SizeConst = 10)]
                    public LightDesc[] Lights;
        }
}
