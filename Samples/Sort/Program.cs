using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Troll3D.Common.Sorts;
using System.Diagnostics;

namespace AlgorithmsTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            Stopwatch sw = new Stopwatch();
            bool ShowConsole = false;
            int size = 40000;

            List<int> bubbleList    = new List<int>();
            List<int> optBubbleList = new List<int>();
            List<int> stdSortList   = new List<int>();
            List<int> insertionSort = new List<int>();
            List<int> heapsortList  = new List<int>();
            List<int> introsortList = new List<int>();
            List<int> quicksortList = new List<int>();

            Random rand = new Random();

            Console.WriteLine( "Initial Array " );

            for ( int i = 0; i < size; i++ )
            {
                bubbleList.Add( rand.Next() % 100 );
                optBubbleList.Add( bubbleList[i] );
                stdSortList.Add( bubbleList[i] );
                insertionSort.Add( bubbleList[i] );
                heapsortList.Add( bubbleList[i] );
                introsortList.Add( bubbleList[i] );
                quicksortList.Add( bubbleList[i] );
                if ( ShowConsole )
                {
                    Console.Write( bubbleList[i] + "; " );
                }
            }

            Console.WriteLine();

            long time;

            // Standard Sort

            sw.Start();
            stdSortList.Sort();
            sw.Stop();
            time = sw.ElapsedMilliseconds;
            sw.Reset();

            if ( ShowConsole )
            {
                ShowList( stdSortList );
            }

            Console.WriteLine( "Standard Sort en " + time + " ms" );

            // Test Bubble Sort
           // sw.Start();
           //// BubbleSort.Sort<int>( bubbleList );
           // sw.Stop();
           // time = sw.ElapsedMilliseconds;
           // sw.Reset();

           // if ( ShowConsole )
           // {
           //     ShowList( bubbleList );
           // }
            
           // Console.WriteLine( "Bubble Sort en "+time+" ms" );

            
            // Test Optimized Bubble Sort

            //sw.Start();
            ////BubbleSort.OptimizedSort<int>( optBubbleList );
            //sw.Stop();
            //time = sw.ElapsedMilliseconds;
            //sw.Reset();

            //if ( ShowConsole )
            //{
            //    ShowList( optBubbleList );
            //}
            //Console.WriteLine( "Optimized Bubble Sort en " + time + " ms" );

            // Insertion Sort

            //sw.Start();
            //InsertionSort.InPlaceSort<int>( insertionSort );
            //sw.Stop();
            //time = sw.ElapsedMilliseconds;
            //sw.Reset();

            //if ( ShowConsole )
            //{
            //    ShowList( insertionSort );
            //}
            //Console.WriteLine( "Insertion Sort en " + time + " ms" );

            //// HeapSort

            //sw.Start();
            //heapsortList = HeapSort.Sort<int>( heapsortList );
            //sw.Stop();
            //time = sw.ElapsedMilliseconds;
            //sw.Reset();

            //if ( ShowConsole )
            //{
            //    ShowList( heapsortList );
            //}
            //Console.WriteLine( "HeapSort en " + time + " ms" );

            // QuickSort

            sw.Start();
            QuickSort.OptimizedSort<int>( quicksortList );
            sw.Stop();
            time = sw.ElapsedMilliseconds;
            sw.Reset();

            if ( ShowConsole )
            {
                ShowList( quicksortList );
            }
            Console.WriteLine( "Quicksort en " + time + " ms" );

            // IntroSort

            sw.Start();
            IntroSort.InPlaceSort<int>( introsortList );
            sw.Stop();
            time = sw.ElapsedMilliseconds;
            sw.Reset();

            if ( ShowConsole )
            {
                ShowList( introsortList );
            }
            Console.WriteLine( "IntroSort en " + time + " ms" );


            Console.Read();


        }

        public static void ShowList(List<int> list)
        {
            for ( int i = 0; i < list.Count; i++ )
            {
                Console.Write( list[i] + "; " );
            }
        }
    }
}
