using System;
using System.Collections.Generic;

namespace Troll3D.Common.Mazes
{
    /// <summary>
    ///  Methode de génération de labyrinthe
    /// </summary>
    public class RecursiveBacktracer
    {
        public RecursiveBacktracer( Maze maze, int seed = 0 )
        {
            maze_ = maze;
            mazecopy_ = new Maze( maze_ );

            copy = new List<Cell>();
            for ( int i = 0; i < mazecopy_.cells_.Count; i++ )
            {
                copy.Add( mazecopy_.cells_[i] );
            }

            seed_ = seed;
            stack = new List<Cell>();

            // Je sélectionne une Cellule au hasard

            rand_ = new Random( seed_ );

            bool isroom = true;

            while ( isroom )
            {
                int pos = rand_.Next() % ( copy.Count - 1 );
                currentcell = copy[pos];
                isroom = currentcell.iswall_;
                copy.RemoveAt( pos );
            }

            currentcell.visited_ = true;
        }


        public int Seed
        {
            get { return seed_; }
        }

        public void Do()
        {
            // Tant qu'il y a des cases  à visiter
            while ( DoOneStep() ) ;
        }

        public bool DoOneStep()
        {
            if ( copy.Count > 0 )
            {
                // Je choisi aléatoirement un des voisins de la case en cours d'analyse

                neighbor = null;
                count = 0;

                // 0 = N, 1 == E, 2 == S, 3 == W
                neighbors = rand_.Next() % 4;

                while ( neighbor == null && count < 4 )
                {

                    if ( neighbors == 0 )
                    {
                        ChoseNeighbor( currentcell.x, currentcell.y + 2 );
                    }
                    else if ( neighbors == 1 )
                    {
                        ChoseNeighbor( currentcell.x + 2, currentcell.y );
                    }
                    else if ( neighbors == 2 )
                    {
                        ChoseNeighbor( currentcell.x, currentcell.y - 2 );
                    }
                    else if ( neighbors == 3 )
                    {
                        ChoseNeighbor( currentcell.x - 2, currentcell.y );
                    }
                }

                if ( neighbor != null )
                {
                    stack.Insert( 0, currentcell );

                    if ( neighbors == 0 )
                    {
                        maze_.GetCell( currentcell.x, currentcell.y + 1 ).isopen_ = true;
                    }
                    else if ( neighbors == 1 )
                    {
                        maze_.GetCell( currentcell.x + 1, currentcell.y ).isopen_ = true;
                    }
                    else if ( neighbors == 2 )
                    {
                        maze_.GetCell( currentcell.x, currentcell.y - 1 ).isopen_ = true;
                    }
                    else if ( neighbors == 3 )
                    {
                        maze_.GetCell( currentcell.x - 1, currentcell.y ).isopen_ = true;
                    }

                    currentcell = neighbor;
                    copy.Remove( currentcell );
                    currentcell.visited_ = true;

                }
                else if ( stack.Count > 0 )
                {
                    currentcell = stack[0];
                    stack.Remove( currentcell );
                } return true;

            }
            else
            {
                return false;
            }
        }


        void ChoseNeighbor( int x, int y )
        {
            Cell cell = maze_.GetCell( x, y );

            if ( cell != null )
            {
                if ( !cell.visited_ )
                {
                    neighbor = cell;
                    // je passe le counteur à 10 pour sortir de la boucle
                    count = 10;
                }
                else
                {
                    neighbors++;
                    neighbors = neighbors % 4;
                    count++;
                }
            }
            else
            {
                neighbors++;
                neighbors = neighbors % 4;
                count++;
            }
        }

        private Maze maze_;
        private Random rand_;
        private Maze mazecopy_;
        private List<Cell> stack;
        private List<Cell> copy;

        private Cell currentcell;
        private Cell neighbor;

        private int count;
        private int neighbors;
        private int seed_;
    }
}