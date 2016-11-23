using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Board
    {
        public char[,] gameBoard;
        public char[,] boardStatus;

        public Board()
        {
            gameBoard = new char[9, 9];
            boardStatus = new char[9, 9];
            InitializeBoards();
        }

        private void InitializeBoards()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    gameBoard[row, col] = 'B';
                    boardStatus[row, col] = 'B';
                }
            }
        }
    }
}
