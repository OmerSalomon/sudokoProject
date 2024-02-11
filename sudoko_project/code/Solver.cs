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

            if (!IsBoardValid())
                throw new Exception("Sudoko is invalid");

            SolveBackTrack();

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

                    if (cell.Value != 0)
                    {
                        foreach (Cell friend in cell.TotalFriends)
                        {
                            if (cell.Value == friend.Value)
                                return false;
                        }
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

                    if (cell.Value != 0)
                    {
                        UnitReduce(cell, cell.TotalFriends, cell.Value);
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

                    if (cell.Value != 0)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        private HashSet<Cell> UnitReduce(Cell cell, HashSet<Cell> unit, int marker)
        {
            HashSet<Cell> removedMarkerCells = new HashSet<Cell>();

            foreach (Cell friend in unit)
            {
                if (friend.Value == 0 && friend.HasMarker(marker))
                {
                    friend.Markers.Remove(marker);
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
                    if (cell.Value == 0)
                        if (cell.Markers.Count < minMarkersCount)
                        {
                            res = cell;
                            minMarkersCount = cell.Markers.Count;
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
                    if (cell.Value == 0)
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
                    if (cell.Value == 0 && cell.Markers.Count == 0)
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

            HashSet<int> markersCopy = new HashSet<int>(lessMarkedCell.Markers);

            if (IsThereHiddenPairInUnit(lessMarkedCell, lessMarkedCell.RowFriends))

                

            foreach (int marker in markersCopy)
            {
                lessMarkedCell.Value = marker;
                HashSet<Cell> FriendReduceAffectedCells = UnitReduce(lessMarkedCell, lessMarkedCell.TotalFriends ,marker);

                bool isSolved = SolveBackTrack();

                if (isSolved)
                    return true;
                else
                {
                    foreach (Cell cell in FriendReduceAffectedCells)
                    {
                        cell.Markers.Add(marker);
                    }
                }
            }

            lessMarkedCell.Value = 0;

            return false;
        }

        private bool IsThereHiddenPairInUnit(Cell mainCell, HashSet<Cell> unit)
        {
            foreach (Cell cell in unit)
                if (mainCell != cell && mainCell.Equals(cell))
                    return true;

            return false;
        }

        
    }
}
