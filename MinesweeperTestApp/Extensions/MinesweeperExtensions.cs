using System;

namespace MinesweeperTestApp.Extensions
{
    public static class MinesweeperExtensions
    {
        public static int InputValue(string message)
        {
            Console.WriteLine(message);
            var value = ReadInputPosition(Console.ReadKey());
            Console.WriteLine();

            return value;
        }
        
        public static void Print2DArray(char[,] matrix)
        {
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i,j] + "\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        
        private static int ReadInputPosition(ConsoleKeyInfo userInput)
        {
            int Bowl;
            
            if (char.IsDigit(userInput.KeyChar))
            {
                return Bowl = int.Parse(userInput.KeyChar.ToString()); // use Parse if it's a Digit
            }
            else
            {
                return Bowl = -1;  // Else we assign a default value
            }
        }
    }
}