using System;
using SharpDX;

namespace Troll3D.Components.Lighting
{
    public class SpotLight : Light
    {
        public SpotLight(float intensity= 1.0f, float angle = 3.141592f/3.0f): base()
        {
            SetAngle( angle);
            SetIntensity(1.0f);
            SetType (  LightType.SPOTLIGHT);
        }
    }
}
