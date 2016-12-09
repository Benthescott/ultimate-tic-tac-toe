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

        /// <summary>
        /// Default constructor
        /// </summary>
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
    }
}
