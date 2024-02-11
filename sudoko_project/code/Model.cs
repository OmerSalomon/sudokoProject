using System;
using System.Collections.Generic;
using System.Text;

namespace sudoko_project
{
    internal class Cell
    {
        private int value;
        private HashSet<int> markers;
        private HashSet<Cell> friends;

        internal Cell(int num, int markerAmount)
        {
            friends = new HashSet<Cell>();
            markers = new HashSet<int>();

            if (num == 0)
            {
                for (int i = 1; i <= markerAmount; i++)
                {
                    markers.Add(i);
                }
            }

            this.value = num;
        }

        internal HashSet<int> GetMarkers()
        {
            return markers;
        }

        internal void RemoveMarker(int marker)
        {
            markers.Remove(marker);
        }

        internal void AddMarker(int marker)
        {
            markers.Add(marker);
        }

        internal int GetValue()
        {
            return value;
        }

        internal void SetValue(int value)
        {
            this.value = value;
        }

        internal int getMarkersCount()
        {
            return markers.Count;
        }

        public override string ToString()
        {
            return value.ToString();
        }

        internal bool HasMarker(int marker)
        {
            return markers.Contains(marker);
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
        private int dimensionLen;

        public Board(char[,] charBoard)
        {
            if (charBoard.GetLength(0) != charBoard.GetLength(1))
                throw new Exception("Board dimension size are not equal"); // Updated to a generic exception.

            dimensionLen = charBoard.GetLength(0);
            cellsBoard = new Cell[dimensionLen, dimensionLen];

            for (int y = 0; y < dimensionLen; y++)
            {
                for (int x = 0; x < dimensionLen; x++)
                {
                    int cellValue = charBoard[y, x] - '0';
                    cellsBoard[y, x] = new Cell(cellValue, dimensionLen);
                }
            }

            CreateGraph();
        }

        private void CreateGraph()
        {
            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    GenerateFriendsForCell(row, column);
                }
            }
        }

        private void GenerateFriendsForCell(int row, int column)
        {
            Cell mainCell = cellsBoard[row, column];

            for (int i = 0; i < dimensionLen; i++)
            {
                Cell friend = cellsBoard[row, i];
                if (friend != mainCell)
                    mainCell.AddFriend(friend);
            }

            for (int i = 0; i < dimensionLen; i++)
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
            for (int y = 0; y < dimensionLen; y++)
            {
                for (int x = 0; x < dimensionLen; x++)
                {
                    string possibleNumbersString = string.Join(", ", cellsBoard[y, x].GetMarkers());
                    int cellNumber = cellsBoard[y, x].GetValue();
                    boardData.AppendLine($"[{x}, {y}]: |{cellNumber}| {possibleNumbersString}");
                }
            }
            return boardData.ToString();
        }

        internal void SetCellValue(int row, int column, int value)
        {
            cellsBoard[row, column].SetValue(value);
        }

        internal char[,] GetCharBoard()
        {
            char[,] charBoard = new char[dimensionLen, dimensionLen];

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    int number = cellsBoard[row, column].GetValue();
                    charBoard[row, column] = (char)('0' + number);
                }
            }

            return charBoard;
        }

        public override string ToString()
        {
            // Assuming Grider.ConvertGridToString exists and can handle the updated types
            return Grider.ConvertGridToString(GetCharBoard());
        }

        internal int GetDimensionLen()
        {
            return dimensionLen;
        }

        internal bool isBoardFull()
        {
            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    if (cellsBoard[row, column].GetValue() == 0)
                        return false;
                }
            }
            return true;
        }

        internal HashSet<int> getCellMarker(int row, int column)
        {
            return cellsBoard[row, column].GetMarkers();
        }

        internal Cell GetCell(int row, int column)
        {
            return cellsBoard[row, column];
        }
    }
}
