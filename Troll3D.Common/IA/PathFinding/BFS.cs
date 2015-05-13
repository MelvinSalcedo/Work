using System;
using System.Collections.Generic;

using Troll3D.Common.Graphs;

namespace Troll3D.Common.IA.PathFinding
{
    /// <summary>
    /// L'algorithme de recherche de chemin fonctionne globalement comme Dijkstra, à la différence prêt que
    /// cette fois, on assigne pas de "poid" aux différents noeuds. On Sélectionne le premier noeud de la liste
    /// ouverte, on ajoute tout ses voisins à la fin de la liste fermé, on met le parent dans la liste fermé
    /// et on sélectionne le premier noeud de la liste ouverte
    /// </summary>
    public class BFS
    {
        public BFS( Graph graph )
        {
            m_graph = graph;
        }

        /// <summary>
        /// Exécute tout le processus de recherche de chemin et retourne le chemin sous la forme d'une liste de noeuds
        /// </summary>
        /// <returns></returns>
        public List<Node> Execute()
        {
            while ( Process() ) { }
            return GetPath();
        }

        /// <summary>
        /// Initialise la recherche de chemin
        /// </summary>
        public void Start( Node startNode, Node endNode )
        {
            Initialize( startNode, endNode );
            m_openList.Add( startNode );
        }

        public bool Process()
        {
            if ( m_openList.Count > 0 )
            {
                // On récupère le premier noeud de la liste ouverte

                Node node = m_openList[0];
                m_openList.RemoveAt( 0 );
                m_isListed[node.Id] = true;

                if ( node == m_end )
                {
                    m_pathfound = true;
                    return false;
                }
                else
                {
                    // On inspecte les voisins du noeud extrait par l'aglorithme de sélection
                    ManageNeighbours( node );
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Inspecte les voisins du noeud passé en paramètre et les rajoute dans la liste ouverte
        /// si cela n'a pas déjà été fait
        /// </summary>
        private void ManageNeighbours( Node node )
        {
            for ( int i = 0; i < node.NeighbourCount; i++ )
            {
                if ( IsNodeListed( node.GetNeighbour( i ) ) == false )
                {
                    Node fetchednode = node.GetNeighbour( i );
                    SetParent( fetchednode, node );
                    m_openList.Add( fetchednode );
                }
            }
        }

        /// <summary> Retourne la liste des noeuds traversé formant le chemin entre les noeuds de début et d'arrivé</summary>
        public List<Node> GetPath()
        {
            List<Node> path = new List<Node>();
            Node node = m_end;

            while ( GetParent(node)!= null )
            {
                path.Insert( 0, node );
                node = GetParent( node );
            }

            return path;
        }

        public void Initialize( Node start, Node end )
        {
            m_parents   = new int[m_graph.Count];
            m_isListed  = new bool[m_graph.Count];
            Reset();

            m_start = start;
            m_end   = end;
        }

        public void Reset()
        {
            for ( int i = 0; i < m_parents.Length; i++ )
            {
                m_parents[i]    = -1;
            }

            m_openList.Clear();
            m_closedList.Clear();
        }

        public bool IsNodeListed( Node node )
        {
            return m_isListed[node.Id];
        }

        public void SetNodeListed( Node node, bool value )
        {
            m_isListed[node.Id] = value;
        }

        private Node GetParent( Node node )
        {
            if ( m_parents[node.Id] == -1 )
            {
                return null;
            }
            return m_graph.GetNode( m_parents[node.Id] );
        }

        private void SetParent( Node node, Node parent )
        {
            m_parents[node.Id] = parent.Id;
        }

        private Node m_closestNode;
        private bool m_pathfound;
        private Graph m_graph;

        private int[] m_parents;
        private bool[] m_isListed;

        private List<Node> m_closedList = new List<Node>();
        private List<Node> m_openList   = new List<Node>();

        private Node m_start;
        private Node m_end;
    }
}
