using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Troll3D.Common.Graphs
{
    /// <summary>
    ///  Représente un graphe et les diverses opérations que l'on peut effectuer
    /// </summary>
    public class Graph
    {
        /// <summary>
        /// Initialise "count" noeuds 
        /// </summary>
        /// <param name="count"></param>
        public Graph( int count = 0 )
        {

        }

        public void AddNode( Node node )
        {
            m_nodes.Add( node );
        }

        public void AddNewNode()
        {
            m_nodes.Add( new Node( m_nodes.Count ) );
        }

        public Node GetNode( int index )
        {
            try
            {
                return m_nodes[index];
            }
            catch
            {
                throw new Exception( "Erreur, le noeud demandé ne figure pas dans les limites du graphe" );
            }
        }

        public int Count
        {
            get
            {
                return m_nodes.Count;
            }
        }

        private List<Node> m_nodes = new List<Node>();
    }
}
