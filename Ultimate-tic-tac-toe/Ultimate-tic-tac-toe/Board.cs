using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ultimate_tic_tac_toe
{
    class Board
    {
        public List<char[,]> MiniGames;

        public char[,] Main;

        public Board()
        {
            this.MiniGames = new List<char[,]>();
            this.Main = new char[3, 3];
            this.InitBoards();
        }

        public Board(Board b)
        {
            for (short x = 0; x < 9; x++)
                for (short y = 0; y < 3; y++)
                    for (short z = 0; z < 3; z++)
                    {
                        this.MiniGames[x][y, z] = b.MiniGames[x][y, z];
                        if (x == 8)
                        {
                            this.Main[x, y] = b.Main[x, y];
                        }
                    }
        }

        private void InitBoards()
        {
            for (short x = 0; x < 9; x++)
            {
                this.MiniGames.Add(new char[3, 3]);
                for (short y = 0; y < 3; y++)
                    for (short z = 0; z < 3; z++)
                    {
                        this.MiniGames[x][y, z] = 'B';
                        this.Main[y, z] = 'B';
                    }
            }
        }

        private char CheckRightDiaganolWin(char[,] board)
        {
            int xPlayer = 0;
            int oPlayer = 0;

            for (short row = 0; row < 3; row++)
                for (short col = 0; col < 3; col++)
                {
                    if (row == col && board[row, col] == 'X')
                        xPlayer++;
                    else if (row == col && board[row, col] == 'O')
                        oPlayer++;

                    if (xPlayer == 3)
                        return 'X';
                    else if (oPlayer == 3)
                        return 'O';
                }

            return 'N';
        }

        private char CheckLeftDiaganolWin(char[,] board)
        {
            int xPlayer = 0;
            int oPlayer = 0;

            for (short row = 0; row < 3; row++)
                for (short col = 0; col < 3; col++)
                {
                    if (row + col == 2 && board[row, col] == 'X')
                        xPlayer++;
                    else if (row + col == 2 && board[row, col] == 'O')
                        oPlayer++;

                    if (xPlayer == 3)
                        return 'X';
                    else if (oPlayer == 3)
                        return 'O';
                }

            return 'N';
        }

        private char CheckRowWin(char[,] board, int row)
        {
            int xPlayer = 0;
            int oPlayer = 0;

            for (short col = 0; col < 3; col++)
            {
                if (board[row, col] == 'X')
                    xPlayer++;
                else if (board[row, col] == 'O')
                    oPlayer++;

                if (xPlayer == 3)
                    return 'X';
                else if (oPlayer == 3)
                    return 'O';
                else if (row < 2)
                    return CheckRowWin(board, ++row);
            }

            return 'N';
        }

        private char CheckColWin(char[,] board, int col)
        {
            int xPlayer = 0;
            int oPlayer = 0;

            for (short row = 0; row < 3; row++)
            {
                if (board[row, col] == 'X')
                    xPlayer++;
                else if (board[row, col] == 'O')
                    oPlayer++;

                if (xPlayer == 3)
                    return 'X';
                else if (oPlayer == 3)
                    return 'O';
                else if (col < 2)
                    return CheckRowWin(board, ++col);
            }

            return 'N';
        }

        private Tuple<bool, char> CheckTie(char[,] board)
        {
            // If a mini game is still in progress then return false, 'N'
            for (int row = 0; row < 3; row++)
                for (int col = 0; col < 3; col++)
                    if (board[row, col] == 'B')
                        return new Tuple<bool, char>(false, 'N');

            return new Tuple<bool, char>(true, 'T');
        }

        /// <summary>
        /// Returns true/false if game is won and a char for who won
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        public Tuple<bool, char> BoardComplete(char[,] board)
        {
            List<char> results = new List<char>();

            results.Add(this.CheckRightDiaganolWin(board));
            results.Add(this.CheckLeftDiaganolWin(board));
            results.Add(this.CheckRowWin(board, 0));
            results.Add(this.CheckColWin(board, 0));

            if (results.Find(x => x == 'X') == 'X')
                return new Tuple<bool, char>(true, 'X');
            else if (results.Find(x => x == 'O') == 'O')
                return new Tuple<bool, char>(true, 'O');
            else
                return CheckTie(board);

        }

        /// <summary>
        /// Returns the x,y coordinate of a board
        /// </summary>
        /// <param name="boardNum">Board number (0-8)</param>
        /// <returns></returns>
        public Tuple<short, short> BoardCoord(int boardNum)
        {
            switch(boardNum)
            {
                case 0: return new Tuple<short, short>(0, 0);
                case 1: return new Tuple<short, short>(0, 1);
                case 2: return new Tuple<short, short>(0, 2);
                case 3: return new Tuple<short, short>(1, 0);
                case 4: return new Tuple<short, short>(1, 1);
                case 5: return new Tuple<short, short>(1, 2);
                case 6: return new Tuple<short, short>(2, 0);
                case 7: return new Tuple<short, short>(2, 1);
                default: return new Tuple<short, short>(2, 2);
            }
        }
    }
}
