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
        private Move SelectedMove;
        public Board board;

        public AlphaBeta()
        {
            IsMaxPlayer = true;
            board = new Board();
        }


        /// <summary>
        ///     Initializes board to the current state of the game board.
        /// </summary>
        /// <param name="b"></param>
        private void InitBoard(Board b)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.board.boardStatus[i, j] = b.boardStatus[i, j];
                    this.board.gameBoard[i, j]   = b.gameBoard[i, j];
                }
            }
        }


        /// <summary>
        ///     Creates and returns a Node with the coordinates of the most recent move.
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private Node CreateNode(Move m)
        {
            return new Node(m);
        }




        /// <summary>
        ///     This method is the exposed method for the model to receive the 
        ///     AI's move.
        /// </summary>
        /// <param name="b"></param>
        /// <param name="m"></param>
        /// <returns></returns>
        public Move MakeAIMove(Board b, Move m)
        {
            InitBoard(b);
            int value = AB(CreateNode(m), 100, int.MinValue, int.MaxValue, true);

            return SelectedMove;
        }


        /// <summary>
        ///     This function makes a valid move based on the current board.
        /// </summary>
        /// <returns></returns>
        public Move MakeMove()
        {
            return new Ultimate_tic_tac_toe.Move();
        }


        /// <summary>
        ///     This function reverts the most recent move made on the game board.
        /// </summary>
        public void UndoMove()
        {

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

            if (depth == 0 || IsTerminalNode(depth))
            {
                return evaluate(player);
            }

            if (player == this.IsMaxPlayer)
            {
                foreach (Node child in Subtree(player))
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
                foreach (Node child in Subtree(player))
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




        /// <summary>
        /// 
        ///     Creates subtree to max depth, DFS style
        /// 
        /// </summary>
        /// <param name="Player"></param>
        /// <returns></returns>
        public List<Node> Subtree(bool Player)
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
        public bool IsTerminalNode(int depth)
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
                    if (board.boardStatus[row, col] == 'X' && board.boardStatus[row + 3, col] == 'X'
                        && board.boardStatus[row + 6, col] == 'X')
                        terminalNode = true;
                    else if (board.boardStatus[row, col] == 'O' && board.boardStatus[row + 3, col] == 'O'
                        && board.boardStatus[row + 6, col] == 'O')
                        terminalNode = true;
                }

                // Reset number
                col = 1;

                // Check for a horizontal win
                for (row -= 3; row >= 1; row -= 3)
                {
                    if (board.boardStatus[row, col] == 'X' && board.boardStatus[row, col + 3] == 'X'
                        && board.boardStatus[row, col + 6] == 'X')
                        terminalNode = true;
                    else if (board.boardStatus[row, col] == 'O' && board.boardStatus[row, col + 3] == 'O'
                        && board.boardStatus[row, col + 6] == 'O')
                        terminalNode = true;
                }

                // Reset number
                row = 1;

                // Check for a diagonal win
                if (board.boardStatus[row, col] == 'X' && board.boardStatus[row + 3, col + 3] == 'X'
                    && board.boardStatus[row + 6, col + 6] == 'X')
                    terminalNode = true;
                else if (board.boardStatus[row, col] == 'O' && board.boardStatus[row + 3, col + 3] == 'O'
                    && board.boardStatus[row + 6, col + 6] == 'O')
                    terminalNode = true;
                else
                {
                    if (board.boardStatus[row, col + 6] == 'X' && board.boardStatus[row + 3, col + 3] == 'X'
                        && board.boardStatus[row + 6, col] == 'X')
                        terminalNode = true;
                    else if (board.boardStatus[row, col + 6] == 'O' && board.boardStatus[row + 3, col + 3] == 'O'
                        && board.boardStatus[row + 6, col] == 'O')
                        terminalNode = true;
                }

                // Check TIE condition
                for (; col <= 7; col += 3)
                    if (!(board.boardStatus[row, col] == 'B' || board.boardStatus[row + 3, col] == 'B'
                        || board.boardStatus[row + 6, col] == 'B'))
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

            /// TODO write actual heuristic function.

            return rnd.Next(11);
        }
    }
}
