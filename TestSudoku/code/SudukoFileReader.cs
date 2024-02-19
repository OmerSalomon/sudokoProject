using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.Test.code
{
    internal class SudukoFileReader
    {
        public const string SUDOKOS_FILE_RELATIVE_PATH = @"sudoko_for_tests\sudokos.txt";
        public static List<string> ReadSudokosStringsFromFile()
        {

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            // Navigate up to the project root directory (adjust the number of ".." as needed)
            string projectRootPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string relativeFilePath = SUDOKOS_FILE_RELATIVE_PATH;
            string filePath = Path.Combine(projectRootPath, relativeFilePath);

            try
            {
                // List to hold each full Sudoku puzzle
                List<string> sudokuPuzzles = new List<string>();

                // Read each line of the file as a separate Sudoku puzzle
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string puzzle;
                    while ((puzzle = reader.ReadLine()) != null)
                    {
                        // Each line represents a full Sudoku puzzle
                        sudokuPuzzles.Add(puzzle);
                    }
                }

                return sudokuPuzzles;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while reading the file.");
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}
