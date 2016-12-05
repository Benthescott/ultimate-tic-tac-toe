using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Ultimate_tic_tac_toe
{
    class Game
    {
        private Board board;
        private Move aMove;
        private short boardNumberToPlayOn;
        private short boardNumberPlayedOn;
        private bool isMoveUnlimited;
        private bool xTurn;

        public Game()
        {
            xTurn = true;           // Player will always be X and go first
            isMoveUnlimited = true; // Player may choose any move at the beginning of the game
            board = new Board();
            aMove = new Move();
        }

        public char PlayerMadeMove(short boardNum, short moveRow, short moveCol)
        {
            boardNumberPlayedOn = boardNum;
            aMove = new Move(moveRow, moveCol);

            if (IsValidMove())
            {
                boardNumberToPlayOn = board.MainBoardCoord(moveRow, moveCol);

                if (board.BoardComplete(board.MiniGames[boardNumberToPlayOn]).Item1)
                    isMoveUnlimited = true;
                else
                    isMoveUnlimited = false;

                updateBoard();

                if (xTurn)
                {
                    xTurn = false;
                    return 'X';
                }
                else
                {
                    xTurn = true;
                    return 'O';
                }
            }
            else
                return 'B';     // Not a valid move
        }

        private bool IsValidMove()
        {
            // If mini game has been finished, do not allow move within mini game
            if (board.BoardComplete(board.MiniGames[boardNumberToPlayOn]).Item1)
                return false;

            // If the player may move anywhere and move is available; then move is valid
            if (board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] == 'B')
                if (isMoveUnlimited)
                    return true;        // Valid move
                // Check if desired move is located in the required mini game
                else if (boardNumberPlayedOn == boardNumberToPlayOn)
                    return true;        // Valid move

            return false;       // Not a valid move
        }

        private void updateBoard()
        {
            if (xTurn)
                board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] = 'X';
            else
                board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] = 'O';

            Tuple<bool, char> boardState = board.BoardComplete(board.MiniGames[boardNumberPlayedOn]);

            if (boardState.Item1)
            {
                Tuple<short, short> boardCoords = board.BoardCoord(boardNumberPlayedOn);
                board.Main[boardCoords.Item1, boardCoords.Item2] = boardState.Item2;
            }        
        }

        /// <summary>
        /// Determines if big game is over (either a win or tie).
        /// </summary>
        /// <returns>A char representing either a win (X or O), a TIE (T), or continue (B)</returns>
        public Tuple<bool, char> isGameOver()
        {
            return board.BoardComplete(board.Main);
        }

        /// <summary>
        /// Model helper function (private function). Searches game for empty moves.
        /// </summary>
        /// <returns></returns>
        private bool isMiniGameTied()
        {
            /* No win has been detected up to this point when this function is called.
                  Check if all moves in mini game have been used. If yes, then it is a TIE. */

            for (int row = miniGameRowNum - 1; row <= miniGameRowNum + 1; row++)
                for (int col = miniGameColNum - 1; col <= miniGameColNum + 1; col++)
                    if (board.gameBoard[row, col] == 'B')
                        return false;

            return true;
        }

        /// <summary>
        /// Only called by controller to determine if the current board is tied. Only checks boardStatus.
        /// </summary>
        /// <returns></returns>
        public bool isBoardTied()
        {
            if (board.boardStatus[miniGameRowNum, miniGameColNum] == 'T')
                return true;
            else
                return false;
        } 

        /// <summary>
        /// If a win was detected, this function determines if X was the winner
        /// </summary>
        /// <returns></returns>
        public bool xWon()
        {
            if (board.boardStatus[miniGameRowNum, miniGameColNum] == 'X')
                return true;
            else
                return false;
        }

        public bool isMiniGameWon()
        {
            if (board.BoardComplete(board.))
        }
    }
}