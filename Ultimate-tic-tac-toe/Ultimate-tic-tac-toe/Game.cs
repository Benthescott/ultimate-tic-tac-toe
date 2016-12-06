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

        /// <summary>
        /// Processes a move and returns a tuple of 3. Bool for valid move,
        ///     char #1 for whose move or (mini or main) game state,
        ///     char #2 for winner, tie, or just regular move
        /// </summary>
        /// <param name="boardNum"></param>
        /// <param name="moveRow"></param>
        /// <param name="moveCol"></param>
        /// <returns></returns>
        public Tuple<bool, char, char> MadeMove(short boardNum, short moveRow, short moveCol)
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

                UpdateBoard();

                Tuple<bool, char> moveResult = IsMiniGameOver(boardNum);

                if (moveResult.Item1)
                {
                    // Mini Game is complete
                    Tuple<short, short> boardCoords = board.BoardCoord(boardNumberPlayedOn);
                    board.Main[boardCoords.Item1, boardCoords.Item2] = moveResult.Item2;

                    Tuple<bool, char> gameState = IsGameOver();

                    if (gameState.Item1)
                        return new Tuple<bool, char, char>(true, 'G', gameState.Item2);     // Game over (item2 has winner/tie)
                    else
                    {
                        TurnManager();
                        return new Tuple<bool, char, char>(true, 'M', moveResult.Item2);    // Mini game is complete (item2 has winner/tie)
                    }
                }
                else if (xTurn)
                {
                    TurnManager();
                    return new Tuple<bool, char, char>(true, 'X', 'B');     // Valid move, no win/tie
                }
                else
                {
                    TurnManager();
                    return new Tuple<bool, char, char>(true, 'O', 'B');     // Valid move, no win/tie
                }
            }
            else
                return new Tuple<bool, char, char>(false, 'B', 'B');         // Not a valid move
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
                else if (boardNumberPlayedOn == boardNumberToPlayOn)    // Check if desired move is located in the required mini game
                    return true;        // Valid move

            return false;       // Not a valid move
        }

        private void UpdateBoard()
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

        public Tuple<bool, char> IsGameOver()
        {
            return board.BoardComplete(board.Main);
        }

        private Tuple<bool, char> IsMiniGameOver(short boardNum)
        {
            return board.BoardComplete(board.MiniGames[boardNum]);
        }

        private void TurnManager()
        {
            if (xTurn)
                xTurn = false;
            else
                xTurn = true;
        }
    }
}