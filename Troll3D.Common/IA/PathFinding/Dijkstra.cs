using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Common.Graphs;

namespace Troll3D.Common.IA.PathFinding
{
    public class Dijkstra
    {
        public Dijkstra( Graph graph )
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
            AddToOpenList( startNode );
        }

        public bool Process()
        {
            if ( m_openList.Count > 0 )
            {
                // On récupère le noeud dont le cout est le plus faible

                Node node  = PickCheapestNode();

                // On tiens à jour une variable qui contient le noeud le plus proche de la destination souhaité
                // au cas ou on serait incapable d'atteindre cette dernière
                RegisterClosestNode(node);

                if ( node == m_end )
                {
                    m_pathfound = true;
                    return false;
                    // ProcessOver
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
                    SetCost( fetchednode, GetCost(node)+1 );
                    AddToOpenList( fetchednode );
                }
            }
        }

        /// <summary> Retourne la liste des noeuds traversé formant le chemin entre les noeuds de début et d'arrivé</summary>
        public List<Node> GetPath()
        {
            List<Node> path = new List<Node>();
            Node node = GetClosestNode();

            while ( GetParent(node)!= null )
            {
                path.Insert( 0, node );
                node = GetParent( node );
            }

            return path;
        }

        /// <summary>
        /// Récupère le noeud de cout le plus faible dans la liste ouverte. Comme la liste est trié, c'est toujours le premier élément 
        /// de la liste. Le noeud est ajouté à la liste fermée
        /// </summary>
        /// <returns></returns>
        private Node PickCheapestNode()
        {
            Node node = m_openList[0];
            SetNodeListed( node, true );
            m_openList.RemoveAt( 0 );
            m_closedList.Add( node );
            return node;
        }


        /// <summary>
        /// Ajoute le noeud passé en paramètre dans la liste ouverte
        /// </summary>
        /// <param name="node"></param>
        private void AddToOpenList( Node node )
        {
            // On s'assure que le premier élément soit toujours l'élément au cout le plus faible

            bool inserted = false;
            SetNodeListed( node, true );

            for ( int i = 0; i < m_openList.Count && inserted == false; i++ )
            {
                if ( GetCost( m_openList[i] ) > GetCost( node ) )
                {
                    m_openList.Insert( i, node );
                    inserted = true;
                }
            }
            if ( inserted == false )
            {
                m_openList.Add( node );
            }
        }

        /// <summary> 
        /// Cette méthode existe dans le cas ou l'algorithme de recherche n'aurait trouvé aucun chemin. Dans ce cas, il retourne
        /// le noeud le plus proche de la destination souhaité
        /// </summary>
        private Node GetClosestNode()
        {
            if ( m_pathfound == false )
            {
                return m_closestNode;
            }
            else
            {
                return m_closedList[m_closedList.Count - 1];
            }
        }

        /// <summary>
        /// Cette fonction se charge de vérifier si le noeud qui a été selectionné est plus près ou non de l'objectif,
        /// si c'est le cas, on l'enregistre. Cette étape est réalisé dans le cas ou l'on serait incapable de trouver un chemin
        /// vers la position souhaité. Dans ce cas de figure, on retourne un chemin jusqu'au noeud le plus proche de la position souhaité
        /// </summary>
        private void RegisterClosestNode(Node currentNode)
        {
            if ( m_closestNode== null )
            {
                m_closestNode = currentNode;
            }
            else if ( GetCost( m_closestNode ) > GetCost( currentNode ) )
            {
                m_closestNode = currentNode;
            }
        }

        public void Initialize( Node start, Node end )
        {
            m_cost      = new int[m_graph.Count];
            m_parents   = new int[m_graph.Count];
            m_isListed  = new bool[m_graph.Count];
            Reset();

            m_start = start;
            m_end   = end;
        }

        public void Reset()
        {
            for ( int i = 0; i < m_cost.Length; i++ )
            {
                m_cost[i]       = 0;
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

        public int GetCost( Node node )
        {
            return m_cost[node.Id];
        }

        private void SetCost( Node node, int cost )
        {
            m_cost[node.Id] = cost;
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

        private int[] m_cost;
        private int[] m_parents;
        private bool[] m_isListed;

        private List<Node> m_closedList = new List<Node>();
        private List<Node> m_openList   = new List<Node>();

        private Node m_start;
        private Node m_end;
    }
}
