using System;
using System.Collections.Generic;

namespace Troll3D.Common.Mazes
{

    /// <summary>
    /// Un labyrinthe est composé de cellule qui peuvent être des murs (ou pas)
    /// Le labyrinthe est généré du coin inférieur gauche au coin supérieur droit :
    /// 
    ///              (1,1)
    ///
    ///  (0,0)
    /// </summary>
    public class Maze
    {
        public Maze( int width, int height )
        {
            Initialize( width, height );
            MakeCells();
        }

        public Maze( Maze maze )
        {
            Initialize( maze.Width, maze.Height );
            CopyCells( maze.cells_ );
        }

        public void Initialize( int width, int height )
        {
            width_ = width;
            height_ = height;
            cells_ = new List<Cell>();
        }

        public int Count()
        {
            return ( ( height_ * 2 ) + 1 ) * ( ( width_ * 2 ) + 1 );
        }

        public Cell GetCell( int x, int y )
        {
            if ( y >= 0 && y < ( ( height_ * 2 ) + 1 ) && x >= 0 && x < ( ( width_ * 2 ) + 1 ) )
            {
                return cells_[y * ( ( width_ * 2 ) + 1 ) + x];
            }
            else return null;
        }

        public float[] Texture
        {
            get { return MakeTexture32Bits(); }
        }

        public int Width
        {
            get { return width_; }
        }

        public int Height
        {
            get { return height_; }
        }

        public List<Cell> CellsCopy
        {
            get
            {
                List<Cell> cells = new List<Cell>();
                for ( int i = 0; i < cells_.Count; i++ )
                {
                    cells.Add( cells_[i] );
                }

                return cells_;
            }
        }

        private void CopyCells( List<Cell> cells )
        {
            for ( int i = 0; i < cells.Count; i++ )
            {
                cells_.Add( new Cell( cells[i] ) );
            }
        }

        private void MakeCells()
        {
            for ( int i = 0; i < ( height_ * 2 ) + 1; i++ )
            {
                for ( int j = 0; j < ( width_ * 2 ) + 1; j++ )
                {
                    Cell newcell;

                    if ( i % 2 == 0 )
                    {
                        newcell = new Cell( j, i, i * ( ( width_ * 2 ) + 1 ) + j, true );
                    }
                    else
                    {
                        if ( j % 2 == 0 )
                        {
                            newcell = new Cell( j, i, i * ( ( width_ * 2 ) + 1 ) + j, true );
                        }
                        else
                        {
                            newcell = new Cell( j, i, i * ( ( width_ * 2 ) + 1 ) + j, false );
                        }
                    }
                    cells_.Add( newcell );
                }
            }
        }

        private float[] MakeTexture32Bits()
        {
            float[] data = new float[Count() * 4];

            for ( int i = 0; i < Count(); i++ )
            {
                if ( !cells_[i].iswall_ )
                {
                    data[i * 4] = 1.0f;
                    data[i * 4 + 1] = 1.0f;
                    data[i * 4 + 2] = 1.0f;
                    data[i * 4 + 3] = 1.0f;
                }
                else if ( cells_[i].isopen_ )
                {
                    data[i * 4] = 1.0f;
                    data[i * 4 + 1] = 1.0f;
                    data[i * 4 + 2] = 1.0f;
                    data[i * 4 + 3] = 1.0f;
                }
                else
                {
                    data[i * 4] = 0.0f;
                    data[i * 4 + 1] = 0.0f;
                    data[i * 4 + 2] = 0.0f;
                    data[i * 4 + 3] = 1.0f;
                }
            }
            return data;
        }

        ///      - - - - -       (2*2)+1 murs
        ///      | o | o |
        ///      - - - - -
        ///      | o | o |
        ///      - - - - -

        /// <summary>
        ///  La liste de cellule comporte toute les cellules, les mur ainsi que les "salles"
        /// </summary>
        public List<Cell> cells_;

        private int width_;
        private int height_;
    }

}



