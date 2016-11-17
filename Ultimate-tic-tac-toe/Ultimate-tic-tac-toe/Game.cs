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
        private Move playerMove;

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
            char status;
            int boardNum;

            switch(playerMove.gridMove)
            {

            }

            if (currentBoard.BoardStatus.)
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
                            case "L": playerMove.gridMove = GridLocation.GridPos.TopLeft; break;
                            case "M": playerMove.gridMove = GridLocation.GridPos.TopMiddle; break;
                            case "R": playerMove.gridMove = GridLocation.GridPos.TopRight; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[1])
                        {
                            case "L": playerMove.gridMove = GridLocation.GridPos.MiddleLeft; break;
                            case "M": playerMove.gridMove = GridLocation.GridPos.MiddleMiddle; break;
                            case "R": playerMove.gridMove = GridLocation.GridPos.MiddleRight; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[1])
                        {
                            case "L": playerMove.gridMove = GridLocation.GridPos.BottomLeft; break;
                            case "M": playerMove.gridMove = GridLocation.GridPos.BottomMiddle; break;
                            case "R": playerMove.gridMove = GridLocation.GridPos.BottomRight; break;
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
                            case "L": playerMove.miniGameMove = GridLocation.MiniGamePos.TopLeft; break;
                            case "M": playerMove.miniGameMove = GridLocation.MiniGamePos.TopMiddle; break;
                            case "R": playerMove.miniGameMove = GridLocation.MiniGamePos.TopRight; break;
                        }
                        break;
                    }
                case "mid":
                    {
                        switch (locationArray[1])
                        {
                            case "L": playerMove.miniGameMove = GridLocation.MiniGamePos.MiddleLeft; break;
                            case "M": playerMove.miniGameMove = GridLocation.MiniGamePos.MiddleMiddle; break;
                            case "R": playerMove.miniGameMove = GridLocation.MiniGamePos.MiddleRight; break;
                        }
                        break;
                    }
                case "bot":
                    {
                        switch (locationArray[1])
                        {
                            case "L": playerMove.miniGameMove = GridLocation.MiniGamePos.BottomLeft; break;
                            case "M": playerMove.miniGameMove = GridLocation.MiniGamePos.BottomMiddle; break;
                            case "R": playerMove.miniGameMove = GridLocation.MiniGamePos.BottomRight; break;
                        }
                        break;
                    }
            }
            #endregion
        }
    }
}