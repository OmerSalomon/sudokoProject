using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;
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

        internal void RemoveMarker(byte marker)
        {
            markers.Remove(marker);
        }

        internal void AddMarker(byte marker)
        {
            markers.Add(marker);
        }

        internal byte GetValue()
        {
            return value;
        }

        internal void SetNumber(byte value)
        {
            this.value = value;
            markers.Remove(value);
        }

        internal byte getMarkersCount()
        {
            return (byte)markers.Count;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        internal bool HasMarker(byte marker)
        {
            if (markers.Contains(marker))
                return true;
            else
                return false;
        }
    }

    internal class Board
    {
        private Cell[,] cellsBoard;

        private byte dimensionLen;

        private bool boardSolved = false;


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

            ReduceBoard();
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

        internal (byte, byte) FindLessMarkedCell()
        {
            byte minMarkersCount = dimensionLen;
            byte resRow = 0;
            byte resColumn = 0;

            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte colunm = 0; colunm < dimensionLen; colunm++)
                {
                    Cell cell = cellsBoard[row, colunm];
                    if (cell.GetValue() == 0)
                        if (cell.getMarkersCount() < minMarkersCount)
                        {
                            resRow = row;
                            resColumn = colunm;
                            minMarkersCount = cell.getMarkersCount();
                        }
                }
            }

            return (resRow, resColumn);
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

        public void ReduceBoard()
        {
            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte column = 0; column < dimensionLen; column++)
                {
                    byte cellValue = cellsBoard[row, column].GetValue();
                    SpreadReduce(row, column, cellValue);
                }
            }
        }

        /// <summary>
        /// Editing the markers of the row, column and the box
        /// that are given 
        /// 
        /// operation = false: remove markers
        /// operation = true: add markers
        /// </summary>
        /// 
        private HashSet<(byte, byte)> EditBoxMarker(byte row, byte column, byte marker)
        {
            HashSet<(byte, byte)> cords = new HashSet<(byte, byte)> ();

            int sqrt = (int)Math.Sqrt(dimensionLen);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int rowIteration = boxRowStart; rowIteration < boxRowStart + sqrt; rowIteration++)
            {
                for (int columnIteration = boxColStart; columnIteration < boxColStart + sqrt; columnIteration++)
                {
                    if (cellsBoard[rowIteration, columnIteration].HasMarker(marker))
                    {
                        cords.Add(((byte)rowIteration, (byte)columnIteration));
                        cellsBoard[rowIteration, columnIteration].RemoveMarker(marker);
                    }
                }
            }
            return cords;
        }

        private HashSet<(byte, byte)> EditColumnMarkers(byte column, byte marker)
        {
            HashSet<(byte, byte)> res = new HashSet<(byte, byte)>();

            for (byte i = 0; i < dimensionLen; i++)
            {
                if (cellsBoard[i, column].HasMarker(marker))
                {
                    cellsBoard[i, column].RemoveMarker(marker);
                    res.Add((i, column));
                }
            }
                
            return res;
        }

        private HashSet<(byte , byte)> EditRowMarkers(byte row, byte marker)
        {
            HashSet<(byte, byte)> res = new HashSet<(byte, byte)>();

            for (byte i = 0; i < dimensionLen; i++)
            {
                if (cellsBoard[row, i].HasMarker(marker))
                {
                    cellsBoard[row, i].RemoveMarker(marker);
                    res.Add((row, i));
                }

            }

            return res; 
        }

        internal void SetCellValue(byte row, byte column, byte value)
        {
            cellsBoard[row, column].SetNumber(value);
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

        internal HashSet<(byte, byte)> SpreadReduce(byte row, byte column, byte marker)
        {
            HashSet<(byte, byte)> res = new HashSet<(byte, byte)>();

            byte cellNum = cellsBoard[row, column].GetValue();
            if (cellNum != 0)
            {
                res.UnionWith(EditRowMarkers(row, marker));
                res.UnionWith(EditColumnMarkers(column, marker));
                res.UnionWith(EditBoxMarker(row, column, marker));
            }

            return res;
        }

        internal int getDimensionLen()
        {
            return dimensionLen;
        }

        internal void setCellNumber(byte row, byte column, byte cellValue)
        {
            cellsBoard[row, column].SetNumber(cellValue);
        }
        internal bool isBoardFull()
        {
            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte column = 0; column < dimensionLen; column++)
                {
                    if (cellsBoard[row, column].GetValue() == 0)
                        return false;
                }
            }
            return true;
        }

        internal HashSet<byte> getCellMarker(byte row, byte column)
        {
            return cellsBoard[row, column].GetMarkers();
        }

        internal void AddMarkerToCell(byte row, byte column, byte marker)
        {
            cellsBoard[row, column].AddMarker(marker);
        }

        internal bool AllCellsHaveMarkers()
        {
            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte column = 0; column < dimensionLen; column++)
                {
                    if (cellsBoard[row, column].GetValue() == 0 && cellsBoard[row, column].getMarkersCount() == 0)
                        return false;
                }
            }
            return true;
        }
    }
}
