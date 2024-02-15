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

            Cell minMarkedCell = FindMinMarkedCell(board.CellsSet);
            int emptyCells = CountEmptyCells(board.CellsSet);

            if (!IsBoardValid())
                throw new Exception("Sudoko is invalid");

            bool isSolved = SolveBackTrack(minMarkedCell, emptyCells);
            
            if (!isSolved)
                throw new SudokoException("Sudoko is unsolvable");

            return board.GetCharBoard();
        }

        private int CountEmptyCells(HashSet<Cell> cellsSet)
        {
            int emptyCellsCount = 0;

            foreach (Cell cell in cellsSet) 
            {
                if (cell.Value == 0)
                    emptyCellsCount++;
            }

            return emptyCellsCount;
        }

        private bool IsBoardValid()
        {
            int dimensionLen = board.Len;

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.CellsBoard[row, column];

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
            int dimensionLen = board.Len;

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.CellsBoard[row, column];

                    if (cell.Value != 0)
                    {
                        ReduceCells(cell.Friends, cell.Value);
                    }
                }
            }
        }

        private int GetFilledCellsNumber()
        {
            int sum = 0;

            int dimensionLen = board.Len;

            for (int row = 0; row < dimensionLen; row++)
            {
                for (int column = 0; column < dimensionLen; column++)
                {
                    Cell cell = board.CellsBoard[row, column];

                    if (cell.Value != 0)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        private HashSet<Cell> ReduceCells(HashSet<Cell> cells, int marker)
        {
            HashSet<Cell> removedMarkerCells = new HashSet<Cell>();

            foreach (Cell friend in cells)
            {
                if (friend.Value == 0 && friend.Markers.Contains(marker))
                {
                    friend.Markers.Remove(marker);
                    removedMarkerCells.Add(friend);
                }
            }

            return removedMarkerCells;
        }

        private Cell FindMinMarkedCell(HashSet<Cell> cells)
        {
            Cell res = null;

            int minMarkersCount = int.MaxValue;

            foreach (Cell cell in cells)
            {
                if (cell.Value == 0)
                {
                    if (cell.Markers.Count == 0)
                        return cell;

                    if (cell.Markers.Count < minMarkersCount)
                    {
                        res = cell;
                        minMarkersCount = cell.Markers.Count;
                    }
                }

                
            }

            return res;
        }

        private bool SolveBackTrack(Cell minMarkedCell, int emptyCells)
        {
            if (emptyCells == 0)
                return true;


            HashSet<int> markersCopy = new HashSet<int>(minMarkedCell.Markers);

            foreach (int marker in markersCopy)
            {
                minMarkedCell.Value = marker;
                HashSet<Cell> removedMarkerCells = ReduceCells(minMarkedCell.Friends, marker);

                Cell nextMinMarkedCell = FindMinMarkedCell(minMarkedCell.Friends);
                if (nextMinMarkedCell == null)
                    nextMinMarkedCell = FindMinMarkedCell(board.CellsSet);


                bool isSolved = SolveBackTrack(nextMinMarkedCell, emptyCells - 1);

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

            minMarkedCell.Value = 0;

            return false;
        }
    }
}
