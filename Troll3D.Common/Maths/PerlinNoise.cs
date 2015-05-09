using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Common.Maths
{
    public class PerlinNoise
    {
        /// <summary>
        /// Retourne un tableau correspondant à une Image en 2 dimension
        /// Pour créer un bruit qui soit de "bonne qualité", il est important de superposer
        /// le résultat de plusieurs bruit de perlin de dimensions différentes
        /// </summary>
        public static float[] GetDatas( int octaves, int perlinwidth, int seed, int texwidth )
        {
            List<PerlinNoise> noises = new List<PerlinNoise>();

            int lastval = perlinwidth;
            noises.Add( new PerlinNoise( perlinwidth, perlinwidth, seed ) );

            for ( int i = 1; i < octaves; i++ )
            {
                noises.Add( new PerlinNoise( lastval * 2, lastval * 2, seed ) );
                lastval = lastval * 2;
            }

            float[] image = new float[texwidth * texwidth * 4];

            for ( int i = 0; i < texwidth; i++ )
            {
                for ( int j = 0; j < texwidth; j++ )
                {
                    float noiseval = 0.0f;
                    lastval = 1;

                    noiseval += 1.0f / ( float )lastval * noises[0].GetNoise( ( float )j / ( float )texwidth, ( float )i / ( float )texwidth );
                    lastval = lastval * 2;

                    for ( int h = 1; h < noises.Count; h++ )
                    {
                        noiseval += 1.0f / ( ( float )lastval ) * noises[h].GetNoise( ( float )j / ( float )texwidth, ( float )i / ( float )texwidth );
                        lastval = lastval * 2;
                    }

                    // pour faire une "rampe" autours de -1 1 il me semble
                    noiseval = (noiseval + 1.0f)/2.0f;
                    noiseval = noiseval / octaves;
                    image[( ( i * texwidth + j ) * 4 ) + 0] = noiseval;
                    image[( ( i * texwidth + j ) * 4 ) + 1] = noiseval;
                    image[( ( i * texwidth + j ) * 4 ) + 2] = noiseval;
                    image[( ( i * texwidth + j ) * 4 ) + 3] = 1.0f;
                }
            }
            return image;
        }

        /// <summary>
        /// Initialise les paramètres du bruit de perlin
        /// </summary>
        public PerlinNoise( int width, int height, int seed )
        {
            rand_       = new Random( seed );
            Seed        = seed;

            Width = width;
            Height= height;

            //InitializeGradients();
            BuildPermutationArray();
            BuildGradients();
        }

        public int Seed { get ; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        private void InitializeGradients()
        {
            for ( int i = 0; i < Height; i++ )
            {
                for ( int j = 0; j < Width; j++ )
                {
                    gradients_.Add( RandomGradient() );
                }
            }
        }

        /// <summary>
        /// Récupère la valeur du bruit aux coordonnées demandés (dans l'intervale [0,1])
        /// </summary>
        private float GetNoise( float x, float y )
        {
            // Conversion [0;1] vers [0; width] et [0; height]

            Vec2 realpos = new Vec2( x * ( ( float )Width - 1.0f ), y * ( ( float )Height - 1.0f ) );

            // On cherche le "noeud" de la grille sur lequel on va travailler

            int basex = ( int )realpos.X;
            int basey = ( int )realpos.Y;

            // On transforme ces valeurs pour qu'elles fonctionne sur l'intervalle de la table de 
            // permutation

            int jj = basex & 255;
            int ii = basey & 255;

            // On récupère les indices des gradients que l'on va utiliser 
            int g1 = m_permutationArray[(ii + m_permutationArray[jj] )% 255] % 8;
            int g2 = m_permutationArray[(ii + m_permutationArray[(jj + 1)%255] )% 255] % 8;
            int g3 = m_permutationArray[(ii + 1 + m_permutationArray[jj] )% 255] % 8;
            int g4 = m_permutationArray[( ii + 1 + m_permutationArray[( jj + 1 ) % 255] ) % 255] % 8;

            float tempx = realpos.X - basex;
            float tempy = realpos.Y - basey;
            float s = gradients_[g1].X*tempx + gradients_[g1].Y + tempy;

            tempx = realpos.X - ( basex + 1 );
            tempy = realpos.Y - basey;
            float t = gradients_[g2].X * tempx + gradients_[g2].Y + tempy; ;

            tempx = realpos.X - basex;
            tempy = realpos.Y - ( basey + 1 );
            float u = gradients_[g3].X * tempx + gradients_[g3].Y + tempy;

            tempx = realpos.X - ( basex + 1 );
            tempy = realpos.Y - ( basey + 1 );
            float v = gradients_[g4].X * tempx + gradients_[g4].Y + tempy; 

            // On lisse le résultat à l'aide de la fonction 3p^2 - 2p^3 dans l'axe des x
            // (la fonction ressemble à un s

            double floatpart = realpos.X - ( float )basex;
            float smoothx = 3.0f * ( float )Math.Pow( floatpart, 2.0 ) - 2.0f * ( float )Math.Pow( floatpart, 3.0f );

            float a = s + smoothx * ( t - s );
            float b = u + smoothx * ( v - u );

            float floatparty = realpos.Y - ( float )basey;
            float smoothy = 3.0f * ( float )Math.Pow( floatparty, 2.0f ) - 2.0f * ( float )Math.Pow( floatparty, 3.0f );

            return (a + smoothy * ( b - a ));
        }

        private Vec2 RandomGradient()
        {
            float x = rand_.Next() % 100 - 50;
            float y = rand_.Next() % 100 - 50;
            Vec2 rv = new Vec2( x, y );
            rv.Normalize();
            return rv;
        }

        private Vec2 GetGradient( int x, int y )
        {
            return gradients_[y * Width + x];
        }

        /// <summary>
        ///  Construit le tableau de permutation ... bon, pour le moment je me contente d'en récupérer un
        /// </summary>
        private void BuildPermutationArray()
        {
            m_permutationArray = new int[256]{
                151,160,137,91,90,15,131,13,201,95,96,53,194,233,7,225,140,36,103,30,69,
                142,8,99,37,240,21,10,23,190,6,148,247,120,234,75,0,26,197,62,94,252,219,
                203,117,35,11,32,57,177,33,88,237,149,56,87,174,20,125,136,171,168,68,175,
                74,165,71,134,139,48,27,166,77,146,158,231,83,111,229,122,60,211,133,230,220,
                105,92,41,55,46,245,40,244,102,143,54,65,25,63,161,1,216,80,73,209,76,132,
                187,208,89,18,169,200,196,135,130,116,188,159,86,164,100,109,198,173,186,3,
                64,52,217,226,250,124,123,5,202,38,147,118,126,255,82,85,212,207,206,59,227,
                47,16,58,17,182,189,28,42,223,183,170,213,119,248,152,2,44,154,163,70,221,
                153,101,155,167,43,172,9,129,22,39,253,19,98,108,110,79,113,224,232,178,185,
                112,104,218,246,97,228,251,34,242,193,238,210,144,12,191,179,162,241,81,51,145,
                235,249,14,239,107,49,192,214,31,181,199,106,157,184,84,204,176,115,121,50,45,
                127,4,150,254,138,236,205,93,222,114,67,29,24,72,243,141,128,195,78,66,215,61,
                156,180};
        }

        /// <summary>
        /// On construit un tableau de 8 gradients
        /// </summary>
        private void BuildGradients()
        {
            float half = 1.0f / (float)Math.Sqrt( 2 );

            gradients_.Add( new Vec2( 1.0f, 0.0f ) );
            gradients_.Add( new Vec2( 0.0f, 1.0f ) );
            gradients_.Add( new Vec2( -1.0f, 0.0f ) );
            gradients_.Add( new Vec2( 0.0f, -1.0f ) );

            gradients_.Add( new Vec2( half, 0.0f ) );
            gradients_.Add( new Vec2( 0.0f, half ) );
            gradients_.Add( new Vec2( -half, 0.0f ) );
            gradients_.Add( new Vec2( 0.0f, -half ) );
        }

        /// <summary>
        /// Tableau de permutation utilisé pour trouver la valeur du bruit
        /// </summary>
        private int[] m_permutationArray;
        Random rand_;
        private List<Vec2> gradients_ = new List<Vec2>();

    }
}
