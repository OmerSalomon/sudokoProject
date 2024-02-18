using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.code
{
    internal class BoardPrinter
    {
        private static void PrintHorizontalLine(int blockHeightAndWidth, string horizontalBlockSeparator, char symbol)
        {
            for (int i = 0; i < blockHeightAndWidth; i++)
                Console.Write(symbol + horizontalBlockSeparator);
            Console.Write(symbol);
            Console.WriteLine();
        }
        private static void PrintWithColor(char[,] board, int i, int j)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write((char)(board[i,j]));
            Console.ForegroundColor = originalColor;
        }


        public static void PrintBoard(char[,] board)
        {
            int boardSize = board.GetLength(0);

            int blockHeightAndWidth = (int)Math.Sqrt(boardSize);
            int temp = blockHeightAndWidth;

            if (Math.Sqrt(boardSize) != blockHeightAndWidth)
                temp++;

            int horizontalBlockSeparatorLength = blockHeightAndWidth * 2 + 1;
            string horizontalBlockSeparator = new string('-', horizontalBlockSeparatorLength);

            PrintHorizontalLine(temp, horizontalBlockSeparator, '+');

            for (int i = 0; i < boardSize; i++)
            {
                if (i != 0 && i % blockHeightAndWidth == 0)
                    PrintHorizontalLine(temp, horizontalBlockSeparator, '-');
                Console.Write("| ");
                for (int j = 0; j < boardSize; j++)
                {
                    if (j != 0 && j % blockHeightAndWidth == 0)
                        Console.Write("| ");
                    PrintWithColor(board, i, j);
                    Console.Write(" ");
                }
                Console.WriteLine("|");
            }
            PrintHorizontalLine(temp, horizontalBlockSeparator, '+');
        }
    }
}
