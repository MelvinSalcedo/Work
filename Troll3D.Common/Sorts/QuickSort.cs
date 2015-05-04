using System;
using System.Collections.Generic;
using System.Collections;

namespace Troll3D.Common.Sorts
{
    public class QuickSort
    {
        public static Random Rand = new Random();

        public static List<T> Sort<T>( List<T> array ) where T : IComparable
        {
            Random rand = new Random();

            List<T> less = new List<T>();
            List<T> more = new List<T>();

            if ( array.Count > 1 )
            {
                // Sélection du pivot

                int pivot = rand.Next() % array.Count;

                // On place les élements du tableau dans 2 nouvelles liste, less et more, en fonction de la valeur du pivot

                for ( int i = 0; i < array.Count; i++ )
                {
                    if ( i != pivot )
                    {
                        if ( array[i].CompareTo( array[pivot] ) > 0 )
                        {
                            less.Add( array[i] );
                        }
                        else
                        {
                            more.Add( array[i] );
                        }
                    }
                }

                // On tri les tableaux ainsi obtenu jusqu'à ce que le tableau ne fasse plus qu'un seul élément

                less = Sort( less );
                more = Sort( more );

                less.Add( array[pivot] );
                less.AddRange( more );

                return less;
            }
            return array;
        }

        public static void OptimizedSort<T>( List<T> array ) where T : IComparable
        {
            OptimizedSort( array, 0, array.Count-1 );
        }

        /// <summary>En place</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public static void OptimizedSort<T>( List<T> array, int left, int right ) where T : IComparable
        {
            // Si la liste a plus de 2 éléments

            if ( left < right )
            {
                // On récupère la position du pivot après partionnement

                int newpivotindex = InPlacePartionning( left, right, array );

                OptimizedSort( array, left, newpivotindex - 1 );
                OptimizedSort( array, newpivotindex + 1, right );
            }
        }

        public static void OptimizedSortThreeMedian<T>( List<T> array, int left = -1, int right = -1 ) where T : IComparable
        {
            if ( left == -1 )
            {
                left = 0;
                right = array.Count - 1;
            }
            // Si la liste a plus de 2 éléments
            if ( left < right )
            {
                // On récupère la position du pivot après partionnement

                int newpivotindex = InPlacePartionningThreeMedian( left, right, array );

                OptimizedSort( array, left, newpivotindex - 1 );
                OptimizedSort( array, newpivotindex + 1, right );
            }
        }

        public static int InPlacePartionning<T>( int left, int right, List<T> array ) where T : IComparable
        {
            // Choix du pivot

            int pivot = left + Rand.Next() % ( right - left + 1 );

            T pivotVal = array[pivot];
            T val;
            // On déplace le pivot à la fin du tableau

            array[pivot] = array[right];

            // On parcours cette portion du tableau tout en vérifiant que l'élément analysé est inférieur
            // au pivot. Si l'élément est inférieur au pivot, on le la place au début

            // On réutilise la variable pivot pour "storeIndex"
            pivot = left;

            for ( int i = left; i < right; i++ )
            {
                if ( array[i].CompareTo( pivotVal ) < 0 )
                {
                    val             = array[i];
                    array[i]        = array[pivot];
                    array[pivot]    = val;
                    pivot++;
                }
            }

            array[right] = array[pivot];
            array[pivot] = pivotVal;

            return pivot;
        }

        public static int InPlacePartionningThreeMedian<T>( int left, int right, List<T> array ) where T : IComparable
        {
            // Choix du pivot via la technique des 3 médians

            List<T> pivotlist = new List<T>();

            pivotlist.Add(array[left]);
            pivotlist.Add(array[ (right-left) / 2]);
            pivotlist.Add(array[right]);
            
            pivotlist =  HeapSort.Sort(pivotlist);

            int pivot;

            if(array[left].CompareTo(pivotlist[1])==0)
            {
                pivot = left;
            }
            else if(array[ (right-left) / 2].CompareTo(pivotlist[1]) == 0)
            {
                pivot =(right-left) / 2;
            }
            else
            {
                pivot = right;
            }

            T pivotVal = array[pivot];

            // On déplace le pivot à la fin du tableau

            array[pivot] = array[right];

            // On parcours cette portion du tableau tout en vérifiant que l'élément analysé est inférieur
            // au pivot. Si l'élément est inférieur au pivot, on le la place au début

            int storeindex = left;

            for(int i=left; i< right-1; i++){
                if( array[i].CompareTo( pivotVal)>0){
                    if(i!=storeindex){
                        T val = array[i];
                        array[i] = array[storeindex];
                        array[storeindex] = val;
                        storeindex++;
                    }else{
                        storeindex++;
                    }
                }
            }

            array[storeindex] = array[right];
            array[right] = pivotVal;

            return storeindex;
        }
    }
}
