using System;
using System.Collections.Generic;
using SharpDX;

using Troll3D.Components;

namespace Troll3D
{

    /// <summary>
    /// Compare un objet de type ray (qui représente un segment, une droite ou un demi droite) avec
    /// les différents colliders présent dans la scène
    /// </summary>
    public class TRaycast
    {
        /// <summary> 
        /// Récupère la position de la souris dans l'espace réel
        /// </summary>
        public static Vector3 GetWorldMousePosition()
        {
            // En premier lieu, on récupère la position de la souris sur l'écran

            Vector2 MousePosition = new Vector2( InputManager.Instance.mouseInformation.x, InputManager.Instance.mouseInformation.y );

            // Maintenant on va transformer ça en View space si possible

            // On transforme la position de la souris dans l'interval[-1;1]

            MousePosition.X = ( float )MousePosition.X / Screen.Instance.Width* 2 - 1;

            MousePosition.Y = Screen.Instance.Height- MousePosition.Y;
            MousePosition.Y = ( float )MousePosition.Y / Screen.Instance.Height* 2 - 1;

            // Maintenant, on utilise la matrice de la caméra pour récupérer la position "réelle" du point

            Matrix lol = Camera.Main.GetProjection().Data * Camera.Main.m_transform.worldmatrix_;

            Vector4 transformed = Vector4.Transform( new Vector4( MousePosition, -1.0f, 1.0f ), lol );
            transformed.X = transformed.X / transformed.W;
            transformed.Y = transformed.Y / transformed.W;
            transformed.Z = transformed.Z / transformed.W;
            return ( Vector3 )( transformed );
        }

        public static TrollRay GetRayFromMouse()
        {
            // En premier lieu, on récupère la position de la souris sur l'écran
            Vector2 MousePosition = InputManager.Instance.mouseInformation.xy;

            // Maintenant on va transformer ça en View space si possible

            // On transforme la position de la souris dans l'interval[-1;1]

            MousePosition.X = ( float )MousePosition.X / Screen.Instance.Width* 2 - 1;

            MousePosition.Y = Screen.Instance.Height- MousePosition.Y;
            MousePosition.Y = ( float )MousePosition.Y / Screen.Instance.Height* 2 - 1;

            // Maintenant, on utilise la matrice de la caméra pour récupérer la position "réelle" du point

            Matrix lol = Camera.Main.m_transform.worldmatrix_ * Camera.Main.GetProjection().Data;
            //lol.Invert();

            Vector4 transformed = Vector4.Transform( new Vector4( MousePosition, -1.0f, 1.0f ), lol );
            Vector4 transformed2 = Vector4.Transform( new Vector4( MousePosition, 1.0f, 1.0f ), lol );

            transformed.X = transformed.X / transformed.W;
            transformed.Y = transformed.Y / transformed.W;
            transformed.Z = transformed.Z / transformed.W;

            transformed2.X = transformed2.X / transformed2.W;
            transformed2.Y = transformed2.Y / transformed2.W;
            transformed2.Z = transformed2.Z / transformed2.W;

            TrollRay tray = new TrollRay( ( Vector3 )transformed, ( Vector3 )transformed2 );
            return tray;
        }

        public static RaycastResult FireRayFromMouse()
        {
            return GetRayFromMouse().Fire();
        }

        /// <summary>
        /// Le triangle est déterminé par les points A, B, C, le point P appartient au plan du triangle
        /// </summary>
        public static bool PointInTriangle( Vector3 A, Vector3 B, Vector3 C, Vector3 P )
        {
            // Le nom de l'algorithm : Barycentric Coordinates
            // L'objectif est en gros de caractérisé un point dans le triangle à l'aide de 2 vecteurs
            // u et v. P = u*(C-A) + v*(B-A)
            // Le but est de déterminé la valeur de u et de v en fonction du point P. si u et v > 0 et
            //u+v < 1, alors on se trouve dans le triangle
            Vector3 u = B - A;
            Vector3 v = C - A;
            Vector3 w = P - A;

            // On fait le produit vectoriel 
            Vector3 vCrossW = Vector3.Cross( v, w );
            Vector3 vCrossU = Vector3.Cross( v, u );

            // On teste le signe de r
            if ( Vector3.Dot( vCrossW, vCrossU ) < 0 )
            {
                return false;
            }

            Vector3 uCrossW = Vector3.Cross( u, w );
            Vector3 uCrossV = Vector3.Cross( u, v );

            if ( Vector3.Dot( uCrossW, uCrossV ) < 0 )
            {
                return false;
            }

            float denom = uCrossV.Length();
            float r = vCrossW.Length() / denom;
            float t = uCrossW.Length() / denom;

            return ( r + t <= 1 );
        }


        /// <summary>
        ///  Inspecte tout les triangles d'un mesh pour trouver le point d'intersection le plus proche (s'il existe)
        ///  Renvoie true si le rayon rentre en collision avec le mesh. On récupère le point d'intersection
        ///  à partir de dernier argument passé en référence
        /// </summary>
        public static bool IntersectWithMesh( TrollRay ray, Collider collider, ref Vector3 intersectionPoint, ref Vector3 intersectionNormal )
        {
            bool doesIntersect = false;
            bool isClosestPointSet = false;

            Vector3 ClosestIntersectionPoint = new Vector3( 0.0f, 0.0f, 0.0f );

            Mesh mesh = collider.Mesh;

            for ( int i = 0; i < mesh.Faces.Count; i++ )
            {
                Vector3 posA = ( Vector3 )( Vector4.Transform( new Vector4( ( ( StandardVertex )( mesh.Vertices[mesh.Faces[i].Indexes[0]] ) ).Position, 1.0f ), collider.transform_.worldmatrix_ ) );
                Vector3 posB = ( Vector3 )( Vector4.Transform( new Vector4( ( ( StandardVertex )( mesh.Vertices[mesh.Faces[i].Indexes[1]] ) ).Position, 1.0f ), collider.transform_.worldmatrix_ ) );
                Vector3 posC = ( Vector3 )( Vector4.Transform( new Vector4( ( ( StandardVertex )( mesh.Vertices[mesh.Faces[i].Indexes[2]] ) ).Position, 1.0f ), collider.transform_.worldmatrix_ ) );

                // On recherche l'équation du plan du triangle, on a besoin de la normale pour se faire
                // Rappel, équation du plan : ax+by+cz+d=0

                Vector3 normal = Vector3.Normalize( Vector3.Cross( posB - posA, posC - posA ) );

                float A = normal.X;
                float B = normal.Y;
                float C = normal.Z;

                // à partir de la normale, on connait les composantes a,b et c de l'équation ax+by+cz+d=0 du plan
                // On doit désormais trouver la composant D; Pour se faire, on sélectionne un point de notre plan pour 
                // remplir x,y et c et trouver d

                float D = -( posA.X * A + posA.Y * B + posA.Z * C );

                // On calcule maintenant les composants de l'équation paramétrique de la droite 
                // à partir de son vecteur directeur


                float denom = ray.direction_.X * A + ray.direction_.Y * B + ray.direction_.Z * C;
                // non  Colinarité
                if ( denom != 0 )
                {
                    float t = -ray.start_.X * A - ray.start_.Y * B - ray.start_.Z * C - D;
                    t = t / denom;

                    Vector3 P = new Vector3(
                        ray.start_.X + t * ray.direction_.X,
                        ray.start_.Y + t * ray.direction_.Y,
                        ray.start_.Z + t * ray.direction_.Z
                    );

                    bool returnval = PointInTriangle( posA, posB, posC, P );
                    if ( returnval == true )
                    {
                        if ( !isClosestPointSet )
                        {
                            isClosestPointSet = true;
                            ClosestIntersectionPoint = P;
                            intersectionNormal = normal;
                        }
                        else
                        {
                            if ( ( P - ray.start_ ).Length() < ( ClosestIntersectionPoint - ray.start_ ).Length() )
                            {
                                ClosestIntersectionPoint = P;
                                intersectionNormal = normal;
                            }
                        }
                        doesIntersect = true;
                    }
                }
            }
            
            intersectionPoint = ClosestIntersectionPoint;
            return doesIntersect;
        }

    }
}
