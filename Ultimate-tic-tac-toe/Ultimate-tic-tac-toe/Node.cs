using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Node
    {
        private Board board;
        private int alpha;
        private int beta;
        private int value;

        public char turn;
        public int boardSelected;



        public Node(char t, int selected)
        {
            this.turn = t;
            this.boardSelected = selected;
        }

        /// <summary>
        /// 
        ///     This function statically evaluates the value of a given node in
        ///     the tree.
        /// 
        /// </summary>
        /// <param name="node">
        /// 
        ///     Node to be evaluated.
        ///     
        /// </param>
        /// <returns>
        /// 
        ///     Returns the numeric value
        ///     of the node.
        /// 
        /// </returns>
        private int evaluate(Node node)
        {
            int result = 0;

            // TODO make function body

            return result;
        }
    }
}
