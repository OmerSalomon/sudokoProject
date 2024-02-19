using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.Test
{
    internal class SudukoFileReader
    {
        public const string SUDOKOS_FILE_RELATIVE_PATH = @"sudoko_for_tests\sudokos.txt";


        /// <summary>
        /// Reads Sudoku puzzles from a file, with each puzzle represented as a single line string.
        /// The method navigates from the current application domain's base directory up to the project's root directory,
        /// where it expects to find the Sudoku puzzles file. Each line in the file is treated as a separate Sudoku puzzle.
        /// </summary>
        /// <returns>
        /// A List of strings, where each string represents a full Sudoku puzzle.
        /// If an error occurs during file reading, the method prints the error message to the console and returns null.
        /// </returns>
        /// <remarks>
        /// The file path to the Sudoku puzzles is constructed dynamically using the project's root directory and a
        /// relative path defined by SUDOKOS_FILE_RELATIVE_PATH.
        /// Adjust the number of directory levels to navigate up ("..") based on the actual project structure.
        /// </remarks>
        /// <exception cref="System.IO.FileNotFoundException">Thrown if the Sudoku puzzles file is not found.</exception>
        /// <exception cref="System.Exception">General exception thrown for any other error encountered during file reading.</exception>
        public static List<string> ReadSudokosStringsFromFile(string relativePath)
        {

            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            // Navigate up to the project root directory (adjust the number of ".." as needed)
            string projectRootPath = Path.GetFullPath(Path.Combine(basePath, @"..\..\..\"));
            string filePath = Path.Combine(projectRootPath, relativePath);

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
