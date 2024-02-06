using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace sudoko_project
{
    internal class Cell
    {
        private byte value;
        private HashSet<byte> markers;
        internal Cell(byte num, byte markerAmount)
        {
            markers = new HashSet<byte>();

            if (num == 0)
            {
                for (byte i = 1; i <= markerAmount; i++)
                {
                    markers.Add(i);
                }
            }

            this.value = num;
        }

        internal HashSet<byte> GetMarkers()
        {
            return markers;
        }

        internal void EditMarker(byte number, bool operation)
        {
            if (operation)
                markers.Remove(number);
            else 
                markers.Add(number);
        }


        internal byte GetValue()
        {
            return value;
        }

        internal void SetNumber(byte num)
        {
            this.value = num;
        }
    }

    internal class Board
    {
        private Cell[,] cellsBoard;
        private byte dimensionLen;

        public Board(char[,] charBoard)
        {
            if (charBoard.GetLength(0) != charBoard.GetLength(1))
                throw new Exception("Board dimension size are not equal"); // Use specific exception as needed.

            dimensionLen = (byte)charBoard.GetLength(0);
            cellsBoard = new Cell[dimensionLen, dimensionLen];

            for (byte y = 0; y < dimensionLen; y++)
            {
                for (byte x = 0; x < dimensionLen; x++)
                {
                    byte cellValue = (byte)(charBoard[y, x] - '0');
                    cellsBoard[y, x] = new Cell(cellValue, dimensionLen);
                }
            }
        }

        public Board(byte dimensionLen)
        {
            this.dimensionLen = dimensionLen;
            cellsBoard = new Cell[dimensionLen, dimensionLen];

            for (byte y = 0; y < dimensionLen; y++)
            {
                for (byte x = 0; x < dimensionLen; x++)
                {
                    cellsBoard[y, x] = new Cell(0, dimensionLen); // Initialize all cells with 0 and possible markers
                }
            }
        }

        internal string GetBoardData()
        {
            StringBuilder boardData = new StringBuilder();
            for (byte y = 0; y < dimensionLen; y++)
            {
                for (byte x = 0; x < dimensionLen; x++)
                {
                    string possibleNumbersString = string.Join(", ", cellsBoard[y, x].GetMarkers());
                    byte cellNumber = cellsBoard[y, x].GetValue();
                    boardData.AppendLine($"[{x}, {y}]: |{cellNumber}| {possibleNumbersString}");
                }
            }
            return boardData.ToString();
        }

        public void editMarkers(byte row, byte column, bool operation)
        {
            byte cellNum = cellsBoard[row, column].GetValue();
            if (cellNum != 0)
            {
                EditRowMarkers(row, cellNum, operation);
                EditColumnMarkers(column, cellNum, operation);
                EditBoxMarker(row, column, cellNum, operation);
            }
        }

        private void EditBoxMarker(byte row, byte column, byte marker, bool operation)
        {
            int sqrt = (int)Math.Sqrt(dimensionLen);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int rowIteration = boxRowStart; rowIteration < boxRowStart + sqrt; rowIteration++)
            {
                for (int columnIteration = boxColStart; columnIteration < boxColStart + sqrt; columnIteration++)
                {
                    cellsBoard[rowIteration, columnIteration].EditMarker(marker, operation);
                }
            }
        }

        private void EditColumnMarkers(byte column, byte marker, bool operation)
        {
            for (byte i = 0; i < dimensionLen; i++)
                cellsBoard[i, column].EditMarker(marker, operation);
        }

        private void EditRowMarkers(byte row, byte marker, bool operation)
        {
            for (byte i = 0; i < dimensionLen; i++)
                cellsBoard[row, i].EditMarker(marker, operation);
        }

        internal void SetCellNumber(byte row, byte column, byte num)
        {
            cellsBoard[row, column].SetNumber(num);
        }

        internal char[,] GetCharBoard()
        {
            char[,] charBoard = new char[dimensionLen, dimensionLen];

            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte column = 0; column < dimensionLen; column++)
                {
                    byte number = cellsBoard[row, column].GetValue();
                    charBoard[row, column] = (char)('0' + number);
                }
            }

            return charBoard;
        }

        public override string ToString()
        {
            return GetBoardData();
        }
    }
}
