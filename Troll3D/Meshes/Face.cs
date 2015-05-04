using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D{

    /// <summary>
    /// Pour le moment, je pars du principe qu'une face est forcément constitué de 3 sommets (triangle)
    /// </summary>
    public class Face{

        public Face(int index1, int index2, int index3){
            Indexes = new int[3];

            Indexes[0] = index1;
            Indexes[1] = index2;
            Indexes[2] = index3;
        }

        public int[] Indexes;
    }

}
