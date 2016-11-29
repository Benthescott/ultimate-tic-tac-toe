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

        public AlphaBeta()
        {
            IsMaxPlayer = true;
        }

        private Node ConvertToNode(Board b)
        {
            Node n = new Node();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    n.board.gameBoard[i, j] = b.gameBoard[i, j];
                }
            }

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    n.board.boardStatus[i, j] = b.boardStatus[i, j];
                }
            }

            return n;
        }

        public void MakeMove(Board b, Move s)
        {
            int value = AB(ConvertToNode(b), 100, int.MinValue, int.MaxValue, true);
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
        public int AB(Node currentNode, int depth, int alpha, int beta, bool player)
        {
            // If we have drilled down to the max depth, or we found a leaf node,
            // evaluate the node.
            if (depth == 0 || currentNode.IsTerminalNode(currentNode, depth))
            {
                return currentNode.evaluate(player);
            }

            if (player == this.IsMaxPlayer)
            {
                foreach (Node child in currentNode.CreateSubtree(player))
                {
                    alpha = Math.Max(alpha, AB(child, depth - 1, alpha, beta, !player));
                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return alpha;
            }
            else
            {
                foreach (Node child in currentNode.CreateSubtree(player))
                {
                    beta = Math.Min(beta, AB(child, depth - 1, alpha, beta, !player));

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                return beta;
            }
        }
    }
}
