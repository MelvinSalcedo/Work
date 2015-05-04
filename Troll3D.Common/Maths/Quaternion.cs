using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Common.Maths {

    /// <summary>
    /// Un quaternion est un outil mathématique, inspiré des nombres complexes, qui permet
    /// de créer une matrice de rotation correcte ( cf Gimbal Lock)
    /// Un quaternion est divisé en 2 parties : Un scalaire w, et un vecteur x y z
    /// 
    /// En 3D, le vecteur représente l'axe de rotation, et le scalaire la valeur de l'angle
    /// </summary>
    public class Quaternion 
    {

        /// <summary>
        /// Retourne la multiplication de 2 quaternions
        /// </summary>
        public static Quaternion operator *( Quaternion u, Quaternion v )
        {
            Quaternion qres = new Quaternion();

            qres.A = u.D * v.A + u.A * v.D + u.B * v.C - u.C * v.B;
            qres.B = u.D * v.B + u.B * v.D + u.C * v.A - u.A * v.C;
            qres.C = u.D * v.C + u.C * v.D + u.A * v.B - u.B * v.A;
            qres.D = u.D * v.D - u.A * v.A - u.B * v.B - u.C * v.C;

            return qres.Normalized() ;
        }

        /// <summary>
        /// Calcule le quaternion correspondant à la rotation demandé
        /// </summary>
        static Quaternion Euler( float x, float y, float z )
        {
            Quaternion quat = new Quaternion();

            // Finds the Sin and Cosin for each half angles.
            float sY = ( float )Math.Sin( y * 0.5 );
            float cY = ( float )Math.Cos( y * 0.5 );
            float sZ = ( float )Math.Sin( x * 0.5 );
            float cZ = ( float )Math.Cos( x * 0.5 );
            float sX = ( float )Math.Sin( z * 0.5 );
            float cX = ( float )Math.Cos( z * 0.5 );

            // Formula to construct a new Quaternion based on Euler Angles.
            quat.A = cY * cZ * cX - sY * sZ * sX;
            quat.B = sY * sZ * cX + cY * cZ * sX;
            quat.C = sY * cZ * cX + cY * sZ * sX;
            quat.D = cY * sZ * cX - sY * cZ * sX;

            return quat;
        }

        static Quaternion Euler( Vec3 vec )
        {
            return Euler( vec.X, vec.Y, vec.Z );
        }

        /// <summary>
        /// Construit un nouveau quaternion
        /// </summary>
        public Quaternion()
        {
            A = B = C = 0;
            D = 1;
        }

        public Quaternion( float a, float b, float c, float angle)
        {
            Vec3 vec = new Vec3( a, b, c );

            float norm = vec.Length();

            if ( norm < 0.00001 )
            {
                // Null Quaternion
                A = 0;
                B = 0;
                C = 0;
                D = 1;
            }
            else
            {
                float sin_a = ( float )Math.Sin( angle / 2.0 );
                float cos_a = ( float )Math.Cos( angle / 2.0 );

                A = a * sin_a * norm;
                B = b * sin_a * norm;
                C = c * sin_a * norm;
                D = cos_a;
            }
        }


        /*! @brief Return a rotation matrix from the current Quaternion */
        public Matrix4x4 RotationMatrix()
        {
            float [] ar = new float[16];

            float xx = A * A;
            float xy = A * B;
            float xz = A * C;
            float xw = A * D;

            float yy = B * B;
            float yz = B * C;
            float yw = B * D;

            float zz = C * C;
            float zw = C * D;

            ar[0] = 1 - 2 * ( yy + zz );
            ar[1] = 2 * ( xy - zw );
            ar[2] = 2 * ( xz + yw );

            ar[4] = 2 * ( xy + zw );
            ar[5] = 1 - 2 * ( xx + zz );
            ar[6] = 2 * ( yz - xw );

            ar[8] = 2 * ( xz - yw );
            ar[9] = 2 * ( yz + xw );
            ar[10] = 1 - 2 * ( xx + yy );

            ar[3] = ar[7] = ar[11] = ar[12] = ar[13] = ar[14] = 0;
            ar[15] = 1;

            return new Matrix4x4( ar );
        }

        /// <summary>
        /// Retourne la norme du quaternion
        /// </summary>
        public float Length()
        {
            Quaternion conj = Conjuge();
            return (float)Math.Sqrt( A * A + B * B + C * C + D * D );
        }

        /// <summary>
        /// Retourne le quaternion inverse
        /// </summary>
        public Quaternion Invert()
        {
            return new Quaternion( A * -1, B * -1, C * -1, D );
        }

        /// <summary>
        /// Retourne le quaternion normalisé
        /// </summary>
        /// <returns></returns>
        Quaternion Normalized()
        {
            float length= Length();

            if ( length > 0.0 )
            {
                return new Quaternion(A / length, B / length, C / length , D / length);
            }
            return new Quaternion();
        }


        /// <summary>
        /// Retourne le conjugé du quaternion
        /// </summary>
        public Quaternion Conjuge()
        {
            return new Quaternion(-A,-B,-C,D);
        }

        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public float D { get; set; }
    }
}

