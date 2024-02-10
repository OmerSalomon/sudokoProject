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
            board.ReduceBoard();
            solveBackTrackB();
            return board.GetCharBoard();
        }

        private bool solveBackTrackB() 
        {
            Console.WriteLine(board.GetFilledCellNumber());
            //Console.WriteLine(Grider.ConvertGridToString(board.GetCharBoard()));
            Console.WriteLine();

            if (board.isBoardFull())
                return true;

            if (!board.AllCellsHaveMarkers())
                return false;

            (byte row, byte column) = board.FindLessMarkedCell();

            HashSet<byte> cellMarkers = new HashSet<byte>(board.getCellMarker(row, column)); //copy of the set

            foreach (byte marker in cellMarkers)
            {
                board.setCellValue(row, column, marker);
                HashSet<(byte, byte)> removedMarkerCord = board.SpreadReduce(row, column, marker);
                bool isSolved = solveBackTrackB();

                if (isSolved)
                    return true;
                else
                {
                    foreach ((byte a, byte b) in removedMarkerCord)
                    {
                        board.AddMarkerToCell(a, b, marker);
                    }
                }
            }

            board.setCellValue(row, column, 0);

            return false;
        }


    }

}
