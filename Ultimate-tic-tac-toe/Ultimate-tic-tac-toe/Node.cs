using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Node
    {
        public Board board;

        public Node()
        {

        }

        /// <summary>
        /// 
        ///     Creates subtree to max depth, DFS style
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
        ///     Checks if current node is a leaf node or not. Max depth or game over.
        /// 
        /// </summary>
        /// <param name="Player"></param>
        /// <returns></returns>
        public bool IsTerminalNode(Node node, int depth)
        {
            bool terminalNode = false;

            if (depth >= 100)
            {
                terminalNode = true;
            }
            else
            {
                // First mini game status to check is located at 1,1 in boardStatus array
                //      The boards are at 1,1; 1,4; 1,7; 4,1; 4,4; 4,7; etc
                int col = 1;
                int row = 1;

                // Check for a vertical win
                for (; col <= 7; col += 3)
                {
                    if (node.board.boardStatus[row, col] == 'X' && node.board.boardStatus[row + 3, col] == 'X'
                        && node.board.boardStatus[row + 6, col] == 'X')
                        terminalNode = true;
                    else if (node.board.boardStatus[row, col] == 'O' && node.board.boardStatus[row + 3, col] == 'O'
                        && node.board.boardStatus[row + 6, col] == 'O')
                        terminalNode = true;
                }

                // Reset number
                col = 1;

                // Check for a horizontal win
                for (row -= 3; row >= 1; row -= 3)
                {
                    if (node.board.boardStatus[row, col] == 'X' && node.board.boardStatus[row, col + 3] == 'X'
                        && node.board.boardStatus[row, col + 6] == 'X')
                        terminalNode = true;
                    else if (node.board.boardStatus[row, col] == 'O' && node.board.boardStatus[row, col + 3] == 'O'
                        && node.board.boardStatus[row, col + 6] == 'O')
                        terminalNode = true;
                }

                // Reset number
                row = 1;

                // Check for a diagonal win
                if (node.board.boardStatus[row, col] == 'X' && node.board.boardStatus[row + 3, col + 3] == 'X'
                    && node.board.boardStatus[row + 6, col + 6] == 'X')
                    terminalNode = true;
                else if (node.board.boardStatus[row, col] == 'O' && node.board.boardStatus[row + 3, col + 3] == 'O'
                    && node.board.boardStatus[row + 6, col + 6] == 'O')
                    terminalNode = true;
                else
                {
                    if (node.board.boardStatus[row, col + 6] == 'X' && node.board.boardStatus[row + 3, col + 3] == 'X'
                        && node.board.boardStatus[row + 6, col] == 'X')
                        terminalNode = true;
                    else if (node.board.boardStatus[row, col + 6] == 'O' && node.board.boardStatus[row + 3, col + 3] == 'O'
                        && node.board.boardStatus[row + 6, col] == 'O')
                        terminalNode = true;
                }

                // Check TIE condition
                for (; col <= 7; col += 3)
                    if (!(node.board.boardStatus[row, col] == 'B' || node.board.boardStatus[row + 3, col] == 'B'
                        || node.board.boardStatus[row + 6, col] == 'B'))
                        terminalNode = true;
            }
            return terminalNode;
        }



        /// <summary>
        /// 
        ///     This function statically evaluates a given node in
        ///     the tree. Gives heuristic value of node.
        /// 
        /// </summary>
        /// <param name="player">
        /// 
        ///     Boolean to indicate max or min player.
        ///     
        /// </param>
        /// <returns>
        /// 
        ///     Returns the heuristic value
        ///     of the node as an integer.
        /// 
        /// </returns>
        public int evaluate(bool player)
        {
            Random rnd = new Random();

            /// TODO make function body

            return rnd.Next(11);
        }
    }
}
