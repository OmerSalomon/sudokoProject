using System;
using System.Collections.Generic;
using System.Text;

namespace sudoko_project
{
    internal class Cell
    {
        public int Value { get; set; }
        public HashSet<int> Markers { get; set; }
        public List<Cell> Friends { get; set; }

        internal Cell(int value, int markerAmount)
        {
            Friends = new List<Cell>();
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
    }

    internal class Board
    {
        public Cell[,] CellsBoard { get; }
        public int Len { get;}

        public List<Cell> CellsSet { get; }
        public static int EMPTY_TILE_VALUE = 0;

        public Board(char[,] charBoard)
        {
            if (charBoard.GetLength(0) != charBoard.GetLength(1))
                throw new Exception("Board dimension size are not equal"); // Updated to a generic exception.

            Len = charBoard.GetLength(0);
            CellsBoard = new Cell[Len, Len];

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    int cellValue = charBoard[row, column] - '0';
                    Cell cell = new Cell(cellValue, Len);
                    CellsBoard[row, column] = cell;
                }
            }

            CreateGraph();

            CellsSet = new List<Cell>();
            CreateBoardSet();
        }

        private void CreateGraph()
        {
            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    GenerateFriendsForCell(row, column);
                }
            }
        }

        private void GenerateFriendsForCell(int row, int column)
        {
            Cell mainCell = CellsBoard[row, column];

            for (int i = 0; i < Len; i++)
            {
                Cell friend = CellsBoard[row, i];
                if (friend != mainCell && !mainCell.Friends.Contains(friend))
                    mainCell.Friends.Add(friend);
            }

            for (int i = 0; i < Len; i++)
            {
                Cell friend = CellsBoard[i, column];
                if (friend != mainCell && !mainCell.Friends.Contains(friend))
                    mainCell.Friends.Add(friend);
            }

            int sqrt = (int)Math.Sqrt(Len);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int rowIteration = boxRowStart; rowIteration < boxRowStart + sqrt; rowIteration++)
            {
                for (int columnIteration = boxColStart; columnIteration < boxColStart + sqrt; columnIteration++)
                {
                    Cell friend = CellsBoard[rowIteration, columnIteration];
                    if (friend != mainCell && !mainCell.Friends.Contains(friend))
                        mainCell.Friends.Add(friend);
                }
            }
        }

        internal string GetBoardData()
        {
            StringBuilder boardData = new StringBuilder();
            for (int y = 0; y < Len; y++)
            {
                for (int x = 0; x < Len; x++)
                {
                    string possibleNumbersString = string.Join(", ", CellsBoard[y, x].Markers);
                    int cellNumber = CellsBoard[y, x].Value;
                    boardData.AppendLine($"[{x}, {y}]: |{cellNumber}| {possibleNumbersString}");
                }
            }
            return boardData.ToString();
        }

        internal void SetCellValue(int row, int column, int value)
        {
            CellsBoard[row, column].Value = value;
        }

        internal char[,] GetCharBoard()
        {
            char[,] charBoard = new char[Len, Len];

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    int number = CellsBoard[row, column].Value;
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

        internal void CreateBoardSet()
        {
            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    CellsSet.Add(CellsBoard[row, column]);
                }
            }
        }

        internal string GetSudokoString()
        {
            StringBuilder boardString = new StringBuilder();

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    Cell cell = CellsBoard[row, column];
                    boardString.Append((char)('0' + cell.Value));
                }
            }

            return boardString.ToString();
        }
    }
}
