using System;
using MinesweeperTestApp.Extensions;
using MinesweeperTestApp.Services;

namespace MinesweeperTestApp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var minesweeperService = new MinesweeperService();
            var n = MinesweeperExtensions.InputValue("Hello player!!! Please input size of matrix:");
            var minesCount = MinesweeperExtensions.InputValue("Please input count of mines:");
            
            if (minesCount > n*n)
            {
                Console.WriteLine("Mines count cannot be bigger than size of matrix");
                Environment.Exit(-1);
            }

            // Generate Board with mines
            var board = minesweeperService.GenerateBoardWithMines(n, minesCount);
            MinesweeperExtensions.Print2DArray(board);
            
            var result = new char[n,n];
            while (minesweeperService.IsContinue && !minesweeperService.IsWinner)
            {
                var x = MinesweeperExtensions.InputValue("Input row position:");
                var y = MinesweeperExtensions.InputValue("Input column position:");

                if (x >= n)
                {
                    Console.WriteLine("Row position cannot be bigger than size of matrix");
                    Environment.Exit(-1);
                }
                
                if (y >= n)
                {
                    Console.WriteLine("Column position cannot be bigger than size of matrix");
                    Environment.Exit(-1);
                }

                // Update Board
                var click = new int[2] {x,y};
                result = minesweeperService.UpdateBoard(board, click);
                minesweeperService.CheckWinCombination(board);
                board = result;
                
                MinesweeperExtensions.Print2DArray(result);
            }

            Console.WriteLine(minesweeperService.IsWinner
                ? "Congrats! You are win!"
                : "Fin! You are lose( try one more time");
        }
    }
}

    