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
        /// <returns></returns>
        public string PlayerMadeMove(string location)
        {
            TranslateBtnName(location);

            if (IsValidMove())
            {

            }
            else
            {

            }

            return "";
        }

        private bool IsValidMove()
        {
            if (currentBoard.BoardStatus)
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
                            case "L": aMove.gameNum = 0; break;
                            case "M": aMove.gameNum = 1; break;
                            case "R": aMove.gameNum = 2; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[1])
                        {
                            case "L": aMove.gameNum = 3; break;
                            case "M": aMove.gameNum = 4; break;
                            case "R": aMove.gameNum = 5; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[1])
                        {
                            case "L": aMove.gameNum = 6; break;
                            case "M": aMove.gameNum = 7; break;
                            case "R": aMove.gameNum = 8; break;
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
                            case "L": aMove.pos = 0; break;
                            case "M": aMove.pos = 1; break;
                            case "R": aMove.pos = 2; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[1])
                        {
                            case "L": aMove.pos = 3; break;
                            case "M": aMove.pos = 4; break;
                            case "R": aMove.pos = 5; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[1])
                        {
                            case "L": aMove.pos = 6; break;
                            case "M": aMove.pos = 7; break;
                            case "R": aMove.pos = 8; break;
                        }
                        break;
                    }
            }
            #endregion
        }
    }
}