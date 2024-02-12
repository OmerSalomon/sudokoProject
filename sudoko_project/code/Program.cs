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
using sudoko_project.code;


class Program
{
    public static void Main(String[] args)
    {
        string input = null;

        while (input == null) // Changed to an infinite loop with a clear exit strategy
        {
            Reader reader = null;

            Console.WriteLine("Choose: \n 1- Read from file \n 2- Read from CLI \n 3- Exit"); // Added an exit option
            int.TryParse(Console.ReadLine(), out int choise);

            if (choise == 1)
            {
                reader = new TextFileReader();
            }
            else if (choise == 2)
            {
                reader = new CLIReader();
            }
            else if (choise == 3)
            {
                return;
            }
            else
            {
                Console.WriteLine($"{choise} is invalid number please enter a valid one");
            }

            try
            {
                input = reader.Read(); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        Solver solver = new Solver();
        char[,] charBoard = Grider.ConvertStringToCharArr(input);
        try
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            char[,] solvedBoard = solver.Solve(charBoard);
            stopwatch.Stop();

            Console.WriteLine(Grider.ConvertGridToString(solvedBoard));
            Console.WriteLine($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }
    }

    // Driver Code

}

// This code has been contributed by 29AjayKumar
