using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class AlphaBeta
    {

        private Board BoardState;
        private bool MaxPlayer = true;
        private short TreeDepth;
        private short MaxTreeDepth;

        public AlphaBeta()
        {
        }

        public Board MakeAIMove(Board b, short boardNum, short depth)
        {
            this.TreeDepth = 0;
            this.BoardState = new Board(b);
            this.MaxTreeDepth = depth;

            int result = this.AB(new Node(this.TreeDepth, boardNum, this.MaxPlayer), depth, short.MinValue, short.MaxValue, MaxPlayer);

            return this.BoardState;
        }

        private Node MakeMove(bool player, short boardNum)
        {
            // Make new node
            Node node = new Node();
            
            // If 'O' player
            if (player == this.MaxPlayer)
            {
                // If the board to play on is not complete (win / tie)
                if (this.BoardState.Main[this.BoardState.BoardCoord(boardNum).Item1, this.BoardState.BoardCoord(boardNum).Item2] == 'B')
                {
                    // Make any 'B' space on boardNum 'O'
                    // Get the first availabe move on the minigame
                    Tuple<short, short> move = this.BoardState.GetOpenMove(this.BoardState.MiniGames[boardNum]);
                    this.BoardState.MiniGames[boardNum][move.Item1, move.Item2] = 'O';
                    // If move made caused Main Board to change, set node.MainChanged to true
                    if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardNum]).Item1)
                    {
                        if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardNum]).Item2 == 'T')
                            this.BoardState.Main[this.BoardState.BoardCoord(boardNum).Item1, this.BoardState.BoardCoord(boardNum).Item2] = 'T';
                        else
                            this.BoardState.Main[this.BoardState.BoardCoord(boardNum).Item1, this.BoardState.BoardCoord(boardNum).Item2] = 'O';
                        node.MainChanged = true;
                    }
                    // Increment this.TreeDepth
                    this.TreeDepth++;

                    // set row/col in node equal to move made
                    node.Row = move.Item1;
                    node.Col = move.Item2;

                    // set node.Depth = this.depth
                    node.Depth = this.TreeDepth;

                    // set node.BoardNumberPlayedOn = to boardNum
                    node.BoardNumberPlayedOn = boardNum;

                    // set node.BoardNumberToPlayOn = board opponent must play on next
                    node.BoardNumberToPlayOn = this.BoardState.MainBoardCoord(move.Item1, move.Item2);

                    // set node.Player = player
                    node.Player = player;
                }
                // 'O' player sent to completed board, can now move anywhere that's open
                else
                {
                    // Get open move on Main
                    Tuple<short, short> mainBoardMove = this.BoardState.GetOpenMove(this.BoardState.Main);

                    // Get Main board number
                    short boardSelected = this.BoardState.MainBoardCoord(mainBoardMove.Item1, mainBoardMove.Item2);

                    // Get miniboard move, set it to 'O'
                    Tuple<short, short> miniBoardMove = this.BoardState.GetOpenMove(this.BoardState.MiniGames[boardSelected]);
                    this.BoardState.MiniGames[boardSelected][miniBoardMove.Item1, miniBoardMove.Item2] = 'O';

                    // If move made caused Main Board to change, set node.MainChanged to true


                }
            }
            // 'X' player
            else
            {
                // Make any 'B' space on boardNum 'X'
                // If move made caused Main Board to change, set node.MainChanged to true

                // Increment this.TreeDepth
                // set row/col in node equal to move made
                // set node.Depth = this.depth
                // set node.BoardNumberPlayedOn = to boardNum
                // set node.BoardNumberToPlayOn = board opponent must play on next
                // set node.Player = player
            }

            return node;
        }

        private void UndoMove(Node n)
        {
            BoardState.MiniGames[n.BoardNumberPlayedOn][n.Row, n.Col] = 'B';
            if (n.MainChanged)
                BoardState.Main[BoardState.BoardCoord(n.BoardNumberPlayedOn).Item1,
                                BoardState.BoardCoord(n.BoardNumberPlayedOn).Item2] = 'B';
        }

        private bool IsTerminal(bool Player, Node node)
        {
            if (node.Depth == 100)
                return true;
            else if (BoardState.BoardComplete(BoardState.Main).Item1)
                return true;
            else
                return false;
        }

        private int evaluate(bool Player)
        {
            return 1;
        }

        private int AB(Node node, int depth, int alpha, int beta, bool player)
        {
            if (depth == 0 || IsTerminal(player, node))
            {
                return evaluate(player);
            }

            if (player == MaxPlayer)
            {
                while (this.TreeDepth < this.MaxTreeDepth)
                {
                    Node child = new Node(this.MakeMove(player, node.BoardNumberToPlayOn));
                    alpha = Math.Max(alpha, AB(child, depth - 1, alpha, beta, !player));

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                // Stack is unwinding, undo last move
                this.UndoMove(node);
                return alpha;
            }
            else
            {
                while (this.TreeDepth < this.MaxTreeDepth)
                {
                    Node child = new Node(this.MakeMove(player, node.BoardNumberToPlayOn));
                    beta = Math.Min(beta, AB(child, depth - 1, alpha, beta, !player));

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                // Stack is unwinding, undo last move
                this.UndoMove(node);
                return beta;
            }
            
        }
    }
}