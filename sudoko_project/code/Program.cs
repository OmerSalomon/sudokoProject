﻿/* A Backtracking program in 
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


class Program
{
    public const string OUTPUT_FILE_RELATIVE_PATH = @"..\..\output.txt";

    public static void Start()
    {
        string sudokoString = null;

        Reader reader = null;
        int choise = -1;
        Console.WriteLine("Choose: \n 1- Read from file \n 2- Read from CLI \n 3- Exit"); // Added an exit option

        while (!(choise > 0 && choise <= 3))
        {
            try
            {
                choise = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
            Solver solver = new Solver();
            try
            {
                Stopwatch stopWatch = Stopwatch.StartNew();

                string solvedSudokoString = solver.Solve(sudokoString);
                stopWatch.Stop();

                char[,] solvedBoard = Grider.ConvertStringToCharArr(solvedSudokoString);

                string solvedBoardString = Grider.ConvertGridToString(solvedBoard);

                File.WriteAllText(Program.OUTPUT_FILE_RELATIVE_PATH, solvedBoardString);

                Console.WriteLine(solvedBoardString);
                Console.WriteLine();
                Console.WriteLine($"Solved board written in {Path.GetFullPath(Program.OUTPUT_FILE_RELATIVE_PATH)}");
                Console.WriteLine();
                Console.WriteLine($"Solving time: {stopWatch.ElapsedMilliseconds} ms");
                Console.WriteLine();
                IOogwayable master = MasterOogway.GetInstance();
                Console.WriteLine($"\"{master.GetRandomSentence()}\" ~Master Oogway");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    public static void Main(String[] args)
    {
        Start();
    }
    
        

    

}

// This code has been contributed by 29AjayKumar
