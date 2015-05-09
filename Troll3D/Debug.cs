using System;
using SharpDX;
using Troll3D.Rendering;
using Troll3D.Rendering.DebugRendering;

namespace Troll3D
{
    /// <summary>
    /// Classe regroupant les méthodes statiques de debug (affichage de ligne, cercles, points etc)
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// Affichage un cercle de rayon r à la position p et de couleur c
        /// </summary>
        /// <param name="p"> Centre du cercle</param>
        /// <param name="r"> Rayon du cercle</param>
        /// <param name="c"> Couleur du cercle</param>
        public static void PrintCircle( Vector3 p, float r, Vector3 c)
        {
            DebugRenderer.Instance.AddPrimitive( new DebugCircle( p, r, c ) );
        }

        /// <summary>
        /// Affiche un cercle de rayon r à la position p et de couleur blanche par défault
        /// </summary>
        /// <param name="c">Centre du cercle</param>
        /// <param name="r">Rayon du cercle</param>
        public static void PrintCircle( Vector3 p, float r )
        {
            PrintCircle( p, r, new Vector3( 1.0f, 1.0f, 1.0f ) );
        }
    }
}
