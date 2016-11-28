using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Move
    {
        public int row;
        public int col;

        public Move() { }

        /// <summary>
        /// Customized constructor
        /// </summary>
        /// <param name="x">Row</param>
        /// <param name="y">Column</param>
        public Move(int x, int y) {
            row = x;
            col = y;
        }
    }
}
