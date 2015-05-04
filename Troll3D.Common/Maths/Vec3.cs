using System;

namespace Troll3D.Common.Maths{

    public class Vec3
    {
        /// <summary>
        /// Calcule et retourne le produit scalaire de deux vecteurs
        /// </summary>
        public static float Dot(Vec3 u, Vec3 v)
        {
            return ((u.X*v.X)+(u.Y*v.Y));
        }

        /// <summary>
        /// Calcule et retourne le produit vectoriel de deux vecteurs
        /// </summary>
        public static Vec3 operator ^( Vec3 u, Vec3 v)
        {
            return new Vec3((u.Y*v.Z)-(v.Y*u.Z),(u.Z*v.X)-(v.Z*u.X), (u.X*v.Y)-(v.X*u.Y));
        }

        /// <summary>
        /// Calcule et retourne l'addition de deux vecteurs
        /// </summary>
        public static Vec3 operator +( Vec3 u ,Vec3 v )
        {
            return new Vec3( u.X + v.X, u.Y + v.Y, u.Z + v.Z );
        }

        /// <summary>
        /// Calcule et retourne la soustraction de deux vecteurs
        /// </summary>
        public static Vec3 operator -( Vec3 u, Vec3 v )
        {
            return new Vec3( u.X - v.X, u.Y - v.Y, u.Z - v.Z );
        }

        /// <summary>
        /// Calcule et retourne l'opposé du vecteur
        /// </summary>
        public static Vec3 operator -( Vec3 u )
        {
            return new Vec3( -u.X, -u.Y, -u.Z );
        }

        /// <summary>
        /// Calcule et retourne la multiplication de deux vecteurs
        /// </summary>
        public static Vec3 operator *( Vec3 u, Vec3 v )
        {
            return new Vec3( u.X * v.X, u.Y * v.Y, u.Z * v.Z );
        }

        /// <summary>
        /// Calcule et retourne la multiplication entre 1 vecteur et un scalaire
        /// </summary>
        public static Vec3 operator *( Vec3 u, float s )
        {
            return new Vec3( u.X * s, u.Y * s, u.Z * s );
        }

        /// <summary>
        /// Calcule et retourne la division entre un vecteur et un scalaire
        /// </summary>
        public static Vec3 operator /( Vec3 u, float s )
        {
            return new Vec3( u.X / s, u.Y / s, u.Z / s );
        }

        /// <summary>
        /// Calcule et retourne un point P situé sur le vecteur uv d'origine U.
        /// P = u + uv*t
        /// </summary>
        public static Vec3 Interpolate( Vec3 u, Vec3 v, float t )
        {
            return u + ( v - u ) * t;
        }

        /// <summary>
        /// Construit un nouveau vecteur de dimension 3
        /// </summary>
        public Vec3( float x = 0.0f, float y = 0.0f, float z = 0.0f )
        {
            Set( x, y, z );
        }

        /// <summary>
        /// Construit un nouveau vecteur de dimension 3 à partir des infos du vecteur
        /// de dimension 2 passé en paramètre. x et y prendront les valeurs du vec2 passé
        /// en paramètre
        /// </summary>
        public Vec3( Vec2 v, float z = 0.0f )
        {
            Set( v.X, v.Y, z );
        }

        /// <summary>
        /// Calcule et retourne la "longueur" (magnitude, norme) du vecteur
        /// </summary>
        public float Length()
        {
            return ( float )( Math.Sqrt(Math.Pow( X, 2 ) + Math.Pow( Y, 2 ) + Math.Pow( Z, 2 ) ));
        }

        /// <summary>
        /// Calcule et retourne la longueur du vecteur sans effectué l'opération de
        /// racine carré pour économiser le processeur 
        /// </summary>
        /// <returns></returns>
        public float SQRTLength()
        {
            return ( float )( Math.Pow( X, 2 ) + Math.Pow( Y, 2 ) + Math.Pow( Z, 2 )  );
        }

        /// <summary>
        /// Calcule et retourne le vecteur normalisé ( dont la longueur ou magnitude 
        /// est égale à 1)
        /// </summary>
        public Vec3 Normalize()
        {
            float length = Length();
            if ( length != 0 )
            {
                return new Vec3( X / length, Y / length, Z / length );
            }
            return new Vec3();
        }

        /// <summary>
        /// Retourne un vecteur en 2 dimensions dans l'ordre indiqué dans la signature de 
        /// la méthode
        /// </summary>
        public Vec2 XY()
        {
            return new Vec2(X,Y);
        }

        /// <summary>
        /// Retourne un vecteur en 2 dimensions dans l'ordre indiqué dans la signature de 
        /// la méthode
        /// </summary>
        public Vec2 XZ()
        {
            return new Vec2(X,Z);
        }

        /// <summary>
        /// Retourne un vecteur en 2 dimensions dans l'ordre indiqué dans la signature de 
        /// la méthode
        /// </summary>
        public Vec2 YZ()
        {
            return new Vec2(Y,Z);
        }

        /// <summary>
        /// Retourne un vecteur en 2 dimensions dans l'ordre indiqué dans la signature de 
        /// la méthode
        /// </summary>
        public Vec2 YX()
        {
            return new Vec2( Y, X );
        }

        /// <summary>
        /// Retourne un vecteur en 2 dimensions dans l'ordre indiqué dans la signature de 
        /// la méthode
        /// </summary>
        public Vec2 ZY()
        {
            return new Vec2( Z, Y );
        }

        /// <summary>
        /// Retourne un vecteur en 2 dimensions dans l'ordre indiqué dans la signature de 
        /// la méthode
        /// </summary>
        public Vec2 ZX()
        {
            return new Vec2( Z, X );
        }

        /// <summary>
        /// Modifie les valeurs x,y,z du vecteur
        /// </summary>
        public void Set( float x, float y, float z )
        {
            X = x;
            Y = y;
            Z = z;
        }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }
}
