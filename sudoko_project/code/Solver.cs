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
            Console.WriteLine(Grider.ConvertGridToString(board.GetCharBoard()));
            Console.WriteLine();

            if (!board.AllCellsHaveMarkers())
                return;

            if (!board.isBoardFull())
            {
                

                (byte row, byte column) = board.FindLessMarkedCell();

                HashSet<byte> cellMarker = board.getCellMarker(row, column);

                HashSet<byte> copyCellMarkers = new HashSet<byte>(cellMarker);

                foreach (byte marker in copyCellMarkers)
                {
                    board.setCellNumber(row, column, marker);
                    HashSet<(byte, byte)> removedMarkerCord = board.SpreadReduce(row, column, marker);
                    solveBackTrack();

                    if (!isBoardSolved)
                    {
                        board.setCellNumber(row, column, marker);
                        foreach ((byte, byte) cord in removedMarkerCord)
                        {
                            board.AddMarkerToCell(row, column, marker);
                        }
                        board.setCellNumber(row, column, 0);
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
