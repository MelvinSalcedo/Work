using System;
using System.Collections.Generic;

namespace Troll3D.Common.Sorts
{
    //// Tri le tableau. Pour se faire, on commence le tri à la 2 eme position, et on remonte
    //// la valeur jusqu'à trouver une valeur plus petite. On recommence l'opération à partir de
    //// la 3 eme position etc :

    //// 9 !6 2 4 3 ->  !6 9 2 4 3
    //// 6 9 !2 4 5 ->  6 !2 9 4 5 -> !2 4 9 4 5
    public class InsertionSort
    {
        public static void InPlaceSort<T>( List<T> array ) where T: IComparable
        {
            int j;
            if ( array.Count > 1 )
            {
                for ( int i = 1; i < array.Count; i++ )
                {
                    T val = array[i];
                    
                    for ( j = i;  j > 0 && array[j - 1].CompareTo(val)>0 ; j-- )
                    {
                        // en gros on déplace le tableau vers la droite 
                        array[j]=array[j - 1];
                    }
                    // Une fois qu'on a trouvé la case d'arrivé de l'élement à trier, on le permute
                    array[j] = val;
                }
            }
        }
    }

}

