using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.src.Component
{
    class Maths{


        // retourne la "valeur" de hauteur de la heightmap passé en paramètre pour une valeur comprise entre 0 et 1
        public static float GetFilteredValue(float[] image, int width, int height, float posx, float posy)
        {

            // il faut récupérer les 4 sommets entourant la position paramétré passé en paramètre

            posx += 0.000001f;
            posy += 0.0000001f;
            int x1 = (int)Math.Ceiling(posx * (float)width);
            if (x1 > width - 1)
            {
                x1 = width - 1;
            }

            int x2 = (x1 - 1) % width;
            if (x2 < 0)
            {
                x2 = 0;
            }
            int y1 = (int)Math.Ceiling(posy * (float)height);
            if (y1 > height - 1)
            {
                y1 = height - 1;
            }
            int y2 = (y1 - 1) % height;
            if (y2 < 0)
            {
                y2 = 0;
            }

            float val = image[y1 * (width * 4) + x1 * 4];
            val += image[y1 * (width * 4) + x2 * 4];
            val += image[y2 * (width * 4) + x1 * 4];
            val += image[y2 * (width * 4) + x2 * 4];

            return (float)val / 4.0f;
        }

    }
}
