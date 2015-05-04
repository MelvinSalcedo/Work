using System;
using System.Collections.Generic;


namespace Troll3D.Common.Sorts
{
    public class IntroSort
    {
        public static void InPlaceSort<T>( List<T> array ) where T: IComparable
        {
            InPlaceSort( array, 0, array.Count-1, 0 );
            InsertionSort.InPlaceSort(array);
        }

        public static void InPlaceSort<T>( List<T> array, int left, int right, int recursion ) where T : IComparable
        {
            
            if(recursion>=  2*System.Math.Log(array.Count,2.0f))
            {

            }
            else
            {
               
                // Si la liste a plus de 2 éléments

                if(left<right){

                    // On récupère la position du pivot après partionnement
                        int newpivotindex = QuickSort.InPlacePartionning(left, right, array);

                        InPlaceSort(array, left, newpivotindex-1, recursion+1);
                        InPlaceSort(array, newpivotindex+1, right, recursion+1);
                    }
            }
        }
    }
}
