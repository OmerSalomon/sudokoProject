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
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;


class Program
{
    public const string OUTPUT_FILE_RELATIVE_PATH = @"..\..\output.txt";

    /// <summary>
    /// Starts the application process for solving a Sudoku puzzle.
    /// This method provides options to read the Sudoku puzzle from a file, from the CLI, or to exit the application.
    /// Upon receiving a valid Sudoku puzzle input, it solves the puzzle, measures the solving time,
    /// prints the solved board to the console, writes the solved board to a specified output file,
    /// and displays a wisdom quote from Master Oogway.
    /// </summary>
    public static void Start()
    {
        string sudokoString = null;

        Reader reader = null;
        int choise = -1;

        while (!(choise > 0 && choise <= 3))
        {
            Console.WriteLine("Choose: \n 1- Read from file \n 2- Read from CLI \n 3- Exit");

            try
            {
                choise = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            else {
                Console.WriteLine("number is invalid\n");
            }
        }

        

        try
        {
            sudokoString = reader.Read();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if (sudokoString != null)
        {
            Stopwatch stopWatch = Stopwatch.StartNew();
            Solver solver = new Solver();
            try
            {
                string solvedSudokoString = solver.Solve(sudokoString);
                stopWatch.Stop();
                char[,] solvedBoard = Grider.ConvertStringToCharArr(solvedSudokoString);

                string solvedBoardString = Grider.GetSudokuGridAsString(solvedBoard);

                File.WriteAllText(Program.OUTPUT_FILE_RELATIVE_PATH, solvedBoardString);

                BoardPrinter.PrintBoard(solvedBoard);
                Console.WriteLine();
                Console.WriteLine($"Solved board written in {Path.GetFullPath(Program.OUTPUT_FILE_RELATIVE_PATH)}");
                Console.WriteLine();
                Console.WriteLine($"Solving time: {stopWatch.ElapsedMilliseconds} ms");
                Console.WriteLine();
                IWiserable master = MasterOogway.GetInstance();
                Console.WriteLine($"\"{master.GetRandomSentence()}\" ~Master Oogway");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                stopWatch.Stop();
                Console.WriteLine($"Solving time: {stopWatch.ElapsedMilliseconds} ms");
            }
        }
    }

    public static void Main(String[] args)
    {
        Start();
    }
    
        

    

}

// This code has been contributed by 29AjayKumar
