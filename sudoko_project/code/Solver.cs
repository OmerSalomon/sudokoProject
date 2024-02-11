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
        private bool isBoardSolved = false;

        private Board board;

        internal char[,] Solve(char[,] charBoard)
        {
            board = new Board(charBoard);

            FirstBoardReduce();
            SolveBackTrack();
            return board.GetCharBoard();
        }

        private void FirstBoardReduce()
        {
            int dimentionLen = board.GetDimensionLen();

            for (byte row = 0; row < dimentionLen; row++)
            {
                for (byte column = 0; column < dimentionLen; column++)
                {
                    Cell cell = board.GetCell(row, column);

                    if (cell.GetValue() != 0)
                    {
                        SpreadReduce(cell, cell.GetValue());
                    }
                }
            }
        }

        private HashSet<Cell> SpreadReduce(Cell cell, byte marker)
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

            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte colunm = 0; colunm < dimensionLen; colunm++)
                {
                    Cell cell = board.GetCell(row, colunm);
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
            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte column = 0; column < dimensionLen; column++)
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

            for (byte row = 0; row < dimensionLen; row++)
            {
                for (byte column = 0; column < dimensionLen; column++)
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
            Console.WriteLine(Grider.ConvertGridToString(board.GetCharBoard()));
            Console.WriteLine();

            if (IsBoardFull())
                return true;

            if (!AllCellsHaveMarkers())
                return false;

            Cell lessMarkedCell = FindLessMarkedCell();

            HashSet<byte> markersCopy = new HashSet<byte>(lessMarkedCell.GetMarkers());

            foreach (byte marker in markersCopy)
            {
                lessMarkedCell.SetValue(marker);
                HashSet<Cell> removedMarkerCells = SpreadReduce(lessMarkedCell, marker);

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
