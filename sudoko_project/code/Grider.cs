using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    internal class Grider
    {
        /// <summary>
        /// Converts a linear string representation of a grid into a 2D character array.
        /// </summary>
        /// <param name="str">The string representation of the grid, where characters are arranged linearly.</param>
        /// <returns>A 2D character array representing the original grid structure.</returns>
        /// <exception cref="ArgumentException">Thrown if the length of the string is not a perfect square, indicating the string cannot form a square grid.</exception>
        /// <remarks>
        /// This method assumes that the input string represents a square grid, with the total number of characters being a perfect square (e.g., 9, 16, 25, etc.).
        /// It calculates the dimension of the grid by taking the square root of the string's length and then populates a 2D character array with characters
        /// from the string, row by row.
        ///
        /// This conversion is particularly useful for reconstructing 2D grid structures from their serialized or linearized forms, such as in the context of
        /// loading saved game states, parsing grid-based puzzle inputs, or any scenario where grid data is transmitted or stored in a linear format.
        /// </remarks>
        public static char[,] ConvertStringToCharArr(string str)
        {
            int dimension = (int)Math.Sqrt(str.Length);

            char[,] grid = new char[dimension, dimension];

            int stringIndex = 0;
            for (int i = 0; i < dimension; i++)
            {
                for (int j = 0; j < dimension; j++)
                {
                    grid[i, j] = str[stringIndex++];
                }
            }

            return grid;
        }

        /// <summary>
        /// Converts a 2D character array into a human-readable string representation.
        /// </summary>
        /// <param name="grid">The 2D character array representing the grid to be converted.</param>
        /// <returns>A string representation of the grid, where each row is separated by a new line and each character within a row is separated by a space.</returns>
        /// <remarks>
        /// This method iterates through each element of the provided 2D character array, appending each character to a <see cref="StringBuilder"/>.
        /// Characters in the same row are separated by a single space, and each row is terminated with a newline character, except for the last row.
        /// This formatting is intended to enhance readability and is particularly suited for displaying grid-based structures, such as puzzles or game boards, in a text-based interface.
        ///
        /// Example usage includes debugging output, logging, or displaying grid contents to the console or a text-based UI.
        /// </remarks>
        public static string ConvertGridToString(char[,] grid)
        {
            StringBuilder sb = new StringBuilder();
            int rows = grid.GetLength(0); 
            int cols = grid.GetLength(1); 

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(grid[i, j]); 
                    if (j < cols - 1)
                    {
                        sb.Append(" "); 
                    }
                }

                if (i < rows - 1)
                {
                    sb.AppendLine(); 
                }  
            }
            return sb.ToString();
        }
    }
}
