using System;
using System.Collections.Generic;
using Troll3D;
using Troll3D.Components;

namespace RenderTextureTest
{
    /// <summary>
    /// Se charge de faire tourner un objet sur lui même
    /// </summary>
    public class RotateCube : Behaviour
    {
        public override void Update()
        {
            Entity.transform_.RotateEuler( coef, 0.0f, 0.0f );
        }

        float coef = 0.0005f;
    }
}
