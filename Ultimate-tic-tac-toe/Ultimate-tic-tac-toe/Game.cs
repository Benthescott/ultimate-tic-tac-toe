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
        private Board currentBoard;
        private bool isPlayerX;
        private bool xTurn;
        private Move aMove;

        public Game()
        {
            
        }

        /// <summary>
        /// Returns a string for the controller to know if X or O image should be placed
        /// on the button clicked on by the user.
        /// </summary>
        /// <param name="location">Entire button name</param>
        /// <returns>A capitalized X/O/B is returned for the controller to know how to respond to user click.</returns>
        public char PlayerMadeMove(string location)
        {
            TranslateBtnName(location);

            if (IsValidMove())
            {
                if (xTurn)
                {
                    // Do stuff with model (update game board) and anything else necessary
                    //   process the turn before returning to controller.
                    return 'X';
                }
                else
                {
                    // Do stuff with model (update game board) and anything else necessary
                    //   process the turn before returning to controller.
                    return 'O';
                }
            }
            else
            {
                return 'B';
            }
        }

        private bool IsValidMove()
        {
            if (currentBoard.BoardStatus[aMove.row, aMove.col] != 'B')
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