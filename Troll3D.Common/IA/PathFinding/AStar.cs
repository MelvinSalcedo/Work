using System;
using System.Collections.Generic;

using Troll3D.Common.Graphs;

namespace Troll3D.Common.IA.PathFinding
{
    /// <summary> Expliquer le fonctionnement de l'algorithme serait relativement sympa lolilol </summary>
    public class AStar
    {
        public AStar( Graph graph )
        {
            Graph = graph;
        }

        /// <summary> 
        /// Initialise la recherche de chemin
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public void Start( Node start, Node end )
        {
            pathfound = false;
            Initialize(start, end);
            AddToOpenList( StartNode );
        }

        /// <summary>
        /// Exécute le processus de recherche de chemin dans son intégralité et retourne le chemin trouvé sous la forme
        /// d'une liste de noeuds
        /// </summary>
        public List<Node> Execute()
        {
            while ( Process() ) { }
            return GetPath();
        }

        /// <summary> Exécute seulement une étape du processus de recherche de chemin, utilise si on souhaite 
        /// décomposer le fonctionnement de l'algorithme</summary>
        public bool Process()
        {
            if ( openlist_.Count > 0 )
            {
                // On récupère le noeud dont le cout est le plus faible

                currentnode_ = PickCheapestNode();

                // On tiens à jour une variable qui contient le noeud le plus proche de la destination souhaité
                // au cas ou on serait incapable d'atteindre cette dernière
                RegisterClosestNode();

                if ( currentnode_ == EndNode )
                {
                    pathfound = true;
                    return false;
                    // ProcessOver
                }
                else
                {
                    // On inspecte les voisins du noeud extrait par l'aglorithme de sélection
                    ManageNeighbours(currentnode_);
                    return true;
                }
            }
            else
            {
                return false;
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


        public Node closestnode_;

        public bool pathfound = false;

        /// <summary>
        /// Inspecte les voisins du noeud passé en paramètre et les rajoute dans la liste ouverte
        /// si cela n'a pas déjà été fait
        /// </summary>
        private void ManageNeighbours(Node node)
        {
            for ( int i = 0; i < node.NeighbourCount; i++ )
            {
                if ( IsNodeListed( node.GetNeighbour( i ) ) == false )
                {
                    Node fetchednode = node.GetNeighbour( i );
                    SetParent( fetchednode, node );
                    SetCost( fetchednode , Fitness(fetchednode));
                    AddToOpenList( fetchednode );
                }
            }
        }

        /// <summary>
        /// Récupère le noeud de cout le plus faible dans la liste ouverte. Comme la liste est trié, c'est toujours le premier élément 
        /// de la liste. Le noeud est ajouté à la liste fermée
        /// </summary>
        /// <returns></returns>
        private Node PickCheapestNode()
        {
            Node node = openlist_[0];
            SetNodeListed( node, true );
            openlist_.RemoveAt( 0 );
            closedlist_.Add( node );
            return node;
        }

        private void AddToOpenList( Node node )
        {
            // On s'assure que le premier élément soit toujours l'élément au cout le plus faible

            bool inserted = false;
            SetNodeListed( node, true );

            for ( int i = 0; i < openlist_.Count && inserted == false; i++ )
            {
                if ( GetCost( openlist_[i]) > GetCost(node) )
                {
                    openlist_.Insert( i, node );
                    inserted = true;
                }
            }
            if ( inserted == false )
            {
                openlist_.Add( node );
            }
        }

        /// <summary> La principale différence avec les autres algorithmes de recherche de chemin
        /// est que AStar a besoin d'une heuristique pour donner un "cout" à un noeud. Généralement, on utilise
        /// la distance à vol d'oiseau entre le noeud actuel et la destination finale
        /// </summary>
        /// <returns></returns>
        public virtual float Fitness(Node node)
        {
            return ( ( ( AStarNode )EndNode ).Position - ( ( ( AStarNode )node ).Position ) ).Length();
        }

        int failsafe = 0;
        private Node currentnode_;

        public List<Node> openlist_     = new List<Node>();
        public List<Node> closedlist_   = new List<Node>();



        /// <summary> Cette méthode existe dans le cas ou l'algorithme de recherche n'aurait trouvé aucun chemin. Dans ce cas, il retourne
        /// le noeud le plus proche de la destination souhaité</summary>
        private Node GetClosestNode()
        {
            if ( pathfound == false )
            {
                return closestnode_;
            }
            else
            {
                return closedlist_[closedlist_.Count - 1];
            }
        }

        /// <summary>
        /// Cette fonction se charge de vérifier si le noeud qui a été selectionné est plus près ou non de l'objectif,
        /// si c'est le cas, on l'enregistre. Cette étape est réalisé dans le cas ou l'on serait incapable de trouver un chemin
        /// vers la position souhaité. Dans ce cas de figure, on retourne un chemin jusqu'au noeud le plus proche de la position souhaité
        /// </summary>
        private void RegisterClosestNode()
        {
            if ( closestnode_ == null )
            {
                closestnode_ = currentnode_;
            }
            else if ( GetCost( closestnode_ ) > GetCost( currentnode_ ) )
            {
                closestnode_ = currentnode_;
            }
        }

        private void Initialize( Node start, Node end )
        {
            NodeCost    = new float[Graph.Count];
            ParentNode  = new int[Graph.Count];
            IsListed    = new bool[Graph.Count];
            Reset();

            StartNode   = start;
            EndNode     = end;
        }

        public void Reset()
        {
            for ( int i = 0; i < NodeCost.Length; i++ )
            {
                NodeCost[i]     = 0;
                ParentNode[i]   = -1;
            }
            OpenList.Clear();
            ClosedList.Clear();
        }

        public bool IsNodeListed( Node node )
        {
            return IsListed[node.Id];
        }

        public void SetNodeListed( Node node, bool value )
        {
            IsListed[node.Id] = value;
        }

        public float GetCost( Node node )
        {
            return NodeCost[node.Id];
        }

        private void SetCost( Node node, float cost )
        {
            NodeCost[node.Id] = cost;
        }

        private Node GetParent( Node node )
        {
            if ( ParentNode[node.Id] == -1 )
            {
                return null;
            }
            return Graph.GetNode(ParentNode[node.Id]);
        }

        public void SetParent( Node node, Node parent )
        {
            ParentNode[node.Id] = parent.Id;
        }

        /// <summary> Enregistre les couts des noeuds calculés par A * </summary>
        public float [] NodeCost;

        /// <summary> Enregistre les identifiants de parent d'un noeud </summary>
        public int[] ParentNode;

        /// <summary> Enregistre si un noeud est listé</summary>
        public bool[] IsListed;

        /// <summary>Enregistre les identifiants des noeuds dans la liste ouverte</summary>
        public Stack<int>   OpenList = new Stack<int>();

        /// <summary>Enregistre les identifiants des noeuds dans la liste fermée </summary>
        public Stack<int>   ClosedList = new Stack<int>();

        public Node StartNode { get; private set; }
        /// <summary> Correspond à la destination souhaité </summary>
        public Node EndNode { get; private set; }

        /// <summary>Noeud en cours d'analyse</summary>
        public Node CurrentNode { get; set; }

        public Graph Graph { get; private set; }

    }
}
