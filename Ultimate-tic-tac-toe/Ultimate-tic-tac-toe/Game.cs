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
        private bool isPlayerX;
        private bool xTurn;
        private Move aMove;
        private int miniGameRowNum;
        private int miniGameColNum;

        public Game()
        {
            
        }

        /// <summary>
        /// If a win was detected, this function determines if X was the winner
        /// </summary>
        /// <returns></returns>
        public bool xWon()
        {
            int gameRow = miniGameRowNum;
            int gameCol = miniGameColNum;

            // Check for a horizontal win
            for (int i = 0; i < 3; i++)
            {
                if (board.gameBoard[gameRow, gameCol - 1] == 'X' && board.gameBoard[gameRow, gameCol] == 'X'
                    && board.gameBoard[gameRow, gameCol + 1] == 'X')
                    return true;                                            // X won horizontally

                gameRow++;
            }

            // Reset number
            gameRow = miniGameRowNum;

            // Check for a vertical win
            for (int i = 0; i < 3; i++)
            {
                if (board.gameBoard[gameRow, gameCol - 1] == 'X' && board.gameBoard[gameRow, gameCol] == 'X'
                    && board.gameBoard[gameRow, gameCol + 1] == 'X')
                    return true;                                            // X won vertically

                gameCol++;
            }

            // Reset number
            gameCol = miniGameColNum;

            // Check for a diagonal win
            if (board.gameBoard[gameRow, gameCol] == 'X' && ((board.gameBoard[gameRow + 1, gameCol - 1] == 'X'
                && board.gameBoard[gameRow - 1, gameCol + 1] == 'X') || (board.gameBoard[gameRow - 1, gameCol - 1] == 'X'
                    && board.gameBoard[gameRow + 1, gameCol + 1] == 'X')))
                return true;                                                // X won diagonally

            return false;
        }

        public bool isMiniGameWon()
        {
            int gameRow = miniGameRowNum;
            int gameCol = miniGameColNum;

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

            // Reset number
            gameRow = miniGameRowNum;

            // Check for a vertical win
            for (int i = 0; i < 3; i++)
            {
                if (board.gameBoard[gameRow, gameCol - 1] == 'X' && board.gameBoard[gameRow, gameCol] == 'X'
                    && board.gameBoard[gameRow, gameCol + 1] == 'X')
                    return true;                                            // X won vertically
                else if (board.gameBoard[gameRow, gameCol - 1] == 'O' && board.gameBoard[gameRow, gameCol] == 'O'
                    && board.gameBoard[gameRow, gameCol + 1] == 'O')
                    return true;                                            // O won vertically

                gameCol++;
            }

            // Reset number
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
                    // Do stuff with model (update game board) and anything else necessary to
                    //   process the turn before returning to controller.
                    return 'X';
                }
                else
                {
                    // Do stuff with model (update game board) and anything else necessary to
                    //   process the turn before returning to controller.
                    return 'O';
                }
            }
            else
                return 'B';     //Not a valid move
        }

        private bool IsValidMove()
        {
            if (board.gameBoard[aMove.row, aMove.col] != 'B')
                return false;   //Not a valid move
            else
                return true;
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
                                aMove.row = 1;
                                aMove.col = 1; break;
                            case "M":
                                aMove.row = 1;
                                aMove.col = 4; break;
                            case "R":
                                aMove.row = 1;
                                aMove.col = 7; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[1])
                        {
                            case "L":
                                aMove.row = 4;
                                aMove.col = 1; break;
                            case "M":
                                aMove.row = 4;
                                aMove.col = 4; break;
                            case "R":
                                aMove.row = 4;
                                aMove.col = 7; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[1])
                        {
                            case "L":
                                aMove.row = 7;
                                aMove.col = 1; break;
                            case "M":
                                aMove.row = 7;
                                aMove.col = 4; break;
                            case "R":
                                aMove.row = 7;
                                aMove.col = 7; break;
                        }
                        break;
                    }
            }

            //Capture current minigame location
            miniGameRowNum = aMove.row;
            miniGameColNum = aMove.col;

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
                        switch (locationArray[1])
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
                        switch (locationArray[1])
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