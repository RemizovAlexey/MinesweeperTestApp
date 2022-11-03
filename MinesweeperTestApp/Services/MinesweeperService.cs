using System;

namespace MinesweeperTestApp.Services
{
    public class MinesweeperService
    {
        public bool IsContinue { get; private set; } = true;
        public bool IsWinner { get; private set; }

        public char[,] GenerateBoardWithMines(int size, int minesCount)
        {
            var r = new Random();
            var board = new char[size, size];
            var mineCount = 0;

            for (var i = 0; i < size; i++) {
                for (var j = 0; j < size; j++) {
                    board[i,j] = 'E';
                }
            }

            while (mineCount < minesCount)
            {
                int x = 0, y = 0;
                do
                {
                    x = r.Next(size);
                    y = r.Next(size);
                }
                while (board[x, y] == 'M');

                mineCount++;
                board[x, y] = 'M';
            }

            return board;
        }

        public char[,] UpdateBoard(char[,] board, int[] click) {
            int m = board.GetLength(0), n = board.GetLength(1);
            int row = click[0], col = click[1];
        
            if (board[row, col] == 'M') // Mine
            { 
                board[row, col] = 'X';
                IsContinue = false;
            }
            else // Empty
            { 
                // Get number of mines first.
                var count = 0;
                for (var i = -1; i < 2; i++) 
                {
                    for (var j = -1; j < 2; j++) 
                    {
                        if (i == 0 && j == 0) continue;
                        int r = row + i, c = col + j;
                        if (r < 0 || r >= m || c < 0 || c < 0 || c >= n) continue;
                        if (board[r,c] == 'M' || board[r,c] == 'X') count++;
                    }
                }
            
                if (count > 0) // If it is not a 'B', stop further DFS.
                { 
                    board[row,col] = (char)(count + '0');
                }
                else // Continue DFS to adjacent cells.
                { 
                    board[row,col] = 'B';
                    for (int i = -1; i < 2; i++) {
                        for (int j = -1; j < 2; j++) {
                            if (i == 0 && j == 0) continue;
                            int r = row + i, c = col + j;
                            if (r < 0 || r >= m || c < 0 || c < 0 || c >= n) continue;
                            if (board[r,c] == 'E') UpdateBoard(board, new int[] {r, c});
                        }
                    }
                }
            }

            return board;
        }

        public void CheckWinCombination(char[,] board)
        {
            int m = board.GetLength(0), n = board.GetLength(1);
            var emptyFieldCount = 0;
            
            for (var i = 0; i < m; i++)
            {
                for (var j = 0; j < n; j++)
                {
                    if (board[i, j] == 'E')
                        emptyFieldCount++;
                }
            }

            if (emptyFieldCount == 0 && IsContinue)
                IsWinner = true;
        }
    }
}