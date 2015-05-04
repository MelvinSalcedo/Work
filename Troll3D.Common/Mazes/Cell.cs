using System;
using System.Collections.Generic;

namespace Troll3D.Common.Mazes
{

    public class Cell
    {
        public Cell(int x, int y, int id, bool iswall = false)
        {
            x_          =   x;
            y_          =   y;
            id_         = id;
            visited_    =   false;
            iswall_     =   iswall;
            isopen_     =   false;
        }

        public Cell(Cell cell)
        {
            x_          =   cell.x;
            y_          =   cell.y;
            id_         =   cell.Id;
            visited_    =   cell.visited_;
            iswall_     =   cell.iswall_;
            isopen_     =   cell.isopen_;
        }

        public int x 
        {
            get{ return x_;}
        }

        public int y 
        {
            get { return y_; }
        }

        public int Id 
        {
            get { return id_; }
        }

        public bool visited_;
        public bool iswall_;
        public bool isopen_;

        private int id_;
        private int x_;
        private int y_;
    }
}
