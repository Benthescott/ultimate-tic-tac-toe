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

        public Node()
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Player"></param>
        /// <returns></returns>
        public List<Node> CreateSubtree(bool Player)
        {
            List<Node> children = new List<Node>();

            /// TODO Create subtree here and return the results

            return children;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Player"></param>
        /// <returns></returns>
        public bool IsTerminalNode(bool Player)
        {
            bool terminalNode = false;

            /// TODO Check if game is over

            return terminalNode;
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
        public int evaluate()
        {
            int result = 0;

            /// TODO make function body

            return result;
        }
    }
}
