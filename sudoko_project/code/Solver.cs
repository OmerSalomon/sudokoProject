using System;
using System.Collections.Generic;
using System.Data;
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
            solveBackTrack();
            return board.GetCharBoard();
        }

        private void solveBackTrack()
        {
            if (!board.isBoardFull())
            {
                (byte row, byte column) = board.FindLessMarkedCell();

                HashSet<byte> cellMarker = board.getCellMarker(row, column);

                HashSet<byte> copyCellMarkers = new HashSet<byte>(cellMarker);

                foreach (byte marker in copyCellMarkers)
                {
                    board.setCellNumber(row, column, marker);
                    board.reduceCell(row, column);

                    if (!isBoardSolved)
                    {
                        solveBackTrack();
                        board.ReverseReduceCell(row, column);
                    }
                }
            }
            else
            {
                isBoardSolved = true;
            }

        }

    }

}
