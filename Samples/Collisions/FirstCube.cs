using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components;

namespace Collisions
{
    public class FirstCube : Behaviour
    {
        public override void Initialize()
        {
            m_renderer = ( MeshRenderer )Entity.GetComponent( ComponentType.MeshRenderer );
        }
        public override void OnCollisionEnter( Troll3D.CollisionEvent e )
        {
            m_renderer.material_.SetMainColor( 1.0f, 0.0f, 0.0f, 1.0f );
        }

        public override void OnCollisionExit( Troll3D.CollisionEvent e )
        {
            m_renderer.material_.SetMainColor( 0.0f, 0.0f, 1.0f, 1.0f );
        }

        public override void OnKeyDown( Troll3D.KeyboardEvent e )
        {
            float speed = 0.055f;
            if ( e.keycode_ == Troll3D.KeyCode.Key_Z )
            {
                Entity.transform_.Translate( 0.0f, 0.0f, speed );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_S )
            {
                Entity.transform_.Translate( 0.0f, 0.0f, -speed );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_Q )
            {
                Entity.transform_.Translate( -speed , 0.0f, 0.0f );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_D )
            {
                Entity.transform_.Translate( speed, 0.0f, 0.0f );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_R )
            {
                Entity.transform_.Translate( 0.0f, speed, 0.0f );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_F )
            {
                Entity.transform_.Translate( 0.0f, -speed, 0.0f );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_T )
            {
                Entity.transform_.RotateEuler( 0.1f, 0.0f, 0.0f );
            }

            if ( e.keycode_ == Troll3D.KeyCode.Key_G )
            {
                Entity.transform_.RotateEuler( 0.0f, 0.1f, 0.0f );
            }
        }

        private MeshRenderer m_renderer;
    }
}
