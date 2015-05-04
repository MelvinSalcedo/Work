using System;
using System.Collections.Generic;

namespace Troll3D.Common.Maths
{
    public static class MathCommon
    {
        /// <summary>
        /// Calcule et retourne le résultat d'une factoriel (noté n!)
        /// </summary>
        public static long Factoriel( long n )
        {
            if ( n > 1 )
            {
                return n * Factoriel( n - 1 );
            }
            return 1;
        }

        /// <summary>
        ///  Calcule et retourne le résultat d'un polynome de Bernstein
        /// </summary>
        public static double PolynomeBernstein( int n, int i, float t )
        {
            double val = Factoriel( n );
            double underval = ( Factoriel( i ) * Factoriel( n - i ) );
            val = val / underval;
            val = val * Math.Pow( t, i );
            val = val * Math.Pow( ( 1.0f - t ), n - i );
            return val;
        }

        /// <summary>
        /// Calcule et retourne le résultat d'un Polynome de Riesenfeld
        /// </summary>
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
