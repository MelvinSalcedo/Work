using System;

namespace Troll3D.Common.Maths
{
    public class Matrix4x4
    {
        /// <summary>
        /// Calcule et retourne la multiplication de 2 matrices
        /// </summary>
        public static Matrix4x4 operator *( Matrix4x4 m, Matrix4x4 n)
        {
            float [] array = new float[16];

            for(int i=0; i< 16;i++)
            {
                int val     =   i/4;
                int val2    =   i%4;

                array[i]=   (m.get((val*4))    *n.get(val2))     +
                            (m.get((val*4)+1)  *n.get(val2+4))     +
                            (m.get((val*4)+2)  *n.get(val2+8))     +
                            (m.get((val*4)+3)  *n.get(val2+12));
            }

            return new Matrix4x4(array);
        }

        /// <summary>
        /// Calcule et retourne la multiplication d'une matrice et d'un vecteur de dimension 4
        /// </summary>
        public static Vec4 operator * (Matrix4x4 m, Vec4 v){
                Vec4 u = new Vec4();
                u.X=m.get(0)*v.X  + m.get(1)*v.Y  + m.get(2)*v.Z    + m.get(3)*v.W;
                u.Y=m.get(4)*v.X  + m.get(5)*v.Y  + m.get(6)*v.Z    + m.get(7)*v.W;
                u.Z=m.get(8)*v.X  + m.get(9)*v.Y  + m.get(10)*v.Z   + m.get(11)*v.W;
                u.W=m.get(12)*v.X + m.get(13)*v.Y + m.get(14)*v.Z   + m.get(15)*v.W;
                return u;
        }

        /// <summary>
        /// Construit une nouvelle matrice correspondant à la matrice identité
        /// </summary>
        public Matrix4x4()
        {
            Identity();
        }

        /// <summary>
        /// Construit une nouvelle matrice à partir du tableau passé en paramètre
        /// </summary>
        /// <param name="array"></param>
        public Matrix4x4(float [] a)
        {
            for(int i=0; i< 16; i++)
            {
                Datas[i] = a[i];
            }
        }
        
        /// <summary>
        /// Transforme la matrice actuelle en matrice identité
        /// 
        /// 1 0 0 0
        /// 0 1 0 0
        /// 0 0 1 0
        /// 0 0 0 1
        /// </summary>
        public void Identity(){
            Datas[0]=1; 	Datas[1]=0; 	Datas[2]=0; 	Datas[3]=0;
            Datas[4]=0; 	Datas[5]=1; 	Datas[6]=0; 	Datas[7]=0;
            Datas[8]=0; 	Datas[9]=0; 	Datas[10]=1; 	Datas[11]=0;
            Datas[12]=0;	Datas[13]=0; 	Datas[14]=0; 	Datas[15]=1;
        }

        /// <summary>
        /// Transforme la matrice en Matrice de translation 
        /// </summary>
        /// <param name="v"></param>
        public void Translation(Vec3 v)
        {
            Translation(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// Transforme la matrice en matrice de translation
        /// </summary>
        public void Translation(float x, float y, float z)
        {
            Identity();

            set(3, x);
            set(7, y);
            set(11, z);
        }

        /// <summary>
        /// Effectue une translation de valeur "v" sur la matrice actuelle
        /// </summary>
        public void Translate(Vec3 val)
        {
            Translate(val.X, val.Y, val.Z);
        }

        /// <summary>
        /// Effectue une translation de valeur "v" sur la matrice actuelle
        /// </summary>
        public void Translate(float x, float y, float z){
            
            Matrix4x4 mat = new Matrix4x4();

            mat.set(3, x);
            mat.set(7, y);
            mat.set(11, z);

            Datas =  (this * mat).Datas;
        }

        /// <summary>
        /// Transforme la matrice en matrice de rotation
        /// </summary>
        public void Rotation(Vec3 v)
        {
            Rotation(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// Transforme la matrice en matrice de rotation
        /// </summary>
        public void Rotation(float x, float y, float z)
        {
            Identity();
            Rotatex(x);
            Rotatey(y);
            Rotatez(z);
        }

        /// <summary>
        /// Effectue une rotation sur la matrice
        /// </summary>
        public void Rotate(Vec3 v)
        {
            Rotate(v.X,v.Y,v.Z); 
        }

        /// <summary>
        /// Effectue une rotation sur la matrice
        /// </summary>
        public void Rotate(float x, float y, float z){
            
            Matrix4x4 mat = new Matrix4x4();
            mat.Rotatex(x);
            mat.Rotatex(y);
            mat.Rotatez(z);

            Datas = (this*mat).Datas;
        }

        /// <summary>
        /// Transforme la matrice en matrice de "scaling" (homotéthie)
        /// </summary>
        public void Scaling(Vec3 v)
        {          
             Scaling(v.X, v.Y, v.Z);
        }

        /// <summary>
        /// Transforme la matrice en matrice de "scaling" (homotéthie)
        /// </summary>
        public void Scaling(float x, float y, float z)
        {
            Identity();

            set(0, x);
            set(5, y);
            set(10, z);
            set(15, 1);
        }

        /// <summary>
        /// Effectue une homotéthie sur la matrice
        /// </summary>
        public void Scale(Vec3 v)
        {
            Scale(v.X,v.Y,v.Z);
        }

        public void Scale(float x, float y, float z)
        {    
            Matrix4x4 mat = new Matrix4x4();

            mat.set(0, x);
            mat.set(5, y);
            mat.set(10, z);
            mat.set(15, 1);

            Datas = (this * mat).Datas;
        }

        void Rotatex(float angle)
        {
            Matrix4x4 mat = new Matrix4x4();

            float [] vals = new float[16];

            vals[0]=1; 	vals[1]=0;              vals[2]=0;              vals[3]=0;
            vals[4]=0; 	vals[5]= (float)Math.Cos(angle); 	vals[6]=(float)Math.Sin(angle); 	vals[7]=0;
            vals[8]=0; 	vals[9]=-(float)Math.Sin(angle);    vals[10]=(float)Math.Cos(angle);    vals[11]=0;
            vals[12]=0;	vals[13]=0; 			vals[14]=0; 			vals[15]=1;

            mat.set(vals);

            this.Datas = ( this * mat ).Datas;
        }

        void Rotatey(float angle)
        {
            Matrix4x4 mat = new Matrix4x4();

            float[] vals = new float[16];

            vals[0]     = (float)Math.Cos(angle); 	    vals[1]=0;      vals[2]=(float)Math.Sin(angle);     vals[3]=0;
            vals[4]     = 0;                            vals[5]=1;      vals[6]=0;              vals[7]=0;
            vals[8]     = -(float)Math.Sin( angle );    vals[9] = 0; vals[10] = (float)Math.Cos( angle ); vals[11] = 0;
            vals[12]    =0;                             vals[13]=0; 	vals[14]=0;             vals[15]=1;

            mat.set(vals);

            this.Datas = ( this * mat ).Datas;
        }
        void Rotatez(float angle)
        {
            Matrix4x4 mat = new Matrix4x4();

            float [] vals = new float[16];

            vals[0]=(float)Math.Cos(angle); 	vals[1]=(float)Math.Sin(angle); 	vals[2]=0;      vals[3]=0;
            vals[4]=-(float)Math.Sin(angle);    vals[5]=(float)Math.Cos(angle); 	vals[6]=0;      vals[7]=0;
            vals[8]=0;              vals[9]=0;              vals[10]=1;     vals[11]=0;
            vals[12]=0;             vals[13]=0; 			vals[14]=0;     vals[15]=1;

            mat.set(vals);

            this.Datas = ( this * mat ).Datas;
        }

        /// <summary>
        /// Retourne l'inverse de la matrice
        /// </summary>
        Matrix4x4 Inverse()
        {

            float [] inv = new float [16];
            float det;

                inv[0] = Datas[5]  * Datas[10] * Datas[15] -
                         Datas[5]  * Datas[11] * Datas[14] -
                         Datas[9]  * Datas[6]  * Datas[15] +
                         Datas[9]  * Datas[7]  * Datas[14] +
                         Datas[13] * Datas[6]  * Datas[11] -
                         Datas[13] * Datas[7]  * Datas[10];

                inv[4] = -Datas[4]  * Datas[10] * Datas[15] +
                          Datas[4]  * Datas[11] * Datas[14] +
                          Datas[8]  * Datas[6]  * Datas[15] -
                          Datas[8]  * Datas[7]  * Datas[14] -
                          Datas[12] * Datas[6]  * Datas[11] +
                          Datas[12] * Datas[7]  * Datas[10];

                inv[8] = Datas[4]  * Datas[9] * Datas[15] -
                         Datas[4]  * Datas[11] * Datas[13] -
                         Datas[8]  * Datas[5] * Datas[15] +
                         Datas[8]  * Datas[7] * Datas[13] +
                         Datas[12] * Datas[5] * Datas[11] -
                         Datas[12] * Datas[7] * Datas[9];

                inv[12] = -Datas[4]  * Datas[9] * Datas[14] +
                           Datas[4]  * Datas[10] * Datas[13] +
                           Datas[8]  * Datas[5] * Datas[14] -
                           Datas[8]  * Datas[6] * Datas[13] -
                           Datas[12] * Datas[5] * Datas[10] +
                           Datas[12] * Datas[6] * Datas[9];

                inv[1] = -Datas[1]  * Datas[10] * Datas[15] +
                          Datas[1]  * Datas[11] * Datas[14] +
                          Datas[9]  * Datas[2] * Datas[15] -
                          Datas[9]  * Datas[3] * Datas[14] -
                          Datas[13] * Datas[2] * Datas[11] +
                          Datas[13] * Datas[3] * Datas[10];

                inv[5] = Datas[0]  * Datas[10] * Datas[15] -
                         Datas[0]  * Datas[11] * Datas[14] -
                         Datas[8]  * Datas[2] * Datas[15] +
                         Datas[8]  * Datas[3] * Datas[14] +
                         Datas[12] * Datas[2] * Datas[11] -
                         Datas[12] * Datas[3] * Datas[10];

                inv[9] = -Datas[0]  * Datas[9] * Datas[15] +
                          Datas[0]  * Datas[11] * Datas[13] +
                          Datas[8]  * Datas[1] * Datas[15] -
                          Datas[8]  * Datas[3] * Datas[13] -
                          Datas[12] * Datas[1] * Datas[11] +
                          Datas[12] * Datas[3] * Datas[9];

                inv[13] = Datas[0]  * Datas[9] * Datas[14] -
                          Datas[0]  * Datas[10] * Datas[13] -
                          Datas[8]  * Datas[1] * Datas[14] +
                          Datas[8]  * Datas[2] * Datas[13] +
                          Datas[12] * Datas[1] * Datas[10] -
                          Datas[12] * Datas[2] * Datas[9];

                inv[2] = Datas[1]  * Datas[6] * Datas[15] -
                         Datas[1]  * Datas[7] * Datas[14] -
                         Datas[5]  * Datas[2] * Datas[15] +
                         Datas[5]  * Datas[3] * Datas[14] +
                         Datas[13] * Datas[2] * Datas[7] -
                         Datas[13] * Datas[3] * Datas[6];

                inv[6] = -Datas[0]  * Datas[6] * Datas[15] +
                          Datas[0]  * Datas[7] * Datas[14] +
                          Datas[4]  * Datas[2] * Datas[15] -
                          Datas[4]  * Datas[3] * Datas[14] -
                          Datas[12] * Datas[2] * Datas[7] +
                          Datas[12] * Datas[3] * Datas[6];

                inv[10] = Datas[0]  * Datas[5] * Datas[15] -
                          Datas[0]  * Datas[7] * Datas[13] -
                          Datas[4]  * Datas[1] * Datas[15] +
                          Datas[4]  * Datas[3] * Datas[13] +
                          Datas[12] * Datas[1] * Datas[7] -
                          Datas[12] * Datas[3] * Datas[5];

                inv[14] = -Datas[0]  * Datas[5] * Datas[14] +
                           Datas[0]  * Datas[6] * Datas[13] +
                           Datas[4]  * Datas[1] * Datas[14] -
                           Datas[4]  * Datas[2] * Datas[13] -
                           Datas[12] * Datas[1] * Datas[6] +
                           Datas[12] * Datas[2] * Datas[5];

                inv[3] = -Datas[1] * Datas[6] * Datas[11] +
                          Datas[1] * Datas[7] * Datas[10] +
                          Datas[5] * Datas[2] * Datas[11] -
                          Datas[5] * Datas[3] * Datas[10] -
                          Datas[9] * Datas[2] * Datas[7] +
                          Datas[9] * Datas[3] * Datas[6];

                inv[7] = Datas[0] * Datas[6] * Datas[11] -
                         Datas[0] * Datas[7] * Datas[10] -
                         Datas[4] * Datas[2] * Datas[11] +
                         Datas[4] * Datas[3] * Datas[10] +
                         Datas[8] * Datas[2] * Datas[7] -
                         Datas[8] * Datas[3] * Datas[6];

                inv[11] = -Datas[0] * Datas[5] * Datas[11] +
                           Datas[0] * Datas[7] * Datas[9] +
                           Datas[4] * Datas[1] * Datas[11] -
                           Datas[4] * Datas[3] * Datas[9] -
                           Datas[8] * Datas[1] * Datas[7] +
                           Datas[8] * Datas[3] * Datas[5];

                inv[15] = Datas[0] * Datas[5] * Datas[10] -
                          Datas[0] * Datas[6] * Datas[9] -
                          Datas[4] * Datas[1] * Datas[10] +
                          Datas[4] * Datas[2] * Datas[9] +
                          Datas[8] * Datas[1] * Datas[6] -
                          Datas[8] * Datas[2] * Datas[5];

                det = Datas[0] * inv[0] + Datas[1] * inv[4] + Datas[2] * inv[8] + Datas[3] * inv[12];

               /* if (det == 0)
                    return false;

                det = 1.0 / det;
*/
                return new Matrix4x4(inv);
        }

        void Frustsum(float left, float right, float bottom, float top, float near, float far){
            float [] a = new float[16];

            float temp, temp2, temp3, temp4;

            temp    = 2.0f * near;
            temp2   = right - left;
            temp3   = top - bottom;
            temp4   = far - near;

            a[0] =  temp / temp2;  a[1] = 0.0f;            a[2] = (right + left) / temp2;     a[3] = 0.0f;
            a[4] = 0.0f;            a[5] = temp / temp3;   a[6] = (top + bottom) / temp3;     a[7] = 0.0f;
            a[8] = 0.0f;            a[9] = 0.0f;            a[10] = (-far - near) / temp4;     a[11] = (-temp * far) / temp4;
            a[12] = 0.0f;           a[13] = 0.0f;           a[14] = -1.0f;                      a[15] = 0.0f;

            set(a);
        }

        void Perspective(float angle, float ratio, float near, float far)
        {
            float f= ((1.0f) / ((float)Math.Tan(angle / 2.0f)));

            float [] a = new float[16];

            a[0]=f/ratio;   a[1]=0; 	a[2]=0;                            a[3]=0;
            a[4]=0; 		a[5]=f; 	a[6]=0;                            a[7]=0;
            a[8]=0; 		a[9]=0; 	a[10]=((near+far)/(near-far));     a[11]=((2*near*far)/(near-far));
            a[12]=0;		a[13]=0; 	a[14]=-1;                          a[15]=1;

            set(a);
        }
        void Ortho(float left, float right, float top, float bot, float near, float far)
        {
            float [] a = new float[16];

            a[0]    = (2.0f)/(right-left);   a[1]=0;                  a[2]   =0;                     a[3]  =-((right+left)/(right-left));
            a[0]    = 4.0f;                   a[5]=(2.0f)/(top-bot);   a[6]   =0;                     a[7]  =-((top+bot)/(top-bot));
            a[8]    = 0;                     a[9]=0;                  a[10]  =(-2.0f)/(far-near);    a[11] =-((far+near)/(far-near));
            a[12]   = 0;                     a[13]=0;                 a[14]  =0;                     a[15] =1;

            set(a);
        }

        void LookAt(Vec3 from, Vec3 to)
        {
            //Je récupère le vecteur entre la position de l'objet et le point "visé"

            //------------------

               Vec3     target  =   to-from;
               target           =   target.Normalize();

            //------------------

               //  Je calcule le produit vectoriel du vecteur "target" et de "forward" pour trouver l'axe de rotation
               Vec3 side        =   new Vec3(0.0f, -1.0f, 0.0f)^(target);
               side             =   side.Normalize();

            //------------------

                Vec3 up   = side ^(target);
                up        = up.Normalize();

            //------------------

                float [] vals = new float[16];

                vals[0] = side.X;
                vals[1] = side.Y;
                vals[2] = side.Z;
                vals[3] = 0.0f;

            //------------------

                vals[4] = up.X;
                vals[5] = up.Y;
                vals[6] = up.Z;
                vals[7] = 0.0f;

            //------------------

                vals[8] = -target.X;
                vals[9] = -target.Y;
                vals[10] = -target.Z;
                vals[11] = 0.0f;

            //------------------

                vals[12] = vals[13] = vals[14] = 0.0f;  vals[15] = 1.0f;

            //------------------

               set(vals);
               Matrix4x4 mat = new Matrix4x4();
               mat.Translation(-from);

               Datas = (this * mat).Datas;
        }

        public float [] Array()
        {
            return Datas;
        }


        public float get(int i) 
        {
            return Datas[i];
        }

        public void set(int i, float val)
        {
            Datas[i]=val;
        }

        public void set(float[]values)
        {
            for(int i=0; i< 16; i++)
            {
                Datas[i] = values[i];
            }
        }

        private float [] Datas = new float[16];
    }
}
