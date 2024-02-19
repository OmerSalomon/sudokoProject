using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    internal class TesterGrider
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
    }
}
