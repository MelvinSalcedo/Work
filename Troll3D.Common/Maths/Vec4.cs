using System;

namespace Troll3D.Common.Maths
{
    public class Vec4
    {
        /// <summary>
        /// Retourne l'addition de deux vecteurs de dimension 4
        /// </summary>
        public static Vec4 operator +( Vec4 u, Vec4 v )
        {
            return new Vec4( u.X + v.X, u.Y + v.Y, u.Z + v.Z, u.W + v.W );
        }

        /// <summary>
        /// Calcule et retourne la soustraction de deux vecteurs de dimension 4 
        /// </summary>
        public static Vec4 operator -( Vec4 u, Vec4 v)
        {
            return new Vec4( u.X - v.X, u.Y - v.Y, u.Z - v.Z, u.W - v.W );
        }

        /// <summary>
        /// Calcule et retourne l'opposé du vecteur
        /// </summary>
        public static Vec4 operator -( Vec4 u )
        {
            return new Vec4( -u.X, -u.Y, -u.Z, -u.W );
        }

        /// <summary>
        /// Calcule et retourne la multiplication de deux vecteurs de dimension 4 
        /// </summary>
        public static Vec4 operator *( Vec4 u, Vec4 v)
        {
            return new Vec4( u.X * v.X, u.Y * v.Y, u.Z * v.Z, u.W * v.W );
        }

        /// <summary>
        /// Calcule et retourne la multiplication entre le vecteur de dimension 4 et un scalaire
        /// </summary>
        public static Vec4 operator*( Vec4 u, float s )
        {
            return new Vec4( u.X * s, u.Y * s, u.Z * s, u.W * s );
        }

        /// <summary>
        /// Calcule et retourne la division entre le vecteur et un scalaire
        /// </summary>
        public static Vec4 operator /( Vec4 u,  float s )
        {
            return new Vec4( u.X / s, u.Y / s, u.Z / s, u.W / s );
        }

        /// <summary>
        /// Calcule et retourne un point P situé sur le vecteur uv d'origine U.
        /// P = u + uv*t
        /// </summary>
        public static Vec4 Interpolate( Vec4 u, Vec4 v, float t )
        {
            return u + ( v - u ) * t;
        }

        public static Vec4 TurnAround( float teta, float alpha, float radius, Vec4 point )
        {
            Vec4 vec = new Vec4();
            vec.Z = ( float )( ( Math.Sin( alpha ) * Math.Cos( teta ) * radius ) );
            vec.Y = ( float )( Math.Sin( teta ) * radius );
            vec.X = ( float )( Math.Cos( alpha ) * Math.Cos( teta ) * radius );
            return vec + point;
        }

        /// <summary>
        /// Calcule et retourne le produit scalaire de deux vecteurs
        /// </summary>
        public static float Dot( Vec4 u, Vec4 v )
        {
            return ( float )( ( u.X * v.X ) + ( u.Y * v.Y ) + ( u.Z * v.Z ) + ( u.W * v.W ) );
        }

        /// <summary>
        /// Calcule et retourne le produit vectoriel de deux vecteurs
        /// </summary>
        public static Vec4 operator ^( Vec4 u, Vec4 v)
        {
            return new Vec4(
                ( u.Y * v.Z ) - ( v.Y * u.Z ),
                ( u.Z * v.W ) - ( v.Z * u.W ),
                ( u.W * v.X ) - ( v.X * u.X ),
                ( u.X * v.Y ) - ( v.W * u.Y )
                );
        }

        public static Vec4 Forward  { get { return new Vec4(0.0f,0.0f,1.0f,1.0f);}}
        public static Vec4 Zero     { get { return new Vec4(0.0f,0.0f,0.0f,1.0f); } }  
        public static Vec4 Up       { get { return new Vec4(0.0f, 1.0f, 0.0f, 1.0f);}}
        public static Vec4 Right    { get { return new Vec4( 1.0f, 0.0f, 0.0f, 1.0f ); } }

        public Vec4(float x = 0.0f, float y = 0.0f, float z = 0.0f, float w = 1.0f)
        {
            Set( x, y, z, w );
        }

        /// <summary>
        /// Calcule et retourne la longueur du vecteur (longueur/norme/distance)
        /// </summary>
        /// <returns></returns>
        public float Length()
        {
            return ( float )Math.Sqrt( 
                Math.Pow( X, 2 ) +
                Math.Pow( Y, 2 ) +
                Math.Pow( Z, 2 ) +
                Math.Pow( W, 2 ) );
        }

        /// <summary>
        /// Calcule et retourne la longueur du vecteur sans effectué l'opération de 
        /// racine carrée (méthode utile lorsque la rapidité est nécéssaire)
        /// </summary>
        /// <returns></returns>
        public float SQRTLength()
        {
            return ( float )(
                Math.Pow( X, 2 ) +
                Math.Pow( Y, 2 ) +
                Math.Pow( Z, 2 ) +
                Math.Pow( W, 2 ) );
        }

        /// <summary>
        /// Retourne le vecteur normalisé ( sa norme est égale à 1)
        /// </summary>
        public Vec4 Normalize()
        {
            float length = Length();

            if ( length != 0 )
            {
                return new Vec4( X / length, Y / length, Z / length, W/length );
            }

            return new Vec4(0.0f,0.0f,0.0f,0.0f);
        }

        /// <summary>
        /// Retourne l'inverse du vecteur. L'inverse d'un nombre a équivaut à ce que
        /// a*b = 1
        /// </summary>
        /// <returns></returns>
        public Vec4 Inverse()
        {
            throw new NotImplementedException( "A faire" );
        }

       
        /// <summary>
        /// Retourne un vecteur à deux dimension à partir des coordonnées X Y
        /// </summary>
        /// <returns></returns>
        public Vec2 XY()
        {
            return new Vec2( X, Y );
        }

        /// <summary>
        /// Retourne un vecteur à 3 dimension à partir des coordonnées X Y Z
        /// </summary>
        /// <returns></returns>
        public Vec3 XYZ()
        {
            return new Vec3( X, Y, Z );
        }

        /// <summary>
        /// Met à jour l'ensemble des valeurs du vecteur
        /// </summary>
        public void Set( float x, float y, float z, float w )
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float W { get; set; }
    }
}
