using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class AlphaBeta
    {
        private Board BoardState;
        private bool MaxPlayer = true;
        private short MaxTreeDepth;
        private List<Node> RootChildren;
        private Node root;

        public AlphaBeta()
        {
            BoardState = new Board();
        }

        public Node MakeAIMove(Node n, short depth, bool player)
        {
            root = new Node(MakeMove(player, new Tuple<short, short, short>(n.Row, n.Col, n.BoardNumberPlayedOn)));


            MaxTreeDepth = depth;
            RootChildren = new List<Node>();

            short result = AB(root, short.MinValue, short.MaxValue, player);



            Node selectedNode = new Node(BestMove());


            Debug.WriteLine("AI Move: " + selectedNode.BoardNumberPlayedOn + " " + selectedNode.Row + " " + selectedNode.Col);

            Node v = new Node(MakeMove(selectedNode.Player, new Tuple<short, short, short>(selectedNode.Row, selectedNode.Col, selectedNode.BoardNumberPlayedOn)));

            return v;
        }

        private Node BestMove()
        {
            short max = short.MinValue;
            Node selectedNode = new Node();
            foreach (Node child in RootChildren)
            {
                Node node = new Node(MakeMove(true, new Tuple<short, short, short>(child.Row, child.Col, child.BoardNumberPlayedOn)));
                if (BoardState.BoardComplete(BoardState.Main).Item1 && BoardState.BoardComplete(BoardState.Main).Item2 == 'O')
                {
                    UndoMove(node);
                    return node;
                }
                else if (BoardState.BoardComplete(BoardState.MiniGames[node.BoardNumberPlayedOn]).Item1 &&
                         BoardState.BoardComplete(BoardState.MiniGames[node.BoardNumberPlayedOn]).Item2 == 'O')
                {
                    UndoMove(node);
                    return node;
                }
                else if (EvaluateBoard(BoardState.MiniGames[node.BoardNumberPlayedOn], node.Player, 1) <
                         EvaluateBoard(BoardState.MiniGames[node.BoardNumberToPlayOn], !node.Player, 1) &&
                         !BoardState.BoardComplete(BoardState.MiniGames[node.BoardNumberPlayedOn]).Item1)
                {
                    UndoMove(node);
                    continue;
                }
                else
                {
                    UndoMove(node);
                    if (max < child.Value)
                    {
                        max = child.Value;
                        selectedNode = new Node(node);
                    }
                }
            }
            return selectedNode;
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
            char playerCharacter;
            // Set playerCharacter based on respective player
            if (player == MaxPlayer)
                playerCharacter = 'O';
            else
                playerCharacter = 'X';
            // Make any 'B' space on boardNum playerCharacter
            BoardState.MiniGames[move.Item3][move.Item1, move.Item2] = playerCharacter;
            // If move made caused Main Board to change, set node.MainChanged to true
            if (BoardState.BoardComplete(BoardState.MiniGames[move.Item3]).Item1)
            {
                if (BoardState.BoardComplete(BoardState.MiniGames[move.Item3]).Item2 == 'T')
                    BoardState.Main[BoardState.BoardCoord(move.Item3).Item1, BoardState.BoardCoord(move.Item3).Item2] = 'T';
                else
                    BoardState.Main[BoardState.BoardCoord(move.Item3).Item1, BoardState.BoardCoord(move.Item3).Item2] = playerCharacter;
                node.MainChanged = true;
            }
            // set row/col in node equal to move made
            node.Row = move.Item1;
            node.Col = move.Item2;
            // set node.BoardNumberPlayedOn = to boardNum
            node.BoardNumberPlayedOn = move.Item3;
            // set node.BoardNumberToPlayOn = board opponent must play on next
            node.BoardNumberToPlayOn = BoardState.MainBoardCoord(move.Item1, move.Item2);
            // set node.Player = player
            node.Player = player;
            return node;
        }

        private bool IsRoot(Node n)
        {
            bool result = false;

            if (n.BoardNumberPlayedOn == root.BoardNumberPlayedOn &&
                n.BoardNumberToPlayOn == root.BoardNumberToPlayOn &&
                n.Row == root.Row && n.Col == root.Col &&
                n.Player == root.Player && n.Depth == root.Depth)
            {
                result = true;
            }

            return result;
        }

        private void UndoMove(Node n)
        {
            if (!IsRoot(n))
            {
                BoardState.MiniGames[n.BoardNumberPlayedOn][n.Row, n.Col] = 'B';
                if (n.MainChanged)
                    BoardState.Main[BoardState.BoardCoord(n.BoardNumberPlayedOn).Item1,
                                    BoardState.BoardCoord(n.BoardNumberPlayedOn).Item2] = 'B';
            }
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
            short playerCharacterCount = 0;
            short blanks = 0;
            char playerCharacter;
            if (player == MaxPlayer)
                playerCharacter = 'O';
            else
                playerCharacter = 'X';
            for (short i = 0; i < 3; i++)
            {
                if (line[i] == playerCharacter)
                    playerCharacterCount++;
                else if (line[i] == 'B')
                    blanks++;
            }
            if (playerCharacterCount + blanks == 3)
            {
                if (playerCharacterCount == 1)
                    result += 10;
                else if (playerCharacterCount == 2)
                    result += 100;
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

            Tuple<bool, char> res = BoardState.BoardComplete(board);
            // If the game is complete
            if (res.Item1)
            {
                if (res.Item2 == 'T')
                    result = 0;
                else if (player == MaxPlayer && res.Item2 == 'O')
                    result += (short)(multiplier * 100);
                else if (player == !MaxPlayer && res.Item2 == 'X')
                    result += (short)(multiplier * 100);
            }
            // If the game is still in progress
            else
            {
                // Evaluate rows
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[0, 0], board[0, 1], board[0, 2] }));
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[1, 0], board[1, 1], board[1, 2] }));
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[2, 0], board[2, 1], board[2, 2] }));

                // Evaluate columns
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[0, 0], board[1, 0], board[2, 0] }));
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[0, 1], board[1, 1], board[2, 1] }));
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[0, 2], board[1, 2], board[2, 2] }));

                // Evaluate diagonals
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[0, 0], board[1, 1], board[2, 2] }));
                result += (short)(multiplier * EvaluateLine(player, new char[] { board[0, 2], board[1, 1], board[2, 0] }));
            }


            return result;
        }


        /// <summary>
        ///     This is the high level static evaluation function.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="node"></param>
        /// <returns></returns>
        private short Evaluate(bool player, Node current)
        {
            if (player == MaxPlayer)
            {
                Tuple<bool, char> res = BoardState.BoardComplete(BoardState.Main);
                if (res.Item1)
                    if (res.Item2 == 'T')
                        return 0;
                    else if (res.Item2 == 'O')
                        return 10000;
                    else
                        return -10000;
                else
                {
                    if (BoardState.BoardComplete(BoardState.MiniGames[current.BoardNumberToPlayOn]).Item1 && !BoardState.BoardComplete(BoardState.MiniGames[current.BoardNumberPlayedOn]).Item1)
                    {
                        return -5000;
                    }
                }
            }
            else
            {
                Tuple<bool, char> res = BoardState.BoardComplete(BoardState.Main);
                if (res.Item1)
                    if (res.Item2 == 'T')
                        return 0;
                    else if (res.Item2 == 'X')
                        return 10000;
                    else
                        return -10000;
                else
                {
                    if (BoardState.BoardComplete(BoardState.MiniGames[current.BoardNumberToPlayOn]).Item1 && !BoardState.BoardComplete(BoardState.MiniGames[current.BoardNumberPlayedOn]).Item1)
                    {
                        return -5000;
                    }
                }
            }


            short result = 0;

            for (short index = 0; index < 10; index++)
            {
                if (index < 9)
                {
                    result += EvaluateBoard(BoardState.MiniGames[index], player, 1);
                    result -= EvaluateBoard(BoardState.MiniGames[index], !player, 1);
                }
                else
                {
                    result += EvaluateBoard(BoardState.Main, player, 10);
                    result -= EvaluateBoard(BoardState.Main, !player, 10);
                }
            }

            return result;
        }

        private List<Node> Children(bool player, Node node)
        {
            List<Node> children = new List<Node>();

            char[,] board = new char[3, 3];
            board = BoardState.MiniGames[node.BoardNumberToPlayOn];

            List<Tuple<short, short, short>> moveList = new List<Tuple<short, short, short>>();

            moveList = BoardState.GetOpenMoves(board, node.BoardNumberToPlayOn);

            // Board complete
            if (BoardState.BoardComplete(board).Item1)
            {
                List<Tuple<short, short, short>> allMoves = new List<Tuple<short, short, short>>();
                for (short index = 0; index < 9; index++)
                {
                    if (BoardState.BoardComplete(BoardState.MiniGames[index]).Item1)
                        continue;

                    moveList = BoardState.GetOpenMoves(BoardState.MiniGames[index], index);
                    if (moveList.Count > 0)
                        allMoves.AddRange(moveList);

                }
                foreach (Tuple<short, short, short> move in allMoves)
                {
                    Node n = new Node(MakeMove(player, move));
                    n.Depth = (short)(node.Depth + 1);
                    UndoMove(n);
                    n.Player = !player;
                    children.Add(n);
                }
            }
            else
            {
                foreach (Tuple<short, short, short> move in moveList)
                {
                    Node n = new Node(MakeMove(player, move));
                    n.Depth = (short)(node.Depth + 1);
                    UndoMove(n);
                    n.Player = !player;
                    children.Add(n);
                }
            }

            return children;
        }


        /// <summary>
        /// 
        /// This function 
        /// 
        /// 
        /// </summary>
        /// <param name="node">
        /// 
        ///     The current node being evaluated.
        ///     
        /// </param>
        /// <param name="alpha">
        /// 
        ///     Current value of alpha
        ///     
        /// </param>
        /// <param name="beta">
        /// 
        ///     Current value of beta
        ///     
        /// </param>
        /// <param name="player">
        /// 
        /// Current player, Max or Min
        /// 
        /// </param>
        /// <returns></returns>
        private short AB(Node node, short alpha, short beta, bool player)
        {

            if (node.Depth >= MaxTreeDepth || IsTerminal(player, node))
            {
                node.Value = Evaluate(player, node);
                node.Value -= EvaluateBoard(BoardState.MiniGames[node.BoardNumberToPlayOn], !node.Player, 1);

                // Stack is unwinding, undo last move
                UndoMove(node);
                return node.Value;
            }

            if (player == MaxPlayer)
            {
                List<Node> children = Children(player, node);
                foreach (Node child in children)
                {
                    MakeMove(player, new Tuple<short, short, short>(child.Row, child.Col, child.BoardNumberPlayedOn));
                    alpha = Math.Max(alpha, AB(child, alpha, beta, !player));

                    if (alpha >= beta)
                    {
                        break;
                    }
                }

                node.Value = alpha;
                if (node.Depth == 1)
                    RootChildren.Add(node);

                // Stack is unwinding, undo last move
                UndoMove(node);
                return alpha;
            }
            else
            {
                List<Node> children = Children(player, node);
                foreach (Node child in children)
                {
                    MakeMove(player, new Tuple<short, short, short>(child.Row, child.Col, child.BoardNumberPlayedOn));
                    beta = Math.Min(beta, AB(child, alpha, beta, !player));

                    if (beta <= alpha)
                    {
                        break;
                    }
                }

                node.Value = beta;
                if (node.Depth == 1)
                    RootChildren.Add(node);

                // Stack is unwinding, undo last move
                UndoMove(node);
                return beta;
            }

        }
    }
}