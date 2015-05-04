using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpDX;

namespace Troll3D{

    [StructLayout(LayoutKind.Explicit, Size = 272)]
    public struct LightDesc{

        // Datas

            /// <summary>
            ///  Projection, principalement utilisé pour le shadowMapping
            /// </summary>
            [FieldOffset(0)]
            public Matrix Projection;

            /// <summary>
            /// Transformation permet de retrouver la position et l'orientation de la lumière
            /// </summary>
            [FieldOffset(64)]
            public Matrix Transformation;

            [FieldOffset(128)]
            public Matrix Inverse;

            [FieldOffset(192)]
            public Vector4 AmbiantColor;

            [FieldOffset(208)]
            public Vector4 LightColor;

            [FieldOffset( 224 )]
            public float SpecularIntensity;

            [FieldOffset(228)]
            public float Intensity;

            [FieldOffset(232)]
            public float Range;

            [FieldOffset(236)]
            public int Type;

            [FieldOffset(240)]
            public float Angle;

            /// <summary>
            /// Si false, la lumière ne sera pas utilisé pour créer des ombres
            /// </summary>
            [FieldOffset(244)]
            public bool IsCastingShadows;

            [FieldOffset(248)]
            public float ShadowmapWidth;

            [FieldOffset(252)]
            public float ShadowmapHeight;
    }
}
