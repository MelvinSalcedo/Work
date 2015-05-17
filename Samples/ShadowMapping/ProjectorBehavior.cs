using System;
using System.Collections.Generic;
using SharpDX;
using Troll3D;

using Troll3D.Components;

namespace ShadowMapping
{
    public class ProjectorBehavior : Behaviour
    {
        public ProjectorBehavior()
        {

        }


        public Entity entilol;

        public override void Initialize()
        {
            projector = ( Projector )( entity );

            entilol = entity.Append( new Entity() );

            // Une "vue" pointe en z négatif alors on ajoute le modèle de projection qu'on retourne comme une crêpe
            entilol.modelrenderer_ = new MeshRenderer( new MaterialDX11(), ProjectionMesh.GetModel( projector.m_View.projection_ ) );
            entilol.modelrenderer_.material_.AddConstantBuffer<Vector4>( new Vector4( 1.0f, 1.0f, 1.0f, 1.0f ) );
            entilol.modelrenderer_.SetFillMode( SharpDX.Direct3D11.FillMode.Wireframe );
            //entilol.transform_.SetRotationEuler(0.0f, 3.141592f, 0.0f);
        }

        public override void OnKeyDown( KeyboardEvent e )
        {

            float rotatecoef = 0.03f;
            float translatecoef = 0.2f;

            // Field Of View

            if ( e.keycode_ == KeyCode.Key_1 )
            {
                projector.SetFrustum( projector.GetFrustum().GetFieldOfView() + 0.01f,
                    projector.GetFrustum().GetRatio(),
                    projector.GetFrustum().GetNearPlane(),
                    projector.GetFrustum().GetFarPlane() );

                entilol.modelrenderer_.model_ = ProjectionMesh.GetModel( projector.m_View.projection_ );
            }

            if ( e.keycode_ == KeyCode.Key_2 )
            {
                projector.SetFrustum( projector.GetFrustum().GetFieldOfView() - 0.01f,
                    projector.GetFrustum().GetRatio(),
                    projector.GetFrustum().GetNearPlane(),
                    projector.GetFrustum().GetFarPlane() );

                entilol.modelrenderer_.model_ = ProjectionMesh.GetModel( projector.m_View.projection_ );
            }

            // Rotations
            if ( e.keycode_ == KeyCode.Key_T )
            {
                transform.RotateEuler( 0.0f, rotatecoef, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_G )
            {
                transform.RotateEuler( 0.0f, -rotatecoef, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_Y )
            {
                transform.RotateEuler( rotatecoef, 0.0f, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_H )
            {
                transform.RotateEuler( -rotatecoef, 0.0f, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_U )
            {
                transform.RotateEuler( 0.0f, 0.0f, rotatecoef );
            }

            if ( e.keycode_ == KeyCode.Key_J )
            {
                transform.RotateEuler( 0.0f, 0.0f, rotatecoef );
            }


            // Translations
            if ( e.keycode_ == KeyCode.Key_Z )
            {
                transform.Translate( 0.0f, 0.0f, translatecoef );
            }

            if ( e.keycode_ == KeyCode.Key_S )
            {
                transform.Translate( 0.0f, 0.0f, -translatecoef );
            }

            if ( e.keycode_ == KeyCode.Key_Q )
            {
                transform.Translate( -translatecoef, 0.0f, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_D )
            {
                transform.Translate( translatecoef, 0.0f, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_R )
            {
                transform.Translate( 0.0f, translatecoef, 0.0f );
            }

            if ( e.keycode_ == KeyCode.Key_F )
            {
                transform.Translate( 0.0f, -translatecoef, 0.0f );
            }
        }


        Projector projector;
    }
}
