using System;
using System.Collections.Generic;
using Troll3D;

using Troll3D.Components;

namespace ShadowMapping
{
    public class RotateCube : Behaviour
    {

        public RotateCube() { }

        public override void Initialize()
        {
            coef = 0.0057f;
        }

        public override void Update()
        {
            Entity.transform_.RotateEuler( coef, 0.0f, 0.0f );
        }

        float coef;
    }
}
