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
        private bool xTurn;
        private Move aMove;
        private int miniGameRowNum;
        private int miniGameColNum;

        public Game()
        {
            xTurn = true;       // Player will always be X and go first
            board = new Board();
            aMove = new Move();
        }

        private void updateBoard()
        {
            if (xTurn)
                board.gameBoard[aMove.row, aMove.col] = 'X';
            else
                board.gameBoard[aMove.row, aMove.col] = 'O';

            if (isMiniGameWon())
            {
                if (xTurn)
                    board.boardStatus[miniGameRowNum, miniGameColNum] = 'X';
                else
                    board.boardStatus[miniGameRowNum, miniGameColNum] = 'O';
            }
            else if (isMiniGameTied())
            {
                board.boardStatus[miniGameRowNum, miniGameColNum] = 'T';
            }
        }

        /// <summary>
        /// Model helper function (private function). Searches game for empty moves.
        /// </summary>
        /// <returns></returns>
        private bool isMiniGameTied()
        {
            /* No win has been detected up to this point when this function is called.
             *  Check if all moves in mini game have been used. If yes, then it is a TIE.
            */

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
            if (board.boardStatus[miniGameRowNum, miniGameColNum] == 'B')
            {
                int gameRow = miniGameRowNum;
                int gameCol = miniGameColNum;

                gameRow--;

                // Check for a horizontal win
                for (int i = 0; i < 3; i++)
                {
                    if (board.gameBoard[gameRow, gameCol - 1] == 'X' && board.gameBoard[gameRow, gameCol] == 'X'
                        && board.gameBoard[gameRow, gameCol + 1] == 'X')
                        return true;                                            // X won horizontally
                    else if (board.gameBoard[gameRow, gameCol - 1] == 'O' && board.gameBoard[gameRow, gameCol] == 'O'
                        && board.gameBoard[gameRow, gameCol + 1] == 'O')
                        return true;                                            // O won horizontally

                    gameRow++;
                }

                // Reset numbers
                gameRow = miniGameRowNum;
                gameCol--;

                // Check for a vertical win
                for (int i = 0; i < 3; i++)
                {
                    if (board.gameBoard[gameRow - 1, gameCol] == 'X' && board.gameBoard[gameRow, gameCol] == 'X'
                        && board.gameBoard[gameRow + 1, gameCol] == 'X')
                        return true;                                            // X won vertically
                    else if (board.gameBoard[gameRow - 1, gameCol] == 'O' && board.gameBoard[gameRow, gameCol] == 'O'
                        && board.gameBoard[gameRow + 1, gameCol] == 'O')
                        return true;                                            // O won vertically

                    gameCol++;
                }

                // Reset numbers
                gameCol = miniGameColNum;

                // Check for a diagonal win
                if (board.gameBoard[gameRow, gameCol] == 'X' && ((board.gameBoard[gameRow + 1, gameCol - 1] == 'X'
                    && board.gameBoard[gameRow - 1, gameCol + 1] == 'X') || (board.gameBoard[gameRow - 1, gameCol - 1] == 'X'
                        && board.gameBoard[gameRow + 1, gameCol + 1] == 'X')))
                    return true;                                                // X won diagonally
                else if (board.gameBoard[gameRow, gameCol] == 'O' && ((board.gameBoard[gameRow + 1, gameCol - 1] == 'O'
                    && board.gameBoard[gameRow - 1, gameCol + 1] == 'O') || (board.gameBoard[gameRow - 1, gameCol - 1] == 'O'
                        && board.gameBoard[gameRow + 1, gameCol + 1] == 'O')))
                    return true;                                                // O won diagonally

                // If reached, no win condition was detected
                return false;
            }
            else if (board.boardStatus[miniGameRowNum, miniGameColNum] == 'T')
                return false;
            else
                return true;
        }

        /// <summary>
        /// A capitalized X/O/B is returned for the controller to know if the board needs to be updated.
        /// on the button clicked on by the user.
        /// </summary>
        /// <param name="location">Entire button name</param>
        /// <returns>Char</returns>
        public char PlayerMadeMove(string location)
        {
            TranslateBtnName(location);

            if (IsValidMove())
            {
                if (xTurn)
                {
                    updateBoard();
                    // Do stuff with model (update game board) and anything else necessary to
                    //   process the turn before returning to controller.
                    xTurn = false;
                    return 'X';
                }
                else
                {
                    updateBoard();
                    // Do stuff with model (update game board) and anything else necessary to
                    //   process the turn before returning to controller.
                    xTurn = true;
                    return 'O';
                }
            }
            else
                return 'B';     // Not a valid move
        }

        private bool IsValidMove()
        {
            if (board.gameBoard[aMove.row, aMove.col] == 'B')
                return true;
            else
                return false;    //Not a valid move
        }

        private void TranslateBtnName(string location)
        {
            String[] locationArray = location.Split('_');

            #region Initializing playerMove by parsing button name
            //Parse out which mini game was played in
            switch (locationArray[0])
            {
                case "top":
                    {
                        switch (locationArray[1])
                        {
                            case "L":
                                miniGameRowNum = 1;
                                miniGameColNum = 1; break;
                            case "M":
                                miniGameRowNum = 1;
                                miniGameColNum = 4; break;
                            case "R":
                                miniGameRowNum = 1;
                                miniGameColNum = 7; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[1])
                        {
                            case "L":
                                miniGameRowNum = 4;
                                miniGameColNum = 1; break;
                            case "M":
                                miniGameRowNum = 4;
                                miniGameColNum = 4; break;
                            case "R":
                                miniGameRowNum = 4;
                                miniGameColNum = 7; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[1])
                        {
                            case "L":
                                miniGameRowNum = 7;
                                miniGameColNum = 1; break;
                            case "M":
                                miniGameRowNum = 7;
                                miniGameColNum = 4; break;
                            case "R":
                                miniGameRowNum = 7;
                                miniGameColNum = 7; break;
                        }
                        break;
                    }
            }

            aMove.row = miniGameRowNum;
            aMove.col = miniGameColNum;

            //Parse out which location in mini game was played
            switch (locationArray[2])
            {
                case "top":
                    {
                        switch (locationArray[3])
                        {
                            case "L":
                                aMove.row -= 1;
                                aMove.col -= 1; break;
                            case "M":
                                aMove.row -= 1; break;
                            case "R":
                                aMove.row -= 1;
                                aMove.col += 1; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[3])
                        {
                            case "L":
                                aMove.col -= 1; break;
                            case "R":
                                aMove.col += 1; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[3])
                        {
                            case "L":
                                aMove.row += 1;
                                aMove.col -= 1; break;
                            case "M":
                                aMove.row += 1; break;
                            case "R":
                                aMove.row += 1;
                                aMove.col += 1; break;
                        }
                        break;
                    }
            }
            #endregion
        }
    }
}