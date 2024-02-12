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

            if (board.EmptyCells.Count != 0)
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
                        foreach (Cell friend in cell.Friends)
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
                        FriendReduce(cell, cell.Value);
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

        private HashSet<Cell> FriendReduce(Cell cell, int marker)
        {
            HashSet<Cell> removedMarkerCells = new HashSet<Cell>();

            foreach (Cell friend in cell.Friends)
            {
                if (friend.Value == 0 && friend.HasMarker(marker))
                {
                    friend.Markers.Remove(marker);
                    removedMarkerCells.Add(friend);
                }
            }

            return removedMarkerCells;
        }

        private Cell FindLessMarkedCell(HashSet<Cell> emptyCells)
        {
            Cell res = null;

            int minMarkersCount = int.MaxValue;

            foreach (Cell cell in emptyCells)
            {
                if (cell.Markers.Count < minMarkersCount)
                {
                    res = cell;
                    minMarkersCount = cell.Markers.Count;
                }
            }

            return res;
        }

        private bool SolveBackTrack()
        {
            if (board.EmptyCells.Count == 0)
                return true;   

            Cell lessMarkedCell = FindLessMarkedCell(board.EmptyCells);
            board.EmptyCells.Remove(lessMarkedCell);


            HashSet<int> markersCopy = new HashSet<int>(lessMarkedCell.Markers);

            foreach (int marker in markersCopy)
            {
                lessMarkedCell.Value = marker;
                HashSet<Cell> removedMarkerCells = FriendReduce(lessMarkedCell, marker);

                bool isSolved = SolveBackTrack();

                if (isSolved)
                    return true;
                else
                {
                    foreach (Cell cell in removedMarkerCells)
                    {
                        cell.Markers.Add(marker);
                    }
                }
            }

            board.EmptyCells.Add(lessMarkedCell);
            lessMarkedCell.Value = 0;

            return false;
        }
    }
}
