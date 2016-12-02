﻿using System;
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

            int result = this.AB(new Node(this.TreeDepth, boardNum, this.MaxPlayer), depth, int.MinValue, int.MaxValue, MaxPlayer);

            return this.BoardState;
        }

        private Node MakeMove(bool player, short boardNum)
        {
            return new Node();
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
                return beta;
            }
            
        }
    }
}