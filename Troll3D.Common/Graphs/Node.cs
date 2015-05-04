using System;
using System.Collections.Generic;

namespace Troll3D.Common.Graphs
{
    /// <summary> Un noeud d'un graphe est connecté à d'autres noeuds </summary>
    public class Node
    {
        public Node( int id )
        {
            Id = id;
        }

        /// <summary>
        /// Connecte 2 noeuds dans un seul sens(crée un arc)
        /// </summary>
        public void ConnectNode( Node node )
        {
            if ( !m_nodes.Contains( node ) )
            {
                m_nodes.Add( node );
            }
        }

        /// <summary> Connecte les deux noeuds entre eux (crée une nouvelle arrête) </summary>
        public void ConnectTwoWays( Node node )
        {
            ConnectNode( node );
            node.ConnectNode( this );
        }

        /// <summary> Supprime une arrête entre 2 noeuds </summary>
        public void DisconnectNode( Node node )
        {
            if ( m_nodes.Contains( node ) )
            {
                m_nodes.Remove( node );
            }
        }

        /// <summary> Supprime une arrête à double sens entre deux noeuds </summary>
        public void DisconnectNodeTwoWays( Node node )
        {
            DisconnectNode( node );
            node.DisconnectNode( this );
        }

        /// <summary> Retourne le voisin à l'index demandé</summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Node GetNeighbour( int index )
        {
            try
            {
                return m_nodes[index];
            }
            catch
            {
                throw new Exception( "Erreur, le voisin demandé n'existe pas" );
            }
        }

        /// <summary>
        /// Retourne le nombre de voisins connecté à ce noeud
        /// </summary>
        public int NeighbourCount
        {
            get
            {
                return m_nodes.Count;
            }
        }

        /// <summary>Permet d'identifier un noeud du graphe</summary>
        public int Id{get;private set;}

        private List<Node> m_nodes = new List<Node>();
    }
}
