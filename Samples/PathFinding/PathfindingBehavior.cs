using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Troll3D.Components;
using Troll3D;


using Troll3D.Common.IA.PathFinding;
using Troll3D.Common.Graphs;

namespace PathFinding
{
    public class PathfindingBehavior : Behaviour
    {
        public override void Initialize()
        {
            m_tilemap = ( TileMap )Entity.GetComponent( ComponentType.TileMap );

            // On définit les valeurs par défault des cases de départ et d'arrivé comme
            // étant les coins opposés de la grille

            m_startX = 0;
            m_startY = 0;

            Width   = 10;
            Height  = 10;

            m_endX = Width-1;
            m_endY = Width-1;

            m_tilemap.SetTileVal( m_startX, m_startY, 2 );
            m_tilemap.SetTileVal( m_endX,   m_endY, 3 );

            m_graph = new Graph();

            for ( int i = 0; i < Width; i++ )
            {
                for ( int j = 0; j < Width; j++ )
                {
                    m_graph.AddNode( new AStarNode( ( short )( i * Width + j ), j, i ) );
                }
            }

            // On définit les voisins des noeuds du graphe
            for ( int i = 0; i < Width; i++ )
            {
                for ( int j = 0; j < Width; j++ )
                {
                    if(j<9)
                    {
                        m_graph.GetNode( i * Width + j ).ConnectTwoWays( m_graph.GetNode( i * Width + j + 1 ) );
                    }

                    if(i<9)
                    {
                        m_graph.GetNode( i * Width + j ).ConnectTwoWays( m_graph.GetNode( ( i + 1 ) * Width + j ) );
                    }
                }
            }
        }

        public void ComputePath()
        {
            //ComputeAStarPath();
            ComputeDijstraPath();
        }

        /// <summary>
        /// Remet à zéro la tilemap. Supprime les obstacles et remets les noeuds Start et end à leur 
        /// position initiale
        /// </summary>
        public void ResetTilemap()
        {
            for ( int i = 0; i < 10; i++ )
            {
                for ( int j = 0; j < 10; j++ )
                {
                    if ( j == m_startX && i == m_startY )
                    {
                        m_tilemap.SetTileVal( j, i, 2 );
                    }
                    else if ( j == m_endX && i == m_endY)
                    {
                        m_tilemap.SetTileVal( j, i, 3 );
                    }
                    else
                    {
                        m_tilemap.SetTileVal( j, i, 0 );
                    }
                }
            }
        }

        public override void OnKeyDown( KeyboardEvent e )
        {
            //On calcule et affiche le chemin
            if ( e.keycode_ == KeyCode.Key_S )
            {
                ComputePath();
            }
        }

        /// <summary>
        /// On remet à zéro les valeurs permettant de drag les cases start et end
        /// </summary>
        /// <param name="e"></param>
        public override void OnMouseUp( MouseEvent e )
        {
            m_isDown    = false;
            m_dragEnd   = false;
            m_dragStart = false;
        }

        /// <summary>
        /// Déplace les cases start et end si l'utilisteur les "drag"
        /// </summary>
        /// <param name="e"></param>
        public override void OnMouseMove( MouseEvent e )
        {
            if ( m_isDown )
            {
                int x = ( int )( ( ( float )e.mouse_.x / ( float )Screen.Instance.Width ) * 10.0f );
                int y = ( int )( ( ( float )e.mouse_.y / ( float )Screen.Instance.Height ) * 10.0f );

                if ( x < 10 && y < 10 )
                {
                    if ( m_dragStart )
                    {
                        if ( !( x == m_endX && y == m_endY ) )
                        {
                            m_tilemap.SetTileVal( m_startX, m_startY, 0 );
                            m_startX = x;
                            m_startY = y;
                            m_tilemap.SetTileVal( m_startX, m_startY, 2 );
                        }
                    }
                    if ( m_dragEnd )
                    {
                        if ( !( x == m_startX && y == m_startY ) )
                        {
                            m_tilemap.SetTileVal( m_endX, m_endY, 0 );
                            m_endX = x;
                            m_endY = y;
                            m_tilemap.SetTileVal( m_endX, m_endY, 3 );
                        }
                    }
                }
            }
        }

        public override void OnMouseDown( MouseEvent e )
        {
            int x = (int)( (( float )e.mouse_.x / ( float )Screen.Instance.Width ) * 10.0f);
            int y = ( int )(( ( float )e.mouse_.y / ( float )Screen.Instance.Height ) * 10.0f);

            if ( x == m_startX && y == m_startY)
            {
                m_isDown = true;
                m_dragStart = true;
            }

            if ( x == m_endX && y == m_endY )
            {
                m_isDown    = true;
                m_dragEnd   = true;
            }
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Count { get { return Width * Height;} }

        /// <summary>
        /// Affiche le chemin en coloriant les tuiles en bleu
        /// </summary>
        /// <param name="nodes"></param>
        private void DisplayPath( List<Node> nodes )
        {
            for ( int i = 0; i < nodes.Count; i++ )
            {
                int x = nodes[i].Id % Width;
                int y = nodes[i].Id / Width;

                // On vérifie que la tuile ne soit pas celle d'arrivée
                if (!( x == m_endX && y== m_endY ))
                {
                    m_tilemap.SetTileVal( x, y, 5 );
                }
            }
        }

        /// <summary>
        /// Calcule le chemin en utilisant l'algorithme de A étoile
        /// </summary>
        private void ComputeAStarPath()
        {
            AStar astar = new AStar( m_graph );
            astar.Start( m_graph.GetNode( m_startY * Width + m_startX ), m_graph.GetNode( m_endY * Width + m_endX ) );
            ResetTilemap();
            DisplayPath( astar.Execute() );
        }

        /// <summary>
        /// Calcule le chemin en utilisant l'algorithme de Dijkstra
        /// </summary>
        private void ComputeDijstraPath()
        {
            Dijkstra dijkstra = new Dijkstra( m_graph );
            dijkstra.Start( m_graph.GetNode( m_startY * Width + m_startX ), m_graph.GetNode( m_endY * Width + m_endX ) );
            ResetTilemap();
            DisplayPath( dijkstra.Execute() );
        }

        private List<int> Cells = new List<int>();

        private bool m_isDown       = false;
        private bool m_dragStart    = false;
        private bool m_dragEnd      = false;

        private TileMap m_tilemap;

        private Graph   m_graph;

        private int     m_startX;
        private int     m_startY;

        private int     m_endX;
        private int     m_endY;
    }
}
