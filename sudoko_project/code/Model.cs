using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    internal class Cell
    {
        private int number;

        Dictionary<int, bool> possibleNumbers;

        internal Cell(int num, int size)
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

        internal Dictionary<int, bool> getNumberDict()
        {
            return possibleNumbers;
        }

        internal void setNumberImpossible(int number)
        {
            possibleNumbers[number] = false;
        }

        internal int getNumber()
        {
            return number;
        }

        internal void setNumber(int num)
        {
            this.number = num;
        }
    }

    internal class Board
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

        }

        internal int getCellNumber(int y, int x)
        {
            return cellsBoard[y, x].getNumber();
            
        }



        internal string getBoardData()
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

        internal void RemoveImpossibleNumbers()
        {
            int dimantionLen = cellsBoard.GetLength(0);

            for (int row = 0; row < dimantionLen; row++)
            {
                for (int column = 0; column < dimantionLen; column++)
                {
                    for (int xIteration = 0; xIteration < dimantionLen; xIteration++)
                    {
                        Cell xIterationCell = cellsBoard[row, xIteration];
                        int iterationNumber = xIterationCell.getNumber();
                        if (iterationNumber != 0)
                            cellsBoard[row, column].setNumberImpossible(iterationNumber);
                    }

                    for (int yIteration = 0; yIteration < dimantionLen; yIteration++)
                    {
                        Cell yIterationCell = cellsBoard[yIteration, column];
                        int iterationNumber = yIterationCell.getNumber();
                        if (iterationNumber != 0)
                            cellsBoard[row, column].setNumberImpossible(iterationNumber);
                    }

                    int sqrt = (int)Math.Sqrt(dimantionLen);
                    int boxRowStart = row - row % sqrt;
                    int boxColStart = column - column % sqrt;

                    for (int r = boxRowStart; r < boxRowStart + sqrt; r++)
                    {
                        for (int d = boxColStart; d < boxColStart + sqrt; d++)
                        {
                            Cell boxIterationCell = cellsBoard[r, d];
                            int iterationNumber = boxIterationCell.getNumber();
                            if (iterationNumber != 0)
                                cellsBoard[row, column].setNumberImpossible(iterationNumber);
                        }
                    }
                }
            }

            

        }

        internal int getDimensionLen()
        {
            return cellsBoard.GetLength(0);
        }

        internal void setCellNumber(int row, int column, int num)
        {
            cellsBoard[row, column].setNumber(num);
        }

        internal IEnumerable<int> getCellPossibleNumbers(int row, int column)
        {
            Dictionary<int, bool> numberDict = cellsBoard[row, column].getNumberDict();

            HashSet<int> possibleNumberSet = new HashSet<int>();
            

            foreach (int key in numberDict.Keys)
            {
                if (numberDict[key] == true)
                    possibleNumberSet.Add(key);
            }

            return possibleNumberSet;
        }

        internal char[,] getCharBoard()
        {
            int dimantionLen = cellsBoard.GetLength(0);
            char[,] charBoard = new char[dimantionLen, dimantionLen];


            for (int row = 0; row < dimantionLen; row++)
            {
                for (int column = 0; column < dimantionLen; column++)
                {
                    int number = cellsBoard[row, column].getNumber();
                    charBoard[row, column] = (char)('0' + number);
                }
            }

            return charBoard;
        }

        public override string ToString()
        {
            return StringEdit.ConvertGridToString(getCharBoard());
        }
    }
}
