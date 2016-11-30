using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class AlphaBeta
    {

        private bool IsMaxPlayer;
        public Move SelectedMove;

        public char turn;
        public int boardSelected;

        public AlphaBeta(char playerTurn, int selected)
        {
            IsMaxPlayer = true;
            SelectedMove = new Move();
            turn = playerTurn;
            boardSelected = selected;
        }

        /// <summary>
        /// 
        ///     This function is the main control loop for the alpha beta algorithm.
        /// 
        /// </summary>
        /// <param name="currentNode"></param>
        /// <param name="depth"></param>
        /// <param name="alpha"></param>
        /// <param name="beta"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public int Loop(Node currentNode, int depth, int alpha, int beta, bool player)
        {
            // If we have drilled down to the max depth, or we found a leaf node,
            // evaluate the node.

            return 0;
            
            if (depth == 0 || currentNode.IsTerminalNode())
            {
                return currentNode.evaluate(player);
            }

            if (player == this.IsMaxPlayer)
            {
                foreach (Node child in currentNode.Children(player))
                {
                    alpha = Math.Max(alpha, Loop(child, depth + 1, alpha, beta, !player));
                    if (beta < alpha)
                    {
                        break;
                    }

                }

                return alpha;
            }
            else
            {
                foreach (Node child in currentNode.Children(player))
                {
                    beta = Math.Min(beta, Loop(child, depth + 1, alpha, beta, !player));

                    if (beta < alpha)
                    {
                        break;
                    }
                }

                return beta;
            }
            
        }
    }
}
