using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D
{

    // Cette classe va principalement servir à rassembler les différentes fonctions mathématiques complexes réutilisable
    public class MathStuffs
    {

        // Public

        // Static Methods

        // Retourne les coefficients de la droite d'équation ax+by+c = 0
        public static Vector3 GetLineCoef( Vector3 u, Vector3 v )
        {
            float a1, b1, c1;

            a1 = ( v.Y - u.Y );
            b1 = -( v.X - u.X );
            c1 = -( u.X * v.Y ) + ( v.X * u.Y );

            return new Vector3( a1, b1, c1 );
        }

        /// <summary>
        ///  Retourne le vecteur normal v au segment [AB] = b-a. 
        ///  v = ( -(b-a).y ; (b-a).x)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static Vector2 Normal( Vector2 a, Vector2 b )
        {

            Vector2 direction = b - a;
            Vector2 normal = new Vector2( -direction.Y, direction.X );
            normal.Normalize();
            return normal;
        }

        public static Vector3 MidPoint( Vector3 start, Vector3 end, float displacementvalue, int occurence )
        {

            Vector3 dir = end - start;
            // On trouve le point "milieu"

            Vector3 mid = start + dir / 2.0f;

            // On déplace le point milieu en prenant une valeur aléatoire 

            Random rand = new Random();
            float val = ( displacementvalue / occurence ) * rand.NextFloat( -1.0f, 1.0f );

            Vector3 normal = new Vector3( -dir.Y, dir.X, 0.0f );
            normal.Normalize();
            Vector3 displacedMid = mid + normal * val;
            return displacedMid;
        }


        public static Vector3 ProjectPoint( Vector3 point, Vector3 axis )
        {
            float val = Vector3.Dot( point, axis );
            return new Vector3( val, val, 0.0f );
        }

        // Détecte le point de collision entre 2 vecteux AB et CD. 
        public static bool SegmentIntersect( Vector3 a, Vector3 b, Vector3 c, Vector3 d, ref Vector3 returnvec )
        {

            // Dans un premier temps, on vérifie que les vecteurs u et v de ab et cd ne sont pas colinéaire

            Vector3 u = b - a;
            Vector3 v = d - c;

            float determinant = u.X * v.Y - v.X * u.Y;
            if ( determinant >= -0.01 && determinant <= -0.01 )
            {
                return false;
            }

            // Si le déterminant n'est pas égal à 0, alors on cherche l'équation des 2 droites AB et CD

            Vector3 line1 = GetLineCoef( a, b );
            Vector3 line2 = GetLineCoef( c, d );

            float xx = line1.Y * line2.Z - line2.Y * line1.Z;
            float yy = line2.X * line1.Z - line1.X * line2.Z;
            float ww = line1.X * line2.Y - line2.X * line1.Y;

            returnvec = new Vector3( xx / ww, yy / ww, 0.0f );

            float coefa = ( ( returnvec - a ).Length() ) / u.Length();
            float coefb = ( ( returnvec - c ).Length() ) / v.Length();

            if ( coefa >= 0 && coefa <= 1 )
            {
                if ( coefb >= 0 && coefb <= 1 )
                {
                    return true;
                }
            }

            return false;
        }


        // Se note n!
        public static long Factoriel( long n )
        {
            if ( n > 1 )
            {
                return n * Factoriel( n - 1 );
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        ///  Utilisé pour les courbes de Bézier et les B-Splines
        /// </summary>
        /// <param name="n"></param>
        /// <param name="i"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static double PolynomeBernstein( int n, int i, float t )
        {
            double val = Factoriel( n );
            double underval = ( Factoriel( i ) * Factoriel( n - i ) );
            val = val / underval;
            val = val * Math.Pow( t, i );
            val = val * Math.Pow( ( 1.0f - t ), n - i );
            return val;
        }

        public static double PolynomeRiesenfeld( int i, int n, double t )
        {
            double sum = 0.0f;
            for ( int k = 0; k <= ( n - i ); k++ )
            {
                double under = ( double )Factoriel( k ) * Factoriel( n - k + 1 );
                double up = Math.Pow( -1.0f, k ) * Math.Pow( t + n - i - k, n );
                sum += up / under;
            }
            return ( ( double )( n + 1 ) * sum );
        }
    }



}
