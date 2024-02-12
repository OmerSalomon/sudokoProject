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
    public static void Start()
    {
        string input = null;

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
            input = reader.Read();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        if (input != null)
        {
            Solver solver = new Solver();
            char[,] charBoard = Grider.ConvertStringToCharArr(input);
            try
            {
                char[,] solvedBoard = solver.Solve(charBoard);
                Console.WriteLine(Grider.ConvertGridToString(solvedBoard));

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    
}

    public static void Main(String[] args)
    {
        string input = "000000000000003085001020000000507000004000100090000000500000073002010000000040009";
        Solver solver = new Solver();
        char[,] charBoard = Grider.ConvertStringToCharArr(input);

        Console.WriteLine(Grider.ConvertGridToString(solver.Solve(charBoard)));
    }
    
        

    

}

// This code has been contributed by 29AjayKumar
