using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Move
    {
        public short row;
        public short col;

        public Move() { }

        /// <summary>
        /// Customized constructor
        /// </summary>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        public Move(short x, short y)
        {
            row = x;
            col = y;
        }

        public Move(Move m)
        {
            row = m.row;
            col = m.col;
        }

    }
}
