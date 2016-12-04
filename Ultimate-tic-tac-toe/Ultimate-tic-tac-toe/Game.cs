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
        private Move nextMiniGame;
        private int miniGameRowNum;
        private int miniGameColNum;
        private bool xTurn;
        private bool isMoveUnlimited;

        public Game()
        {
            xTurn = true;           // Player will always be X and go first
            isMoveUnlimited = true; // Player may choose any move at the beginning of the game
            board = new Board();
            aMove = new Move();
            nextMiniGame = new Move(0,0);
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
                StoreNextGameMove(location);

                if (board.boardStatus[nextMiniGame.row, nextMiniGame.col] == 'B')
                    isMoveUnlimited = false;
                else
                    isMoveUnlimited = true;

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
            if (board.boardStatus[miniGameRowNum, miniGameColNum] != 'B')
                return false;

            // If the player may move anywhere and move is available; then move is valid
            if (isMoveUnlimited && board.gameBoard[aMove.row, aMove.col] == 'B')
                return true;        // Valid move

            // Check if desired move is located in the required mini game and if move is available
            if (aMove.row == nextMiniGame.row - 1 || aMove.row == nextMiniGame.row + 1 || aMove.row == nextMiniGame.row)
                if (aMove.col == nextMiniGame.col - 1 || aMove.col == nextMiniGame.col + 1 || aMove.col == nextMiniGame.col)
                    if (board.gameBoard[aMove.row, aMove.col] == 'B')
                        return true;    // Valid move

            return false;       // Not a valid move
        }

        private void updateBoard()
        {
            if (xTurn)
                board.MiniGames[board.MainBoardCoord(aMove.row, aMove.col)][] = 'X';
            else
                board.MiniGames[board.MainBoardCoord(aMove.row, aMove.col)][] = 'O';

            if (board.BoardComplete(board.MiniGames[board.MainBoardCoord()]).Item1)
            {
                if (xTurn)
                    board.Main[]
                    board.boardStatus[miniGameRowNum, miniGameColNum] = 'X';
                else
                    board.boardStatus[miniGameRowNum, miniGameColNum] = 'O';
            }
            else if (isMiniGameTied())
                board.boardStatus[miniGameRowNum, miniGameColNum] = 'T';
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

        private void TranslateBtnName(string location)
        {
            String[] locationArray = location.Split('_');

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
        }

        private void StoreNextGameMove(string currentMoveLoc)
        {
            if (currentMoveLoc.Contains("_top_"))
                nextMiniGame.row = 1;
            else if (currentMoveLoc.Contains("_mid_"))
                nextMiniGame.row = 4;
            else
                nextMiniGame.row = 7;

            if (currentMoveLoc.EndsWith("_L_btn"))
                nextMiniGame.col = 1;
            else if (currentMoveLoc.EndsWith("_M_btn"))
                nextMiniGame.col = 4;
            else
                nextMiniGame.col = 7;
        }
    }
}