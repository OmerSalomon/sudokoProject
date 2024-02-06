using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    class Cell
    {
        private int number;

        Dictionary<int, bool> possibleNumbers;

        public Cell(int num, int size)
        {
            possibleNumbers = new Dictionary<int, bool>(0);

            if (num == 0)
            {
                for (int i = 1; i <= size; i++)
                {
                    possibleNumbers[i] = true;
                }
            }

            this.number = num;
        }

        public Dictionary<int, bool> getNumberDict()
        {
            return possibleNumbers;
        }

        public void setNumberImpossible(int number)
        {
            possibleNumbers[number] = false;
        }

        internal int getNumber()
        {
            return number;
        }
    }

    class Board
    {
        Cell[,] cellsBoard;

        public Board(char[,] charBoard)
        {
            if (charBoard.GetLength(0) != charBoard.GetLength(1))
                throw new GridException("Board dimantion size are not equal");

            int dimantionLen = charBoard.GetLength(0);
            cellsBoard = new Cell[dimantionLen, dimantionLen];

            for (int y = 0; y < dimantionLen; y++)
                for (int x = 0; x < dimantionLen; x++)
                    cellsBoard[y, x] = new Cell(charBoard[x, y] - '0', dimantionLen);

            RemoveImpossibleNumbers();
        }

        public string getBoardData()
        {
            int dimantionLen = cellsBoard.GetLength(0);
            string boardData = "";
            for (int y = 0; y < dimantionLen; y++)
            {
                for (int x = 0; x < dimantionLen; x++)
                {
                    string possibleNumbersString = "";
                    Cell cell = cellsBoard[y, x];

                    foreach (int number in cell.getNumberDict().Keys)
                    {
                        if (cell.getNumberDict()[number] == true)
                            possibleNumbersString += number.ToString() + ", ";
                    }

                    int cellNumber = cellsBoard[y, x].getNumber();
                    boardData += "[" + x + ", " + y + "]: " + " |" + cellNumber + "| " + possibleNumbersString + "\n";

                }
            }

            return boardData;
        }

        public override string ToString()
        {
            string res = "";
            int dimantionLen = cellsBoard.GetLength(0);

            for (int y = 0; y < dimantionLen; y++)
            {
                string row = "";
                for (int x = 0; x < dimantionLen; x++)
                {
                    int number = cellsBoard[y, x].getNumber();
                    row += (char)('0' + number) + " ";
                }
                row += '\n';
                res += row;
            }

            return res;
        }

        private bool isSafe(int y, int x, int num)
        {
            int dimantionLen = cellsBoard.GetLength(0);

            for (int d = 0; d < dimantionLen; d++)
            {
                if (cellsBoard[y, d].getNumber() == num)
                {
                    return false;
                }
            }

            for (int r = 0; r < dimantionLen; r++)
            {
                if (cellsBoard[r, x].getNumber() == num)
                {
                    return false;
                }
            }

            int sqrt = (int)Math.Sqrt(dimantionLen);
            int boxRowStart = y - y % sqrt;
            int boxColStart = x - x % sqrt;

            for (int r = boxRowStart;
                r < boxRowStart + sqrt; r++)
            {
                for (int d = boxColStart;
                    d < boxColStart + sqrt; d++)
                {
                    if (cellsBoard[r, d].getNumber() == num)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void RemoveImpossibleNumbers()
        {
            int dimantionLen = cellsBoard.GetLength(0);

            for (int y = 0; y < dimantionLen; y++)
            {
                for (int x = 0; x < dimantionLen; x++)
                {
                    for (int xIteration = 0; xIteration < dimantionLen; xIteration++)
                    {
                        Cell xIterationCell = cellsBoard[y, xIteration];
                        int iterationNumber = xIterationCell.getNumber();
                        if (iterationNumber != 0)
                            cellsBoard[y, x].setNumberImpossible(iterationNumber);
                    }

                    for (int yIteration = 0; yIteration < dimantionLen; yIteration++)
                    {
                        Cell yIterationCell = cellsBoard[yIteration, x];
                        int iterationNumber = yIterationCell.getNumber();
                        if (iterationNumber != 0)
                            cellsBoard[y, x].setNumberImpossible(iterationNumber);
                    }

                    int sqrt = (int)Math.Sqrt(dimantionLen);
                    int boxRowStart = y - y % sqrt;
                    int boxColStart = x - x % sqrt;

                    for (int r = boxRowStart; r < boxRowStart + sqrt; r++)
                    {
                        for (int d = boxColStart; d < boxColStart + sqrt; d++)
                        {
                            Cell boxIterationCell = cellsBoard[r, d];
                            int iterationNumber = boxIterationCell.getNumber();
                            if (iterationNumber != 0)
                                cellsBoard[y, x].setNumberImpossible(iterationNumber);
                        }
                    }
                }
            }

        }

    }
}
