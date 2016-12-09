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
        // Game Board
        private Board BoardState;

        // MaxPlayer
        private bool MaxPlayer = true;

        // From initial call
        private short MaxTreeDepth;

        // List containing children of root
        private List<Node> RootChildren;

        // Root node from Game.cs
        private Node root;


        /// <summary>
        /// 
        ///     Default contructor
        ///     
        /// </summary>
        public AlphaBeta()
        {
            BoardState = new Board();
        }

        /// <summary>
        /// 
        ///     Public method for initiating the AI making a move.
        ///     
        /// </summary>
        /// <param name="n">
        /// 
        ///     The move the X player (Min, Human) made, packaged up in a Node instance.    
        /// 
        /// </param>
        /// <param name="depth">
        /// 
        ///     The depth the AI should search in the game tree.
        /// 
        /// </param>
        /// <param name="player">
        /// 
        ///     The current player's turn, false for Min, true for Max.
        ///     X and O respectively.
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Returns the AI's chosen move, packaged up as a Node instance.
        /// 
        /// </returns>
        public Node MakeAIMove(Node n, short depth, bool player)
        {
            root = new Node(MakeMove(player, new Tuple<short, short, short>(n.Row, n.Col, n.BoardNumberPlayedOn)));
            MaxTreeDepth = depth;
            RootChildren = new List<Node>();
            short result = AB(root, short.MinValue, short.MaxValue, player);

            Node selectedNode = new Node(BestMove());

            // For debugging purposes only
            //Debug.WriteLine("AI Move: " + selectedNode.BoardNumberPlayedOn + " " + selectedNode.Row + " " + selectedNode.Col);

            Node v = new Node(MakeMove(selectedNode.Player, new Tuple<short, short, short>(selectedNode.Row, selectedNode.Col, selectedNode.BoardNumberPlayedOn)));

            return v;
        }


        /// <summary>
        /// 
        ///     Secondary evaluation function. Exists to keep the AI from making some game losing errors,
        ///     because the heuristic function is simplistic and doesn't account for many special cases.
        ///     If none of the special cases exist, the child with the highest heuristic score is chosen.
        /// 
        /// </summary>
        /// <returns>
        /// 
        ///     The best next move to choose based on all of children of the root, packaged up as a Node instance.
        /// 
        /// </returns>
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
        ///     This function applies one move to the game board, AlphaBeta.BoardState
        /// </summary>
        /// <param name="player">
        /// 
        ///     Max or Min player, X or O respectively
        /// 
        /// </param>
        /// <param name="boardNum">
        /// 
        ///     The number of the mini game to play on:
        ///     
        ///     0 | 1 | 2
        ///   --------------
        ///     3 | 4 | 5
        ///   --------------
        ///     6 | 7 | 8
        ///     
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Returns a new node for the tree.
        ///     
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



        /// <summary>
        ///     
        ///     Determines whether the node passed in is the root.
        /// 
        /// 
        /// </summary>
        /// <param name="n">
        /// 
        ///     Node to be evaluated.
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     true if parameter was the root, false otherwise.
        /// 
        /// </returns>
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


        /// <summary>
        /// 
        ///     This function undoes a specific move made to AlphaBeta.BoardState
        /// 
        /// </summary>
        /// <param name="n">
        /// 
        ///     Move to be undone packaged up as a Node instance.
        /// 
        /// </param>
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


        /// <summary>
        ///     
        ///     This function determines whether the given node is a leaf node.
        /// 
        /// </summary>
        /// <param name="node">
        /// 
        ///     The node being evaluated.
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     True or false, if a node is a leaf or not.
        /// 
        /// </returns>
        private bool IsTerminal(bool Player, Node node)
        {
            if (BoardState.BoardComplete(BoardState.Main).Item1)
                return true;
            else
                return false;
        }


        /// <summary>
        /// 
        ///     Lowest level evaluation, assign value to one sequence of 3 spaces on a single board.
        ///     
        /// </summary>
        /// <param name="player">
        /// 
        ///     Max or Min player, true or false respectively
        /// 
        /// </param>
        /// <param name="line">
        /// 
        ///     Line of 3 chars representing one line on one board.
        ///     
        ///     Either row, column, or diagonal.
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Heuristic value of that single line evaluated, based on the rules.
        /// 
        /// </returns>
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
        /// 
        ///     This is the mid-level static evaluation function. Evaluates a single
        ///     3x3 char array at a time, which represents a single mini-game or
        ///     overall game.
        ///     
        /// </summary>
        /// <param name="board">
        /// 
        ///     The 3x3 char array that represents one mini board or the main board.
        /// 
        /// </param>
        /// <param name="player">
        /// 
        ///     Max or Min player, true or false respectively
        /// 
        /// </param>
        /// <param name="multiplier">
        /// 
        ///     Multiplier for the heuristic. Values are either 1 or 10,
        ///     for if the board being evaluated is a mini-game or the overall game,
        ///     respectively. Thus, when evaluated a board state, the status of the overall
        ///     game will be weighted higher.
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Heuristic value of given 3x3 mini-game, based on the multiplier and the 
        ///     values received from the low level evaluation for each line of chars in the 3x3
        /// 
        /// </returns>
        private short EvaluateBoard(char[,] board, bool player, short multiplier)
        {
            short result = 0;

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
        ///     This is the high level static evaluation function. This function
        ///     loops through all mini-games and passes each one to the mid-level
        ///     evaluation function, adding up their individual heuristic scores.
        ///     
        /// </summary>
        /// <param name="player">
        /// 
        ///     Max or Min player, true or false respectively.
        /// 
        /// </param>
        /// <param name="node">
        /// 
        ///     The current move that was just made, packaged up as a Node
        ///     instance. Used for checking special case of the game being won
        ///     or lost, or if the move sent the opponent to a completed board
        ///     without winning their own board.
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     True heuristic value of the given board state. (AlphaBeta.BoardState)
        /// 
        /// </returns>
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


        /// <summary>
        ///     
        ///     This function returns all of the children (valid moves) of a given tree node.
        ///     It populates all properties on each node.
        /// 
        /// </summary>
        /// <param name="player">
        /// 
        ///     Max or Min player, true of false respectively
        /// 
        /// </param>
        /// <param name="node">
        /// 
        ///     Node in the game tree from which to generate valid children
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     List<Node> containing all valid child nodes of the given node
        /// 
        /// </returns>
        private List<Node> Children(bool player, Node node)
        {
            List<Node> children = new List<Node>();

            char[,] board = new char[3, 3];
            board = BoardState.MiniGames[node.BoardNumberToPlayOn];

            List<Tuple<short, short, short>> moveList = new List<Tuple<short, short, short>>();

            moveList = BoardState.GetOpenMoves(board, node.BoardNumberToPlayOn);

            // Board complete, gather all valid nodes from every open board
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
        ///     This function contains the Alpha Beta pruning algorithm.
        /// 
        /// </summary>
        /// <param name="node">
        /// 
        ///     The current node being evaluated. Begins with root.
        ///     
        /// </param>
        /// <param name="alpha">
        /// 
        ///     Current value of alpha, begins with short.Min
        ///     
        /// </param>
        /// <param name="beta">
        /// 
        ///     Current value of beta, begins with short.Max
        ///     
        /// </param>
        /// <param name="player">
        /// 
        /// Current player, Max or Min, true or false respectively
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