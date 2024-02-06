using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    internal class Algorithem
    {
        internal char[,] SolveSudoko(char[,] sudokoCharBoard)
        {
            Board board = new Board(sudokoCharBoard);

            board.RemoveImpossibleNumbers();

            solveBackTracking(board);

            return board.getCharBoard();
        }

        internal bool solveBackTracking(Board board)
        {
            int row = -1;
            int col = -1;
            bool isEmpty = true;
            int dimensionLen = board.getDimensionLen();

            for (int i = 0; i < dimensionLen && isEmpty; i++)
            {
                for (int j = 0; j < dimensionLen && isEmpty; j++)
                {
                    if (board.getCellNumber(i, j) == 0)
                    {
                        row = i;
                        col = j;

                        // We found an empty spot, so mark isEmpty as false to exit loops
                        isEmpty = false;
                    }
                }
            }

            // no empty space left
            if (isEmpty)
            {
                return true;
            }

            // else for each-row backtrack
            foreach (int num in board.getCellPossibleNumbers(row, col))
            {
                if (isSafe(board, row, col, num))
                {
                    board.getCellNumber(row, col);
                    if (solveBackTracking(board))
                    {

                        // Print(board, n);
                        return true;
                    }
                    else
                    {

                        // Replace it
                        board.setCellNumber(row, col, num);
                    }
                }
            }
            return false;
        }

        private bool isSafe(Board board, int row, int column, int num) 
        {
            int dimantionLen = board.getDimensionLen();

            for (int d = 0; d < dimantionLen; d++)
            {
                if (board.getCellNumber(row, column) == num)
                {
                    return false;
                }
            }

            for (int r = 0; r < dimantionLen; r++)
            {
                if (board.getCellNumber(row, column) == num)
                {
                    return false;
                }
            }

            int sqrt = (int)Math.Sqrt(dimantionLen);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int r = boxRowStart;
                r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                    d < boxColStart + sqrt; d++)
                {
                    if (board.getCellNumber(r, d) == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
