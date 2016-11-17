using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Board
    {
        public char[,] TopLeftBoard;
        public char[,] TopMiddleBoard;
        public char[,] TopRightBoard;
        public char[,] LeftMiddleBoard;
        public char[,] MiddleBoard;
        public char[,] RightMiddleBoard;
        public char[,] BottomLeftBoard;
        public char[,] BottomMiddleBoard;
        public char[,] BottomRightBoard;

        public char[,] BoardStatus;

        public Board()
        {
            TopLeftBoard      = new char[9, 9];
            TopMiddleBoard    = new char[9, 9];
            TopRightBoard     = new char[9, 9];
            LeftMiddleBoard   = new char[9, 9];
            MiddleBoard       = new char[9, 9];
            RightMiddleBoard  = new char[9, 9];
            BottomLeftBoard   = new char[9, 9];
            BottomMiddleBoard = new char[9, 9];
            BottomRightBoard  = new char[9, 9];

            BoardStatus       = new char[9, 9];

            InitializeBoards();
        }

        private void InitializeBoards()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    TopLeftBoard[row, col]      = 'B';
                    TopMiddleBoard[row, col]    = 'B';
                    TopRightBoard[row, col]     = 'B';
                    LeftMiddleBoard[row, col]   = 'B';
                    MiddleBoard[row, col]       = 'B';
                    RightMiddleBoard[row, col]  = 'B';
                    BottomLeftBoard[row, col]   = 'B';
                    BottomMiddleBoard[row, col] = 'B';
                    BottomRightBoard[row, col]  = 'B';

                    BoardStatus[row, col]       = 'B';
                }
            }
        }
    }
}
