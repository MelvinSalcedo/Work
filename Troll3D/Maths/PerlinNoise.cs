using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;


namespace Troll3D
{
    public class PerlinNoise{

        // Public

            // Static Methods

                public static float[] GetDatas(int octaves, int perlinwidth, int seed, int texwidth){

                    List<PerlinNoise> noises = new List<PerlinNoise>();

                    int lastval = perlinwidth;
                    noises.Add(new PerlinNoise(perlinwidth, perlinwidth, seed));

                    for (int i = 1; i < octaves; i++){
                        noises.Add(new PerlinNoise(lastval * 2, lastval * 2, seed));
                        lastval = lastval * 2;
                    }

                    float[] image = new float[texwidth * texwidth * 4];

                    for (int i = 0; i < texwidth; i++){
                        for (int j = 0; j < texwidth; j++){

                            float noiseval  = 0.0f;
                            lastval         = 1;

                            noiseval += 1.0f / (float)lastval * noises[0].GetNoise((float)j / (float)texwidth, (float)i / (float)texwidth);
                            lastval = lastval * 2;
                            for (int h = 1; h < noises.Count; h++){
                                noiseval += 1.0f / ((float)lastval) * noises[h].GetNoise((float)j / (float)texwidth, (float)i / (float)texwidth);
                                lastval = lastval * 2;
                            }

                            // pour faire une "rampe" autours de -1 1 il me semble
                            noiseval = (noiseval + (3.75f * 0.5f)) / 3.75f;

                            image[((i * texwidth + j) * 4) + 0] = noiseval;
                            image[((i * texwidth + j) * 4) + 1] = noiseval;
                            image[((i * texwidth + j) * 4) + 2] = noiseval;
                            image[((i * texwidth + j) * 4) + 3] = 1.0f;
                        }
                    }

                   return image;
                }

            // Lifecycle

                public PerlinNoise(int width, int height, int seed){
                    rand_ = new Random(seed);
                    seed_   = seed;

                    gradients_ = new List<Vector2>();

                    width_  = width;
                    height_ = height;

                    InitializeGradients();

                }

            // Methods

                float GetNoise(float x, float y){

                    // Conversion [0;1] vers [0; width] et [0; height]

                    Vector2 realpos = new Vector2(x * ((float)Width - 1.0f), y * ((float)Height - 1.0f));

                    int basex = (int)realpos.X;
                    int basey = (int)realpos.Y;

                    // On récupère les "poids" sur chaque noeud de la grille
                    float s = Vector2.Dot(GetGradient(basex, basey), realpos - new Vector2(basex, basey));
                    float t = Vector2.Dot(GetGradient(basex + 1, basey), realpos - new Vector2(basex + 1, basey));
                    float u = Vector2.Dot(GetGradient(basex, basey + 1), realpos - new Vector2(basex, basey + 1));
                    float v = Vector2.Dot(GetGradient(basex + 1, basey + 1), realpos - new Vector2(basex + 1, basey + 1));

                    // On lisse le résultat à l'aide de la fonction 3p^2 - 2p^3 dans l'axe des x
                    // (la fonction ressemble à un s
                    
                    double floatpart    = realpos.X - (float)basex;
                    float smoothx       = 3.0f * (float)Math.Pow(floatpart, 2.0) - 2.0f * (float)Math.Pow(floatpart, 3.0f);

                    float a = s + smoothx * (t - s);
                    float b = u + smoothx * (v - u);

                    float floatparty = realpos.Y - (float)basey;
                    float smoothy = 3.0f * (float)Math.Pow(floatparty, 2.0f) - 2.0f * (float)Math.Pow(floatparty, 3.0f);

                    float finalval = a + smoothy * (b - a);
                    return finalval;

                }

                public int Seed{
                    get { return seed_; }
                }

                public int Width {
                    get { return width_; }
                }

                public int Height{
                    get { return height_; }
                }

        // Private

            // Methods

                private void InitializeGradients(){
                    for (int i = 0; i < Height; i++){
                        for (int j = 0; j < width_; j++){
                            gradients_.Add(RandomGradient());
                        }
                    }
                }

                private Vector2 RandomGradient(){
                    float x = rand_.Next() % 100 - 50;
                    float y = rand_.Next() % 100 - 50;
                    Vector2 rv = new Vector2(x, y);
                    rv.Normalize();
                    return rv;
                }

                private Vector2 GetGradient(int x, int y){
                    return gradients_[y * Width + x];
                }

            // Datas

                Random rand_;
                private int height_;
                private int width_;
                private int seed_;

                private List<Vector2> gradients_;

    }
}
