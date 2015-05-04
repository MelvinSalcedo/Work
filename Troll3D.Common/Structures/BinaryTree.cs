using System;
using System.Collections.Generic;

namespace Troll3D.Common.Structures
{
    /// <summary>
    ///  Un BinaryTree se présente comme un arbre ou chaque noeud peut potentiellement contenir 2 fils
    ///  Il est possible de "maintenir" un arbre binaire "trier" en utilisante quelques règles simples lors de 
    ///  l'insertion et de la supression de nouveau élément.
    /// </summary>
    public class BinaryTree<T> where T : IComparable
    {
        public BinaryTree( int size )
        {
            m_datas = new List<T>( size + 1 );
        }
        public BinaryTree( List<T> list )
        {
            m_datas = list;
        }

        public void Insert( T val )
        {
            m_datas.Add( val );
            Swim( m_datas.Count - 1 );
        }
        public void Exchange( int n, int o )
        {
            T temp = m_datas[n];
            m_datas[n] = m_datas[o];
            m_datas[o] = temp;
        }
        public T RemoveMax()
        {
            T val = m_datas[1];

            m_datas[1] = m_datas[m_datas.Count - 1];
            m_datas.RemoveAt( m_datas.Count - 1 );

            Sink( 1 );
            return val;
        }

        public int Count()
        {
            return m_datas.Count;
        }

        T GetLeftSon( int n )
        {
            return m_datas[2 * n];
        }
        T GetRightSon( int n )
        {
            return m_datas[( 2 * n ) + 1];
        }
        T GetParent( int n )
        {
            if ( n % 2 == 1 )
            {
                n = n - 1;
            }
            n = n / 2;
            return m_datas[n];
        }

        /// <summary> Si la valeur du noeud est supérieur à celle d'un de ses fils, les éléments sont interverti
        /// </summary>
        /// <param name="n"></param>
        void Sink( int n )
        {
            if ( n < m_datas.Count && ( 2 * n ) < m_datas.Count )
            {

                T maxval;

                bool left = false;

                if ( ( ( 2 * n ) + 1 ) < m_datas.Count )
                {

                    if ( m_datas[2 * n].CompareTo( m_datas[( 2 * n ) + 1] ) > 0 )
                    {
                        maxval = m_datas[2 * n];
                        left = true;
                    }
                    else
                    {
                        maxval = m_datas[( 2 * n ) + 1];
                        left = false;
                    }

                }
                else
                {
                    maxval = m_datas[2 * n];
                    left = true;
                }

                if ( m_datas[n].CompareTo( maxval ) < 0 )
                {
                    if ( left )
                    {
                        Exchange( n, 2 * n );
                        Sink( 2 * n );
                    }
                    else
                    {
                        Exchange( n, ( 2 * n ) + 1 );
                        Sink( ( 2 * n ) + 1 );
                    }
                }
            }
        }
        void Swim( int n )
        {
            if ( n > 0 )
            {
                int parent;
                if ( n % 2 == 1 )
                {
                    parent = ( n - 1 ) / 2;
                }
                else
                {
                    parent = n / 2;
                }

                if ( m_datas[parent].CompareTo( m_datas[n] ) < 0 )
                {
                    Exchange( n, parent );
                    Swim( parent );
                }
            }
        }

        List<T> m_datas;// Commence à 1

    }


}

