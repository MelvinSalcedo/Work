using System;
using System.Collections.Generic;

namespace Troll3D.Common.Sorts
{
    //( 5 1 4 2 8 )  ( 1 5 4 2 8 ), L'algorithme compare les deux premier éléments et échange leurs positions puisque 5 > 1.
    //( 1 5 4 2 8 )  ( 1 4 5 2 8 ), Echange de position puisque 5 > 4
    //( 1 4 5 2 8 )  ( 1 4 2 5 8 ), Echange de position puisque 5 > 2
    //( 1 4 2 5 8 )  ( 1 4 2 5 8 ), Comme 8 > 5, l'algorithme n'échange pas la position entre les 2 éléments.
    public class BubbleSort
    {
        // Explication :
        // L'algorithme de tri à bulle parcours tout les éléments du tableau 2 à 2, et compare si l'élément à la position t est supérieur à la position t+1
        // Si tel est le cas, l'algorithme inverse les positions, et met à "vrai" un booléen pour savoir qu'il doit répéter l'opération
        // Le tri ne s'arrete qu'une fois qu'il aura traversé tout le tableau en le modifiant (in place)
        public static void Sort<T>( List<T> array ) where T : IComparable
        {
            try
            {
                bool swapped = true;

                while ( swapped )
                {
                    swapped = false;

                    for ( int i = 0; i < array.Count - 1; i++ )
                    {
                        if ( array[i].CompareTo( array[i + 1] ) > 0 )
                        {
                            T temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;
                            swapped = true;
                        }
                    }
                };
            }
            catch ( Exception e )
            {
                Console.WriteLine( e );
            }
        }

        // Optimisation du Tri à bulle : 
        // Lorsque le tri effectue une passe, il positionne l'élément à sa place définitive
        public static void OptimizedSort<T>( List<T> array )  where T : IComparable
        {
            try
            {
                int n = 0; // Compte le nombre de passe
                bool swapped = true;

                while ( swapped )
                {
                    swapped = false;

                    for ( int i = 0; i < array.Count - 1 - n; i++ )
                    {
                        if (array[i].CompareTo(array[i + 1])>0  )
                        {
                            T temp = array[i];
                            array[i] = array[i + 1];
                            array[i + 1] = temp;
                            swapped = true;
                        }
                    }
                    n++;
                }
            }
            catch ( Exception e )
            {
                Console.WriteLine( e );
            }
        }
    }
}