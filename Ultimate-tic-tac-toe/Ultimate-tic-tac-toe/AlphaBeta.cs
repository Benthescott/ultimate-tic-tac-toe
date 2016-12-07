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
        private List<Node> nodes;

        public AlphaBeta()
        {
            this.BoardState = new Board();
        }

        /// <summary>
        /// 
        ///     (From Game.cs)
        ///     Call: node = new Node(AI.MakeAIMove(node, 8, true));
        /// 
        /// </summary>
        /// <param name="n">
        ///     Node instance should contain:
        ///     {
        ///         this.BoardNumberPlayedOn = boardNum (The board the player used)
        ///         this.BoardNumberToPlayOn = (The board TO play on)
        ///         this.Row = (Row the player used)
        ///         this.Col = (Col the player used)
        ///     }
        /// </param>
        /// <param name="depth">
        /// 
        ///     Depth of each AI search: Just pass 4 or 8; 8 will take longer.    
        /// 
        /// </param>
        /// <param name="player">
        /// 
        ///     Always pass true
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Returns a Node instance containing all info needed to update the GUI
        ///     with the AI's move
        /// 
        /// </returns>
        public Node MakeAIMove(Node n, short depth, bool player)
        {
            this.MakeMove(!player, new Tuple<short, short, short>(n.Row, n.Col, n.BoardNumberPlayedOn));

            //BoardState.MiniGames[n.BoardNumberPlayedOn][n.Row, n.Col] = 'X';

            this.TreeDepth = 0;
            this.MaxTreeDepth = depth;
            nodes = new List<Node>();

            short result = this.AB(new Node(this.TreeDepth, n.BoardNumberToPlayOn, this.MaxPlayer), short.MinValue, short.MaxValue, player);

            short max = short.MinValue;
            Node selectedNode = new Node();
            for (short i = 0; i < nodes.Count; i++)
            {
                if (max < nodes[i].Value)
                {
                    max = nodes[i].Value;
                    selectedNode = new Node(nodes[i]);
                }
            }

            Console.WriteLine("AI Move: " + selectedNode.BoardNumberPlayedOn + " " + selectedNode.Row + " " + selectedNode.Col);

            Node v = new Node(this.MakeMove(player, new Tuple<short, short, short>(selectedNode.Row, selectedNode.Col, selectedNode.BoardNumberPlayedOn)));

            return v;
        }

        public void UpdateBoard(bool player, Tuple<short, short, short> move)
        {
            MakeMove(player, move);
        }


        /// <summary>
        ///     This function makes one valid move.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="boardNum"></param>
        /// <returns>
        ///     Returns a new node for the tree.
        /// </returns>
        private Node MakeMove(bool player, Tuple<short, short, short> move)
        {
            // Make new node
            Node node = new Node();

            // If 'O' player
            if (player == this.MaxPlayer)
            {

                // Make any 'B' space on boardNum 'O'
                this.BoardState.MiniGames[move.Item3][move.Item1, move.Item2] = 'O';

                // If move made caused Main Board to change, set node.MainChanged to true
                if (this.BoardState.BoardComplete(this.BoardState.MiniGames[move.Item3]).Item1)
                {
                    if (this.BoardState.BoardComplete(this.BoardState.MiniGames[move.Item3]).Item2 == 'T')
                        this.BoardState.Main[this.BoardState.BoardCoord(move.Item3).Item1, this.BoardState.BoardCoord(move.Item3).Item2] = 'T';
                    else
                        this.BoardState.Main[this.BoardState.BoardCoord(move.Item3).Item1, this.BoardState.BoardCoord(move.Item3).Item2] = 'O';
                    node.MainChanged = true;
                }



                // set row/col in node equal to move made
                node.Row = move.Item1;
                node.Col = move.Item2;



                // set node.BoardNumberPlayedOn = to boardNum
                node.BoardNumberPlayedOn = move.Item3;

                // set node.BoardNumberToPlayOn = board opponent must play on next
                node.BoardNumberToPlayOn = this.BoardState.MainBoardCoord(move.Item1, move.Item2);

                // set node.Player = player
                node.Player = player;
            }

            // 'X' player
            else
            {

                // Make 'B' space on boardNum 'X'
                this.BoardState.MiniGames[move.Item3][move.Item1, move.Item2] = 'X';

                // If move made caused Main Board to change, set node.MainChanged to true
                if (this.BoardState.BoardComplete(this.BoardState.MiniGames[move.Item3]).Item1)
                {
                    if (this.BoardState.BoardComplete(this.BoardState.MiniGames[move.Item3]).Item2 == 'T')
                        this.BoardState.Main[this.BoardState.BoardCoord(move.Item3).Item1, this.BoardState.BoardCoord(move.Item3).Item2] = 'T';
                    else
                        this.BoardState.Main[this.BoardState.BoardCoord(move.Item3).Item1, this.BoardState.BoardCoord(move.Item3).Item2] = 'X';
                    node.MainChanged = true;
                }


                // set row/col in node equal to move made
                node.Row = move.Item1;
                node.Col = move.Item2;



                // set node.BoardNumberPlayedOn = to boardNum
                node.BoardNumberPlayedOn = move.Item3;

                // set node.BoardNumberToPlayOn = board opponent must play on next
                node.BoardNumberToPlayOn = this.BoardState.MainBoardCoord(move.Item1, move.Item2);

                // set node.Player = player
                node.Player = player;
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

            if (BoardState.BoardComplete(BoardState.Main).Item1)
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
                    else if (oPlayer == 3)
                        result += 20;
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
                    else if (xPlayer == 3)
                        result += 20;
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
            if (player == MaxPlayer)
            {
                Tuple<bool, char> res = BoardState.BoardComplete(BoardState.Main);
                if (res.Item1)
                    if (res.Item2 == 'T')
                        return 0;
                    else if (res.Item2 == 'O')
                        return short.MaxValue;
                    else
                        return short.MinValue;
            }
            else
            {
                Tuple<bool, char> res = BoardState.BoardComplete(BoardState.Main);
                if (res.Item1)
                    if (res.Item2 == 'T')
                        return 0;
                    else if (res.Item2 == 'X')
                        return short.MaxValue;
                    else
                        return short.MinValue;
            }


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


        private List<Node> Children(bool player, Node node)
        {
            List<Node> children = new List<Node>();

            char[,] board = new char[3, 3];
            board = this.BoardState.MiniGames[node.BoardNumberToPlayOn];

            List<Tuple<short, short, short>> moveList = new List<Tuple<short, short, short>>();

            moveList = this.BoardState.GetOpenMoves(board, node.BoardNumberToPlayOn);

            // Board complete
            if (BoardState.BoardComplete(board).Item1)
            {
                List<Tuple<short, short, short>> allMoves = new List<Tuple<short, short, short>>();
                for (short index = 0; index < 9; index++)
                {
                    if (BoardState.BoardComplete(this.BoardState.MiniGames[index]).Item1)
                        continue;

                    moveList = this.BoardState.GetOpenMoves(this.BoardState.MiniGames[index], index);
                    if (moveList.Count > 0)
                        allMoves.AddRange(moveList);

                }
                foreach (Tuple<short, short, short> move in allMoves)
                {
                    Node n = new Node(this.MakeMove(player, move));
                    n.Depth = Convert.ToInt16(node.Depth + 1);
                    this.UndoMove(n);
                    n.Player = !player;
                    children.Add(n);
                }
            }
            else
            {
                foreach (Tuple<short, short, short> move in moveList)
                {
                    Node n = new Node(this.MakeMove(player, move));
                    n.Depth = Convert.ToInt16(node.Depth + 1);
                    this.UndoMove(n);
                    n.Player = !player;
                    children.Add(n);
                }
            }

            return children;
        }

        private short AB(Node node, short alpha, short beta, bool player)
        {

            if (node.Depth >= this.MaxTreeDepth || IsTerminal(player, node))
            {
                node.Value = this.Evaluate(player);
                this.UndoMove(node);
                return node.Value;
            }

            if (player == MaxPlayer)
            {
                List<Node> children = this.Children(player, node);
                foreach (Node child in children)
                {
                    this.MakeMove(player, new Tuple<short, short, short>(child.Row, child.Col, child.BoardNumberPlayedOn));
                    alpha = Math.Max(alpha, AB(child, alpha, beta, !player));

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                node.Value = alpha;
                if (node.Depth == 1)
                    this.nodes.Add(node);

                // Stack is unwinding, undo last move
                this.UndoMove(node);
                return alpha;
            }
            else
            {
                List<Node> children = this.Children(player, node);
                foreach (Node child in children)
                {
                    this.MakeMove(player, new Tuple<short, short, short>(child.Row, child.Col, child.BoardNumberPlayedOn));
                    beta = Math.Min(beta, AB(child, alpha, beta, !player));

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                node.Value = beta;
                if (node.Depth == 1)
                    this.nodes.Add(node);

                // Stack is unwinding, undo last move
                this.UndoMove(node);
                return beta;
            }

        }
    }
}