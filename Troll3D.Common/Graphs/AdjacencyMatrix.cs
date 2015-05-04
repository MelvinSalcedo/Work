using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Common.Graphs
{
    /// <summary>
    ///  Représente une Matrice d'adjavence. Une matrice d'adjacence est un tableau à 2 dimensions
    ///  qui enregistre les chemins possibles entre les noeuds. Ainsi, si data[1,2] est égal à "true",
    ///  cela signifie qu'il existe un chemin (un arc donc), entre les noeuds 1 et 2. La taille d'une
    ///  matrice d'adjacence est donc égal à n², attention donc aux graphes de dimensions importantes.
    /// </summary>
    public class AdjacencyMatrix
    {
        public AdjacencyMatrix( Graph graph )
        {
            Size = graph.Count;
            Data = new short[Count];

            for ( int i = 0; i < Count; i++ )
            {
                Data[i] = 0;
            }

            for ( int i = 0; i < Count; i++ )
            {
                for ( int j = 0; j < graph.GetNode( i ).NeighbourCount; i++ )
                {
                    Data[i * Size + graph.GetNode(i).Id] = 1;
                }
            }
        }

        public short[] Data { get; private set; }

        /// <summary>
        /// Size correspond à la taille du graphe
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Count correspond à la taille de la matrice d'adjacence 
        /// </summary>
        public int Count
        {
            get { return Size * Size; }
        }

       

        public Graph Graph;
    }
}
