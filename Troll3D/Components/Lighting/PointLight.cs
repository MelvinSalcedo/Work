using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D.Components.Lighting
{
    public class PointLight : Light
    {
        public PointLight()
        {
            SetRange( 30.0f );
            SetIntensity( 1.0f );
            SetSpecularIntensity( 10.0f );
            SetType( LightType.POINT_LIGHT );
        }

        public PointLight( float range = 30.0f, float intensity = 1.0f, float specularIntensity = 10.0f )
        {
            SetRange( range );
            SetIntensity( intensity );
            SetSpecularIntensity( specularIntensity );
            SetType( LightType.POINT_LIGHT );
        }

    }
}
