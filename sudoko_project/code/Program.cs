/* A Backtracking program in 
C# to solve Sudoku problem */
using System;
using System.Dynamic;
using System.Security.AccessControl;
using System.IO;
using sudoko_project;
using System.Collections;
using System.Xml.XPath;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.InteropServices;




class GFG
{


    public static bool isSafe(int[,] board, int row, int col, int num)
    {

        // Row has the unique (row-clash)
        for (int d = 0; d < board.GetLength(0); d++)
        {

            // Check if the number
            // we are trying to
            // place is already present in
            // that row, return false;
            if (board[row, d] == num)
            {
                return false;
            }
        }

        // Column has the unique numbers (column-clash)
        for (int r = 0; r < board.GetLength(0); r++)
        {

            // Check if the number 
            // we are trying to
            // place is already present in
            // that column, return false;
            if (board[r, col] == num)
            {
                return false;
            }
        }

        // corresponding square has
        // unique number (box-clash)
        int sqrt = (int)Math.Sqrt(board.GetLength(0));
        int boxRowStart = row - row % sqrt;
        int boxColStart = col - col % sqrt;

        for (int r = boxRowStart;
            r < boxRowStart + sqrt; r++)
        {
            for (int d = boxColStart;
                d < boxColStart + sqrt; d++)
            {
                if (board[r, d] == num)
                {
                    return false;
                }
            }
        }

        // if there is no clash, it's safe
        return true;
    }

    public static bool solveSudoku(int[,] board,
                                        int n)
    {
        int row = -1;
        int col = -1;
        bool isEmpty = true;

        for (int i = 0; i < n && isEmpty; i++)
        {
            for (int j = 0; j < n && isEmpty; j++)
            {
                if (board[i, j] == 0)
                {
                    row = i;
                    col = j;

                    // We found an empty spot, so mark isEmpty as false to exit loops
                    isEmpty = false;
                }
            }
        }

        // no empty space left
        if (isEmpty)
        {
            return true;
        }

        // else for each-row backtrack
        for (int num = 1; num <= n; num++)
        {
            if (isSafe(board, row, col, num))
            {
                board[row, col] = num;
                if (solveSudoku(board, n))
                {

                    // Print(board, n);
                    return true;
                }
                else
                {

                    // Replace it
                    board[row, col] = 0;
                }
            }
        }
        return false;
    }

    public static void print(int[,] board, int N)
    {

        // We got the answer, just print it
        for (int r = 0; r < N; r++)
        {
            for (int d = 0; d < N; d++)
            {
                int num = board[r, d];
                char ch = (char)((int)'0' + num);
                Console.Write(ch);
                Console.Write(" ");
            }
            Console.Write("\n");

            if ((r + 1) % (int)Math.Sqrt(N) == 0)
            {
                Console.Write("");
            }
        }
    }

    public static char[,] getGrid()
    {
        int[,] grid = { { 3, 0, 6, 5, 0, 8, 4, 0, 0 },
                { 5, 2, 0, 0, 0, 0, 0, 0, 0 },
                { 0, 8, 7, 0, 0, 0, 0, 3, 1 },
                { 0, 0, 3, 0, 1, 0, 0, 8, 0 },
                { 9, 0, 0, 8, 6, 3, 0, 0, 5 },
                { 0, 5, 0, 0, 9, 0, 6, 0, 0 },
                { 1, 3, 0, 0, 0, 0, 2, 5, 0 },
                { 0, 0, 0, 0, 0, 0, 0, 7, 4 },
                { 0, 0, 5, 2, 0, 6, 3, 0, 0 } };

        // Create a new char array of the same dimensions
        char[,] charGrid = new char[grid.GetLength(0), grid.GetLength(1)];

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                // Check if the value is 0, replace with a specific char, e.g., 'X'
                // Otherwise, convert the number to its char representation
                charGrid[i, j] = grid[i, j] == 0 ? '0' : (char)('0' + grid[i, j]);
            }
        }

        return charGrid;
    }

    // Driver Code
    public static void Main(String[] args)
    {
        Board board = new Board(getGrid());
        Console.WriteLine(board.ToString());
        Console.WriteLine(board.getBoardData());
    }
}

// This code has been contributed by 29AjayKumar
