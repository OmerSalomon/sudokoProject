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
using System.Diagnostics;


class Program
{
    public static char[,] getGridB()
    {
        string input = "10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<";

        int inputlength = input.Length;

        int dimension = (int)Math.Sqrt(inputlength);

        if (dimension * dimension != inputlength)
        {
            throw new Exception("The length of the string is not a perfect square.");
        }

        char[,] grid = new char[dimension, dimension];

        int stringIndex = 0;
        for (int i = 0; i < dimension; i++)
        {
            for (int j = 0; j < dimension; j++)
            {
                grid[i, j] = input[stringIndex++];
            }
        }

        return grid;
    }
    public static char[,] getGridA()
    {
        int[,] grid = new int[,] {
            { 3, 0, 6, 5, 0, 8, 4, 0, 0 },
            { 5, 2, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 8, 7, 0, 0, 0, 0, 3, 1 },
            { 0, 0, 3, 0, 1, 0, 0, 8, 0 },
            { 9, 0, 0, 8, 6, 3, 0, 0, 5 },
            { 0, 5, 0, 0, 9, 0, 6, 0, 0 },
            { 1, 3, 0, 0, 0, 0, 2, 5, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 7, 4 },
            { 0, 0, 5, 2, 0, 6, 3, 0, 0 }
        };

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
        Solver solver = new Solver();


        Stopwatch stopWatch = Stopwatch.StartNew();
        char[,] charBoard = solver.Solve(getGridB());
        stopWatch.Stop();

        Console.WriteLine(Grider.ConvertGridToString(charBoard));
        Console.WriteLine($"Elapsed Time is {stopWatch.ElapsedMilliseconds} ms");


    }
}

// This code has been contributed by 29AjayKumar
