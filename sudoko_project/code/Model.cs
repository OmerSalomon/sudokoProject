using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace sudoko_project
{
    internal class Cell
    {
        private byte value;
        private HashSet<byte> markers;
        private HashSet<Cell> friends;
        internal Cell(byte num, byte markerAmount)
        {
            friends = new HashSet<Cell>();
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

        internal void SetValue(byte value)
        {
            this.value = value;
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

        internal void AddFriend(Cell friend)
        {
            friends.Add(friend);
        }

        internal HashSet<Cell> GetFriend()
        {
            return friends;
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

            CreateGraph();
        }

        private void CreateGraph()
        {
            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte colunm = 0; colunm < dimensionLen; colunm++)
                {
                    GenerateFriendsForCell(row, colunm);
                }
            }
        }

        private void GenerateFriendsForCell(byte row, byte column)
        {
            Cell mainCell = cellsBoard[row, column];

            for (byte i = 0; i < dimensionLen; i++)
            {
                Cell friend = cellsBoard[row, i];
                if (friend != mainCell)
                    mainCell.AddFriend(friend);
            }

            for (byte i = 0; i < dimensionLen; i++)
            {
                Cell friend = cellsBoard[i, column];
                if (friend != mainCell)
                    mainCell.AddFriend(friend);
            }

            int sqrt = (int)Math.Sqrt(dimensionLen);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int rowIteration = boxRowStart; rowIteration < boxRowStart + sqrt; rowIteration++)
            {
                for (int columnIteration = boxColStart; columnIteration < boxColStart + sqrt; columnIteration++)
                {
                    Cell friend = cellsBoard[rowIteration, columnIteration];
                    if (friend != mainCell)
                        mainCell.AddFriend(friend);
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

        internal void SetCellValue(byte row, byte column, byte value)
        {
            cellsBoard[row, column].SetValue(value);
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
            return Grider.ConvertGridToString(GetCharBoard());
        }


        internal int GetDimensionLen()
        {
            return dimensionLen;
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

        internal Cell GetCell(byte row, byte colunm)
        {
            return cellsBoard[row, colunm];
        }
    }
}
