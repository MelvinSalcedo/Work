using System;
using System.Collections.Generic;
using Troll3D.Common.Structures;

namespace Troll3D.Common.Sorts
{
    /// <summary>
    ///  Heapsort, pour "tri par tas", utilise un Arbre binaire pour trier les éléments
    ///  l'algorithme commence à insérer les éléments dans l'arbre binaire en respectant les rèles d'insertion,
    ///  puis, invoque successivement la methode permettant d'extraire la valeur la plus importante 
    ///  du tree binaire 
    /// </summary>
    public class HeapSort
    {
        public static List<T> Sort<T>( List<T> array ) where T : IComparable
        {
            List<T> returned = new List<T>();

            //array.insert(array.begin(), NULL);

            BinaryTree<T> tree = new BinaryTree<T>(1);

            for(int i=0; i< array.Count; i++)
            {
                tree.Insert(array[i]);
            }

            while(tree.Count()>1)
            {
                returned.Insert(0, tree.RemoveMax());
            }

            return returned;
        }

    }
}

