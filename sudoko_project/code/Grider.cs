﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    internal class Grider
    {
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

        public static string ConvertGridToString(char[,] grid)
        {
            StringBuilder sb = new StringBuilder();
            int rows = grid.GetLength(0); // Get the number of rows in the grid
            int cols = grid.GetLength(1); // Get the number of columns in the grid

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    sb.Append(grid[i, j]); // Append the current character
                    if (j < cols - 1)
                    {
                        sb.Append(" "); // Add a space between characters (optional)
                    }
                }

                
                    

                if (i < rows - 1)
                {
                    sb.AppendLine(); // Add a newline character after each row except the last
                }
                
            }

            return sb.ToString();
        }
    }
}
