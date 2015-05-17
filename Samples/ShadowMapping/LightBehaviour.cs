using System;
using System.Collections.Generic;
using Troll3D;
using SharpDX;

using Troll3D.Components;

namespace ShadowMapping
{
    public class LightBehaviour : Behaviour
    {
        public LightBehaviour() { }

        public override void Initialize()
        {
            count = 0;
            coef = 0.001f;
        }
        public override void Update()
        {

            //count += (float)TimeHelper.Instance.GetElapsedTime() * (float)coef;
            //transform.SetPosition( Vector3.Lerp( new Vector3(0.0f, 0.0f, -10.0f), new Vector3(0.0f,10.0f, -10.0f),
            //    count
            //    ));

            //if(count>1 || count<0){
            //    coef = - coef;
            //}


        }

        public override void OnKeyDown( KeyboardEvent e )
        {
            if ( e.keycode_ == KeyCode.Key_5 )
            {
                LightManager.Instance.SwitchPCF();
                Console.WriteLine( " PCF is " + LightManager.Instance.IsPCFActivated() );
            }
            if ( e.keycode_ == KeyCode.Key_3 )
            {
                LightManager.Instance.SetAcneBias( LightManager.Instance.GetAcneBias() + TimeHelper.Instance.GetElapsedTime() * 0.0001f );
                Console.WriteLine( LightManager.Instance.GetAcneBias() );
            }

            if ( e.keycode_ == KeyCode.Key_4 )
            {
                LightManager.Instance.SetAcneBias( LightManager.Instance.GetAcneBias() + TimeHelper.Instance.GetElapsedTime() * -0.0001f );
                Console.WriteLine( LightManager.Instance.GetAcneBias() );
            }


            if ( e.keycode_ == KeyCode.Key_1 )
            {
                Entity.transform_.RotateEuler( 0.0f, 0.005f * ( float )TimeHelper.Instance.GetElapsedTime(), 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_2 )
            {
                Entity.transform_.RotateEuler( 0.0f, -0.005f * ( float )TimeHelper.Instance.GetElapsedTime(), 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_Z )
            {
                Entity.transform_.Translate( 0.0f, 0.5f * ( float )TimeHelper.Instance.GetElapsedTime(), 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_S )
            {
                Entity.transform_.Translate( 0.0f, -0.5f * ( float )TimeHelper.Instance.GetElapsedTime(), 0.0f );
            }


            if ( e.keycode_ == KeyCode.Key_Q )
            {
                Entity.transform_.Translate( 0.0f, 0.0f, 0.5f * ( float )TimeHelper.Instance.GetElapsedTime() );
            }

            if ( e.keycode_ == KeyCode.Key_D )
            {
                Entity.transform_.Translate( 0.0f, 0.0f, -0.5f * ( float )TimeHelper.Instance.GetElapsedTime() );
            }

            if ( e.keycode_ == KeyCode.Key_R )
            {
                Entity.transform_.Translate( 0.5f * ( float )TimeHelper.Instance.GetElapsedTime(), 0.0f, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_F )
            {
                Entity.transform_.Translate( -0.5f * ( float )TimeHelper.Instance.GetElapsedTime(), 0.0f, 0.0f );
            }

        }

        public float coef;
        public float count;
    }
}
