using System;
using System.Collections.Generic;
using System.Text;

namespace sudoko_project
{
    internal class Cell
    {
        public int Value { get; set; }
        public HashSet<Cell> TotalFriends { get; set; }
        public HashSet<int> Markers { get; set; }
        public HashSet<Cell> RowFriends { get; set; }
        public HashSet<Cell> ColumnFriend { get; set; }
        public HashSet<Cell> boxFriend { get; set; }


        internal Cell(int value, int markerAmount)
        {
            TotalFriends = new HashSet<Cell>();
            RowFriends = new HashSet<Cell>();
            ColumnFriend = new HashSet<Cell>();
            boxFriend = new HashSet<Cell>();
            Markers = new HashSet<int>();

            if (value == 0)
            {
                for (int i = 1; i <= markerAmount; i++)
                {
                    Markers.Add(i);
                }
            }

            this.Value = value;
        }


        public override string ToString()
        {
            return Value.ToString();
        }

        internal bool HasMarker(int marker)
        {
            return Markers.Contains(marker);
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

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    int cellValue = charBoard[row, column] - '0';
                    cellsBoard[row, column] = new Cell(cellValue, dimensionLen);
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
                    mainCell.RowFriends.Add(friend);
            }

            for (int i = 0; i < dimensionLen; i++)
            {
                Cell friend = cellsBoard[i, column];
                if (friend != mainCell)
                    mainCell.ColumnFriend.Add(friend);
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
                        mainCell.boxFriend.Add(friend);
                }
            }
            
            mainCell.TotalFriends.UnionWith(mainCell.RowFriends);
            mainCell.TotalFriends.UnionWith(mainCell.ColumnFriend);
            mainCell.TotalFriends.UnionWith(mainCell.boxFriend);
        }

        internal string GetBoardData()
        {
            StringBuilder boardData = new StringBuilder();
            for (int y = 0; y < dimensionLen; y++)
            {
                for (int x = 0; x < dimensionLen; x++)
                {
                    string possibleNumbersString = string.Join(", ", cellsBoard[y, x].Markers);
                    int cellNumber = cellsBoard[y, x].Value;
                    boardData.AppendLine($"[{x}, {y}]: |{cellNumber}| {possibleNumbersString}");
                }
            }
            return boardData.ToString();
        }

        internal void SetCellValue(int row, int column, int value)
        {
            cellsBoard[row, column].Value = value;
        }

        internal char[,] GetCharBoard()
        {
            char[,] charBoard = new char[dimensionLen, dimensionLen];

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    int number = cellsBoard[row, column].Value;
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

        internal HashSet<int> getCellMarker(int row, int column)
        {
            return cellsBoard[row, column].Markers;
        }

        internal Cell GetCell(int row, int column)
        {
            return cellsBoard[row, column];
        }
    }
}
