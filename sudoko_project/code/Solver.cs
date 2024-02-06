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
        private Stack<Move> movesStack;
        private Board board;

        public Solver() 
        {
            this.movesStack = new Stack<Move>();
        }

        public void Solve(char[,] charBoard)
        { 
            board = new Board(charBoard);
            board.ReduceBoardMarkers();
            BackTrack(board, 0);

        }

        //dont work
        public void BackTrack(Board reducedBoard, int moves)
        {
            int dimensionLen = reducedBoard.getDimensionLen();
            int totalSquares = dimensionLen * dimensionLen;
            (byte row, byte column) = reducedBoard.FindLessMarkedCell();

            if (moves < totalSquares)
            {
                board.setCellNumber(row, column, )
                board.reduceCell(row, column);
                BackTrack(reducedBoard, moves + 1);
            }
            else
            {
                Console.WriteLine(Grider.ConvertGridToString(board.GetCharBoard()));
            }

            board.ReverseReduceCell(row, column);

            
        }
    }

    internal class Move
    {
        private byte row;

        private byte column;

        private byte cellValue;

        internal Move(byte row, byte column, byte cellValue)
        {
            this.row = row;
            this.column = column;
            this.cellValue = cellValue;
        }

        internal byte getRow() { return row; }
        internal byte getColumn() { return column; }
        internal byte getCellValue() { return cellValue; }

    }
}
