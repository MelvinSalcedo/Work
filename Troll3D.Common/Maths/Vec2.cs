using System;

namespace Troll3D.Common.Maths
{
    public class Vec2
    {
        /// <summary>
        /// Calcule et retourne l'addition de deux vecteurs
        /// </summary>
        public static Vec2 operator +(Vec2 v1, Vec2 v2 )
        {
            return new Vec2(v1.X + v2.X ,v1.Y + v2.Y);
        }

        /// <summary>
        /// Calcule et retourne la soustraction de deux vecteurs
        /// </summary>
        public static Vec2 operator -(Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X - v2.X, v1.Y - v2.Y);
        }

        /// <summary>
        /// Calcule et retourne l'opposé du vecteur
        /// </summary>
        public static Vec2 operator -( Vec2 u )
        {
            return new Vec2( -u.X, -u.Y );
        }

        /// <summary>
        /// Calcule et retourne la multiplication de deux vecteurs
        /// </summary>
        public static Vec2 operator *( Vec2 v1, Vec2 v2)
        {
            return new Vec2(v1.X * v2.X, v1.Y * v2.Y );
        }

        /// <summary>
        /// Calcule et retourne la multiplication entre 1 vecteur et un scalaire
        /// </summary>
        public static Vec2 operator * (Vec2 v1, float scalaire )
        {
            return new Vec2(v1.X * scalaire, v1.Y*scalaire);
        }

        /// <summary>
        /// Calcule et retourne la multiplication entre 1 vecteur et 1 
        /// </summary>
        public static Vec2 operator /( Vec2 v, float scalaire )
        {
            return new Vec2(v.X/ scalaire, v.Y / scalaire);
        }

        /// <summary>
        /// Calcule et retourne le résultat du produit scalaire entre 2 vecteurs
        /// </summary>
        public static float Dot(Vec2 u,  Vec2 v)
        {
            return ( float )( ( u.X * v.X ) + ( u.Y * v.Y ) );
        }

        /// <summary>
        /// Calcule et retourne le résultat du produit vectoriel entre 2 vecteurs
        /// </summary>
        public static Vec2 operator ^(Vec2 u, Vec2 v)
        {
            return new Vec2(( ( u.Y * v.X ) - ( v.Y * u.X ) ) , ( u.X * v.Y ) - ( v.X * u.Y ));
        }

        /// <summary>
        /// Construit un nouveau Vecteur de dimensions 2
        /// </summary>
        public Vec2(float x = 0.0f, float y = 0.0f)
        {
            Set(x,y);
        }

        /// <summary>
        /// Retourne un point P situé sur le vecteur uv d'origine U.
        /// P = u + uv*t
        /// </summary>
        public static Vec2 Interpolate( Vec2 u, Vec2 v, float t )
        {
            return u + ( v - u ) * t;
        }

        /// <summary>
        /// Calcule et retourne la "longueur" (magnitude, norme) du vecteur
        /// </summary>
        public float Length()
        {
            return ( float )Math.Sqrt( Math.Pow( X, 2.0 ) + Math.Pow( Y, 2.0 ) );
        }

        /// <summary> 
        /// Retourne la distance du vecteur sans effectuer de racine carré sur le résultat
        /// </summary>
        public float SQRTLength()
        {
            return (float)( Math.Pow( Y, 2.0 ) + Math.Pow( X, 2.0 ) );
        }

        /// <summary>
        /// Retourne le vecteur normalisé
        /// </summary>
        public Vec2 Normalize()
        {
            float length = Length();

            if ( length != 0 )
            {
                return new Vec2( X / length, Y / length );
            }
            return new Vec2();
        }

        public float X{get;set;}
        public float Y{get;set;}

        /// <summary>
        /// Met à jour l'ensemble des valeurs du vecteur
        /// </summary>
        public void Set(float x, float y)
        {
            X = x;
            Y = y;
        }
    }
}
