using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.code
{
    abstract class Reader
    {
        public abstract string Read();

        /// <summary>
        /// Validates the format and content of a Sudoku puzzle represented as a string.
        /// </summary>
        /// <param name="sudokoString">The string representation of the Sudoku puzzle to be validated.</param>
        /// <exception cref="ReaderException">Thrown if the string does not represent a valid square size or if any character in the string is outside the valid range for Sudoku cell values.</exception>
        /// <remarks>
        /// This method performs two key validations on the input string:
        /// 1. It checks that the length of the string corresponds to a square number, ensuring the puzzle can be formed into a square grid (e.g., 9x9 for a standard Sudoku).
        /// 2. It verifies that each character in the string represents a valid value for a Sudoku cell, based on the size of the puzzle. Each character must be within the range 
        ///    of allowable values for a cell, including '0' for empty cells.
        /// 
        /// An exception is thrown if either validation fails, indicating the input string does not meet the requirements for a solvable Sudoku puzzle. The exception message
        /// provides details about the nature of the validation failure.
        /// </remarks>
        protected void ValidateString(string sudokoString)
        {
            double stringLenSquareRoot = Math.Sqrt(sudokoString.Length);
            if (stringLenSquareRoot != (int)stringLenSquareRoot)
                throw new ReaderException("String len is not a square root of a whole number");

            foreach (char c in sudokoString)
                if (c - '0' > stringLenSquareRoot)
                    throw new ReaderException($"{c} ascii value is not between {Convert.ToByte(c)} and {Convert.ToByte('0') + stringLenSquareRoot + 1}");
        }
    }

    class TextFileReader : Reader
    {
        /// <summary>
        /// Reads and validates the content of a Sudoku puzzle from a specified file path entered by the user.
        /// </summary>
        /// <returns>The content of the file as a string, assuming it passes validation for Sudoku puzzle format.</returns>
        /// <exception cref="ReaderException">Thrown if the content does not meet the Sudoku puzzle format requirements as validated by <see cref="ValidateString"/>.</exception>
        /// <exception cref="System.IO.FileNotFoundException">Thrown if the specified file path does not exist.</exception>
        /// <remarks>
        /// This method prompts the user to enter the file path of a Sudoku puzzle. It then attempts to read the file's content as a string.
        /// After reading, it validates the content to ensure it conforms to the expected format for a Sudoku puzzle (specifically, the string's length being a square number
        /// and each character representing a valid Sudoku cell value). If the content is invalid, a <see cref="ReaderException"/> is thrown detailing the validation failure.
        /// 
        /// Usage of this method requires the user to have read access to the specified file and for the file path to be entered correctly when prompted.
        /// </remarks>
        public override string Read()
        {
            string content;

            Console.WriteLine("Enter filePath");
            string filePath = Console.ReadLine();
            content = System.IO.File.ReadAllText(filePath);
            ValidateString(content);

            return content;
        }
    }

    class CLIReader : Reader
    {
        /// <summary>
        /// Reads a Sudoku puzzle string from the console input and validates it against Sudoku format requirements.
        /// </summary>
        /// <returns>The validated Sudoku puzzle string entered by the user.</returns>
        /// <exception cref="ReaderException">Thrown if the entered string does not conform to the expected Sudoku puzzle format, as validated by <see cref="ValidateString"/>.</exception>
        /// <remarks>
        /// This method prompts the user to enter a Sudoku puzzle string directly into the console. It validates the entered string to ensure it meets the format requirements for a Sudoku puzzle:
        /// - Each character in the string represents a valid cell value within the Sudoku puzzle (typically digits 0-9 for empty and filled cells, respectively).
        ///
        /// The method employs <see cref="ValidateString"/> to perform the validation. If the input string fails validation, a <see cref="ReaderException"/> is thrown, indicating the nature of the validation failure.
        /// This approach allows for quick testing or input of Sudoku puzzles without the need for file operations, streamlining the process for direct user interaction.
        /// </remarks>
        public override string Read()
        {
            Console.WriteLine("Enter String");
          
            string str = Console.ReadLine();
            ValidateString(str);

            return str;
        }
    }
}
