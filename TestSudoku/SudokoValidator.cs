using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSudoku
{
    internal class SudokoValidator
    {
        internal bool IsSudokoValid(string sudokoSolution)
        {
            char[,] sudokoBoard = TesterGrider.ConvertStringToCharArr(sudokoSolution);
            int Len = sudokoBoard.GetLength(0);

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    if (!IsCellValid(sudokoBoard, row, column))
                        return false;

                }
            }

            return true;
        }

        private bool IsCellValid(char[,] sudokoBoard, int row, int column)
        {
            int Len = sudokoBoard.GetLength(0);
            int valueToCheck = sudokoBoard[row, column];

            for (int i = 0; i < Len; i++)
            {
                int cellValue = sudokoBoard[row, i];
                if (i != column && valueToCheck == cellValue)
                    return false;

            }

            for (int i = 0; i < Len; i++)
            {
                int cellValue = sudokoBoard[i, column];
                if (i != row && valueToCheck == cellValue)
                    return false;
            }

            int sqrt = (int)Math.Sqrt(Len);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int rowIteration = boxRowStart; rowIteration < boxRowStart + sqrt; rowIteration++)
            {
                for (int columnIteration = boxColStart; columnIteration < boxColStart + sqrt; columnIteration++)
                {
                    int cellValue = sudokoBoard[rowIteration, columnIteration];
                    if (rowIteration != row && columnIteration != column)
                        if (valueToCheck == cellValue)
                            return false;
                }
            }

            return true;
        }
    }
}
