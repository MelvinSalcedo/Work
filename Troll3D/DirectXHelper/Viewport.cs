using System;
using System.Collections.Generic;

namespace Troll3D{
    /// <summary>
    ///  Un ViewPort permet de définir un rectangle dans lequel DirectX se charge de dessiner les
    ///  différentes primitives. Il peut y avoir plusieurs viewport. Un ViewPort est lié à une caméra.
    ///  Plusieurs caméra peuvent donc être actives en même temps
    /// </summary>
    public class Viewport{
        
        // Public

            // Lifecycle

                public Viewport(int xoffset, int yoffset, int width, int height){
                    Width   = width;
                    Height  = height;
                    XOffset = xoffset;
                    YOffset = yoffset;
                }

            // Virtual Methods

            // Methods

            // Datas

                public int Width;
                public int Height;

                public int XOffset;
                public int YOffset;
    }
}
