using System;
using System.Collections.Generic;
using SharpDX;

namespace Troll3D
{
    /// <summary>
    /// La classe Screen servira à enregistrer et à accéder aux informations concernant les dimensions de la fenêtre
    /// Il sera également possible de s'abonner à un événement en cas de redimensionnement de la fenêtre
    /// </summary>
    public class Screen
    {
        public static Screen Instance;

        public Screen( int width, int height )
        {
            Width   = width;
            Height  = height;
            Instance = this;
        }

        public void Resize( int width, int height )
        {
            Width   = width;
            Height  = height;
        }

        public float GetRatio()
        {
            return ( float )Width/ ( float )Height;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Size2 Size
        {
            get{
                return new Size2()
                {
                    Height = Height,
                    Width = Width
                };

            }
        }
    }
}
