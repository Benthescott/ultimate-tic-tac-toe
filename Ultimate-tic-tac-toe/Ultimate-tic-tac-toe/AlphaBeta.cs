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

            short result = this.AB(new Node(this.TreeDepth, boardNum, this.MaxPlayer), depth, short.MinValue, short.MaxValue, MaxPlayer);

            return this.BoardState;
        }


        /// <summary>
        ///     This function makes one valid move.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="boardNum"></param>
        /// <returns>
        ///     Returns a new node for the tree.
        /// </returns>
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
                    if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardSelected]).Item1)
                    {
                        if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardSelected]).Item2 == 'T')
                            this.BoardState.Main[this.BoardState.BoardCoord(boardSelected).Item1, this.BoardState.BoardCoord(boardSelected).Item2] = 'T';
                        else
                            this.BoardState.Main[this.BoardState.BoardCoord(boardSelected).Item1, this.BoardState.BoardCoord(boardSelected).Item2] = 'O';
                        node.MainChanged = true;
                    }
                    // Increment this.TreeDepth
                    this.TreeDepth++;

                    // set row/col in node equal to move made
                    node.Row = miniBoardMove.Item1;
                    node.Col = miniBoardMove.Item2;

                    // set node.Depth = this.depth
                    node.Depth = this.TreeDepth;

                    // set node.BoardNumberPlayedOn = to boardNum
                    node.BoardNumberPlayedOn = boardSelected;

                    // set node.BoardNumberToPlayOn = board opponent must play on next
                    node.BoardNumberToPlayOn = this.BoardState.MainBoardCoord(miniBoardMove.Item1, miniBoardMove.Item2);

                    // set node.Player = player
                    node.Player = player;
                }
            }
            // 'X' player
            else
            {
                // If the board to play on is not complete (win / tie)
                if (this.BoardState.Main[this.BoardState.BoardCoord(boardNum).Item1, this.BoardState.BoardCoord(boardNum).Item2] == 'B')
                {
                    // Make any 'B' space on boardNum 'X'
                    // Get the first availabe move on the minigame
                    Tuple<short, short> move = this.BoardState.GetOpenMove(this.BoardState.MiniGames[boardNum]);
                    this.BoardState.MiniGames[boardNum][move.Item1, move.Item2] = 'X';
                    // If move made caused Main Board to change, set node.MainChanged to true
                    if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardNum]).Item1)
                    {
                        if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardNum]).Item2 == 'T')
                            this.BoardState.Main[this.BoardState.BoardCoord(boardNum).Item1, this.BoardState.BoardCoord(boardNum).Item2] = 'T';
                        else
                            this.BoardState.Main[this.BoardState.BoardCoord(boardNum).Item1, this.BoardState.BoardCoord(boardNum).Item2] = 'X';
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

                    // Get miniboard move, set it to 'X'
                    Tuple<short, short> miniBoardMove = this.BoardState.GetOpenMove(this.BoardState.MiniGames[boardSelected]);
                    this.BoardState.MiniGames[boardSelected][miniBoardMove.Item1, miniBoardMove.Item2] = 'X';

                    // If move made caused Main Board to change, set node.MainChanged to true
                    if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardSelected]).Item1)
                    {
                        if (this.BoardState.BoardComplete(this.BoardState.MiniGames[boardSelected]).Item2 == 'T')
                            this.BoardState.Main[this.BoardState.BoardCoord(boardSelected).Item1, this.BoardState.BoardCoord(boardSelected).Item2] = 'T';
                        else
                            this.BoardState.Main[this.BoardState.BoardCoord(boardSelected).Item1, this.BoardState.BoardCoord(boardSelected).Item2] = 'X';
                        node.MainChanged = true;
                    }
                    // Increment this.TreeDepth
                    this.TreeDepth++;

                    // set row/col in node equal to move made
                    node.Row = miniBoardMove.Item1;
                    node.Col = miniBoardMove.Item2;

                    // set node.Depth = this.depth
                    node.Depth = this.TreeDepth;

                    // set node.BoardNumberPlayedOn = to boardNum
                    node.BoardNumberPlayedOn = boardSelected;

                    // set node.BoardNumberToPlayOn = board opponent must play on next
                    node.BoardNumberToPlayOn = this.BoardState.MainBoardCoord(miniBoardMove.Item1, miniBoardMove.Item2);

                    // set node.Player = player
                    node.Player = player;
                }
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


        /// <summary>
        ///     Lowest level evaluation, assign value to one sequence of 3 spaces
        /// </summary>
        /// <param name="player"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        private short EvaluateLine(bool player, char[] line)
        {
            short result = 0;
            short xPlayer = 0;
            short oPlayer = 0;
            short blanks = 0;

            if (player == this.MaxPlayer)
            {
                for (short i = 0; i < 3; i++)
                {
                    if (line[i] == 'O')
                        oPlayer++;
                    else if (line[i] == 'B')
                        blanks++;
                }

                if (oPlayer + blanks == 3)
                {
                    if (oPlayer == 1)
                        result++;
                    else if (oPlayer == 2)
                        result += 10;
                }
            }
            else
            {
                for (short i = 0; i < 3; i++)
                {
                    if (line[i] == 'X')
                        xPlayer++;
                    else if (line[i] == 'B')
                        blanks++;
                }

                if (xPlayer + blanks == 3)
                {
                    if (xPlayer == 1)
                        result++;
                    else if (xPlayer == 2)
                        result += 10;
                }
            }

            return result;
        }


        /// <summary>
        ///     This is the low level static evaluation function
        /// </summary>
        /// <param name="board"></param>
        /// <param name="player"></param>
        /// <param name="isMain"></param>
        /// <returns></returns>
        private short EvaluateBoard(char[,] board, bool player, short multiplier)
        {
            short result = 0;
            // Evaluating Main board

            Tuple<bool, char> res = this.BoardState.BoardComplete(board);
            // If the game is complete
            if (res.Item1)
            {
                if (res.Item2 == 'T')
                    result = 0;
                else if (player == this.MaxPlayer && res.Item2 == 'O')
                    result += (short)(multiplier * 100);
                else if (!player == this.MaxPlayer && res.Item2 == 'X')
                    result += (short)(multiplier * 100);
                else
                    result -= (short)(multiplier * 100);
            }
            // If the game is still in progress
            else
            {
                // Evaluate rows
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[0, 0], board[0, 1], board[0, 2] }));
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[1, 0], board[1, 1], board[1, 2] }));
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[2, 0], board[2, 1], board[2, 2] }));

                // Evaluate columns
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[0, 0], board[1, 0], board[2, 0] }));
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[0, 1], board[1, 1], board[2, 1] }));
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[0, 2], board[1, 2], board[2, 2] }));

                // Evaluate diagonals
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[0, 0], board[1, 1], board[2, 2] }));
                result += (short)(multiplier * this.EvaluateLine(player, new char[] { board[0, 2], board[1, 1], board[2, 0] }));
            }


            return result;
        }


        /// <summary>
        ///     This is the high level static evaluation function.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private short Evaluate(bool player)
        {
            short result = 0;

            for (short index = 0; index < 10; index++)
            {
                if (index < 9)
                {
                    result += EvaluateBoard(this.BoardState.MiniGames[index], player, 1);
                    result -= EvaluateBoard(this.BoardState.MiniGames[index], !player, 1);
                }
                else
                {
                    result += EvaluateBoard(this.BoardState.Main, player, 10);
                    result -= EvaluateBoard(this.BoardState.Main, !player, 10);
                }
            }

            return result;
        }

        private short AB(Node node, short depth, short alpha, short beta, bool player)
        {
            if (depth == 0 || IsTerminal(player, node))
            {
                return Evaluate(player);
            }

            if (player == MaxPlayer)
            {
                while (this.TreeDepth < this.MaxTreeDepth)
                {
                    Node child = new Node(this.MakeMove(player, node.BoardNumberToPlayOn));
                    alpha = Math.Max(alpha, AB(child, depth--, alpha, beta, !player));

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
                    beta = Math.Min(beta, AB(child, depth--, alpha, beta, !player));

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