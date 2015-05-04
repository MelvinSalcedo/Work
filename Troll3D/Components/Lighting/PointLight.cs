using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D.Components.Lighting
{
    public class PointLight : Light
    {

        public PointLight( float range = 30.0f, float intensity = 1.0f, float specularIntensity = 10.0f )
        {
            SetRange( range );
            SetIntensity( intensity );
            SetSpecularIntensity( specularIntensity );
            SetType( LightType.POINT_LIGHT );
        }

    }
}
