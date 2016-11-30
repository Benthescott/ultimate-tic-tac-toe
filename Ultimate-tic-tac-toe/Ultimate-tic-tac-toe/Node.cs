using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Node
    {
        public Move CurrentMove;
        public int Value;

        public Node()
        {
            this.CurrentMove = new Move();
        }

        public Node(Move m)
        {
            this.CurrentMove = new Move();

            this.CurrentMove.row = m.row;
            this.CurrentMove.col = m.col;
        }

        public Node(int x, int y)
        {
            this.CurrentMove = new Move();

            this.CurrentMove.row = x;
            this.CurrentMove.col = y;
        }
    }
}
