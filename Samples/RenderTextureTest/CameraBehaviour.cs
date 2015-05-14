using System;
using System.Collections.Generic;
using Troll3D;
using SharpDX;
using Troll3D.Components;

namespace RenderTextureTest
{

    public class CameraBehaviour : Behaviour
    {
        public override void Initialize()
        {
            count = 0;
            coef = 0.001f;
        }

        public override void Update()
        {
            //count += ( float )TimeHelper.Instance.GetElapsedTime() * ( float )coef;
            count += 0.001f;
            //Entity.transform_.SetPosition
            //( 
            //    Vector3.Lerp
            //    ( 
            //        new Vector3( 0.0f, 0.0f, -10.0f ),
            //        new Vector3( 0.0f, 10.0f, -10.0f ),
            //        count
            //    ) 
            //);

            //if ( count > 1 || count < 0 )
            //{
            //    coef = -coef;
            //}

            Entity.transform_.SetPosition( new Vector3( 0.0f, 3.0f, -10.0f ) );

        }
        public float coef;
        public float count;
    }
}
