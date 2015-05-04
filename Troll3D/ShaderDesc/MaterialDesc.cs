using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using SharpDX;

namespace Troll3D
{
    [StructLayout( LayoutKind.Explicit, Size = 16 )]
    public struct MaterialDesc
    {
        public MaterialDesc( float ambiantCoef, float diffuseCoef, float specularCoef )
        {
            AmbiantCoef = ambiantCoef;
            DiffuseCoef = diffuseCoef;
            SpecularCoef = specularCoef;
        }

        [FieldOffset( 0 )]
        public float AmbiantCoef;

        [FieldOffset( 4 )]
        public float DiffuseCoef;

        [FieldOffset( 8 )]
        public float SpecularCoef;


    }
}
