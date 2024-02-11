using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    internal class Solver
    {
        private Board board;

        internal char[,] Solve(char[,] charBoard)
        {
            board = new Board(charBoard);
            FirstBoardReduce();

            SolveBackTrack();

            if (!IsBoardValid())
                throw new Exception("Sudoko is invalid");

            if (!IsBoardFull())
                throw new SudokoException("Sudoko is unsolvable");

            return board.GetCharBoard();
        }

        private bool IsBoardValid()
        {
            int dimensionLen = board.GetDimensionLen();

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);

                    foreach (Cell friend in cell.GetFriend())
                    {
                        if (cell.GetValue() == friend.GetValue())
                            return false;
                    }
                }
            }

            return true;
        }

        private void FirstBoardReduce()
        {
            int dimensionLen = board.GetDimensionLen();

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);

                    if (cell.GetValue() != 0)
                    {
                        FriendReduce(cell, cell.GetValue());
                    }
                }
            }
        }

        private int GetFilledCellsNumber()
        {
            int sum = 0;

            int dimensionLen = board.GetDimensionLen();

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);

                    if (cell.GetValue() != 0)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        private HashSet<Cell> FriendReduce(Cell cell, int marker)
        {
            HashSet<Cell> removedMarkerCells = new HashSet<Cell>();

            foreach (Cell friend in cell.GetFriend())
            {
                if (friend.GetValue() == 0 && friend.HasMarker(marker))
                {
                    friend.RemoveMarker(marker);
                    removedMarkerCells.Add(friend);
                }
            }

            return removedMarkerCells;
        }

        private Cell FindLessMarkedCell()
        {
            Cell res = null;
            int dimensionLen = board.GetDimensionLen();

            int minMarkersCount = int.MaxValue;

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);
                    if (cell.GetValue() == 0)
                        if (cell.getMarkersCount() < minMarkersCount)
                        {
                            res = cell;
                            minMarkersCount = cell.getMarkersCount();
                        }
                }
            }

            return res;
        }

        internal bool IsBoardFull()
        {
            int dimensionLen = board.GetDimensionLen();
            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);
                    if (cell.GetValue() == 0)
                        return false;
                }
            }
            return true;
        }

        internal bool AllCellsHaveMarkers()
        {
            int dimensionLen = board.GetDimensionLen();

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);
                    if (cell.GetValue() == 0 && cell.getMarkersCount() == 0)
                        return false;
                }
            }
            return true;
        }

        private bool SolveBackTrack()
        {
            if (IsBoardFull())
                return true;

            if (!AllCellsHaveMarkers())
                return false;

            Cell lessMarkedCell = FindLessMarkedCell();

            HashSet<int> markersCopy = new HashSet<int>(lessMarkedCell.GetMarkers());

            foreach (int marker in markersCopy)
            {
                lessMarkedCell.SetValue(marker);
                HashSet<Cell> removedMarkerCells = FriendReduce(lessMarkedCell, marker);

                bool isSolved = SolveBackTrack();

                if (isSolved)
                    return true;
                else
                {
                    foreach (Cell cell in removedMarkerCells)
                    {
                        cell.AddMarker(marker);
                    }
                }
            }

            lessMarkedCell.SetValue(0);

            return false;
        }
    }
}
