using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SharpDX;
using Troll3D;
using Troll3D.Components;

namespace Collisions
{
    /// <summary>
    /// Petit bout de code que je pourrai éventuellement réutiliser lorsque je souhaiterai 
    /// avoir des objets déplacable à la souris
    /// </summary>
    public class PickableCube : Behaviour
    {
        public override void OnMouseDown( MouseEvent e )
        {
            base.OnMouseDown( e );

            if(e.mouse_.rightbutton)
            {
                RaycastResult result = TRaycast.FireRayFromMouse();

                if ( result.GetEntity() == Entity )
                {
                    IsBeingDragged = true;
                    IntersectionPoint = result.GetIntersectionPoint();
                    DraggedOffset = Entity.transform_.WorldPosition() - IntersectionPoint;
                }
            }
        }

        public override void OnMouseMove( MouseEvent e )
        {
            if ( IsBeingDragged )
            {
                // Pour savoir comment déplacer l'objet, on a besoin de récupérer le plan de la caméra
                // Un plan est définit par un point et deux vecteurs directeurs

                // On commence par récupérer les 2 vecteurs directeurs, en convertissant les vecteurs up et right
                // à la caméra
                
                Vector3 right = (Vector3)(Vector4.Transform(new Vector4(1.0f,0.0f,0.0f,0.0f), Camera.Main.m_transform.GetViewMatrix()));
                Vector3 up = ( Vector3 )( Vector4.Transform( new Vector4( 0.0f, 1.0f, 0.0f, 0.0f ), Camera.Main.m_transform.GetViewMatrix() ) );

                // On construit le plan en utilisant le point d'intersection 
                Troll3D.Plane plane = new Troll3D.Plane( IntersectionPoint, right, up );

                // On récupère le rayon correspondant à la position actuelle de la souris dans l'espace
                TrollRay ray = TRaycast.GetRayFromMouse();

                Vector3 point = plane.IntersectionWithLine( new LineGeometry( ray.start_, ray.direction_ ) );

                Entity.transform_.SetPosition( point + DraggedOffset );
            }
        }

        public override void OnMouseUp( MouseEvent e )
        {
            IsBeingDragged = false; 
        }

        /// <summary>
        /// Un indicateur permettant de repérer si l'objet est en train d'être déplacé
        /// </summary>
        public bool     IsBeingDragged;

        /// <summary>
        /// Vecteur qui enregistre la différence entre le "centre" de l'objet sélectionné
        /// et le point initial de sélection
        /// </summary>
        public Vector3  DraggedOffset;

        /// <summary>
        /// Point de l'intersection entre le rayon et le collider
        /// </summary>
        public Vector3 IntersectionPoint;


    }
}
