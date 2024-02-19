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
    public class Solver
    {
        private Board board;

        /// <summary>
        /// Attempts to solve a Sudoku puzzle represented by a string and returns the solved puzzle as a string.
        /// </summary>
        /// <param name="sudokoString">The string representation of the Sudoku puzzle, where each character represents a cell's value and empty cells are denoted by '0'.</param>
        /// <returns>The string representation of the solved Sudoku puzzle.</returns>
        /// <exception cref="SudokoException">Thrown if the Sudoku puzzle is determined to be invalid or unsolvable.</exception>
        /// <remarks>
        /// This method converts the input string into a 2D character array representing the Sudoku board, initializes the board, 
        /// and applies a reduction process to simplify the puzzle. It then attempts to solve the puzzle using a backtracking algorithm, 
        /// starting from the cell with the minimum number of markers (potential values).
        /// 
        /// The method performs several checks during the solving process:
        /// - Validates the initial puzzle's validity; if invalid, an exception is thrown indicating the puzzle is invalid.
        /// - Attempts to solve the puzzle through backtracking; if unsolvable, an exception is thrown indicating the puzzle is unsolvable.
        /// 
        /// If the puzzle is successfully solved, the method returns a string representing the solved board.
        /// </remarks>
        public string Solve(string sudokoString)
        {
            char[,] charBoard = Grider.ConvertStringToCharArr(sudokoString);
            
            board = new Board(charBoard);
            TotalBoardReduce();

            Cell minMarkedCell = FindMinMarkedCell(board.Cells);
            int emptyCells = CountEmptyCells(board.Cells);

            if (!IsBoardValid() || minMarkedCell.Markers.Count == 0)
                throw new InvalidSudokoException("Sudoko is invalid");

            bool isSolved = SolveBackTrack(minMarkedCell, emptyCells);
            
            if (!isSolved)
                throw new UnsolvableSudokoException("Sudoko is unsolvable");

            return board.GetSudokoLinearString();
        }

        /// <summary>
        /// Counts and returns the number of empty cells in a given collection.
        /// </summary>
        /// <param name="cells">The collection of cells to be examined.</param>
        /// <returns>The number of cells in the collection that have a value of 0, indicating they are empty.</returns>
        /// <remarks>
        /// This method iterates through the provided collection of cells, incrementing a counter for each cell that is found to be empty (value equals 0).
        /// It's particularly useful in scenarios where the number of unsolved or empty cells needs to be known, such as in puzzle solving algorithms
        /// or when assessing the difficulty or progress state of a game board.
        /// </remarks>
        private int CountEmptyCells(IEnumerable<Cell> cells)
        {
            int emptyCellsCount = 0;

            foreach (Cell cell in cells) 
            {
                if (cell.Value == 0)
                    emptyCellsCount++;
            }

            return emptyCellsCount;
        }

        /// <summary>
        /// Validates the board by ensuring that no two related cells ('friends') contain the same non-zero value.
        /// </summary>
        /// <returns>
        /// True if the board is valid, meaning no 'friend' cells have matching non-zero values; otherwise, false.
        /// </returns>
        /// <remarks>
        /// This method iterates through each cell on the board, checking every cell's value against its 'friends'' values.
        /// A board is considered valid if, for every cell with a non-zero value, none of its 'friends' have the same value.
        /// This validation is crucial for puzzle games like Sudoku, where each number must be unique in its row, column, and square.
        /// 
        /// The method assumes that each cell's 'friends' are correctly identified and that the board's structure (rows, columns, and squares)
        /// conforms to the game's rules. It does not check for the completeness of the solution but rather the consistency of the current state.
        /// </remarks>
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

        /// <summary>
        /// Applies a reduction process across the entire board, removing a specific marker from the friends of each non-zero valued cell.
        /// </summary>
        /// <remarks>
        /// This method iterates through every cell in the board, focusing on cells with a non-zero value. For each of these cells,
        /// it invokes the <see cref="RemoveMarkers"/> method on their friends, passing in the cell's value as the marker to remove.
        /// This reduction process is critical for puzzles and games where the value of one cell affects the potential values of related cells,
        /// allowing for the iterative narrowing of possibilities until the solution is reached.
        /// 
        /// The method assumes that each cell maintains a list of 'friend' cells—other cells that are logically related to it within the context
        /// of the puzzle or game (e.g., cells in the same row, column, or square in Sudoku). It's an essential step in solving strategies,
        /// ensuring that each cell's potential values are as constrained as possible given the current state of the board.
        /// </remarks>
        private void TotalBoardReduce()
        {
            foreach (Cell cell in board.Cells)
            {
                if (cell.Value != 0)
                {
                    RemoveMarkers(cell.Friends, cell.Value);
                }
            }
        }

        /// <summary>
        /// Removes a specified marker from each cell in the provided collection that is empty and contains the marker, then returns those cells.
        /// </summary>
        /// <param name="cells">The collection of cells to be processed.</param>
        /// <param name="marker">The marker to be removed from the cells.</param>
        /// <returns>
        /// A collection of cells from which the marker was removed. These cells are empty (value of 0) and previously contained the specified marker.
        /// </returns>
        /// <remarks>
        /// This method iterates through the given collection of cells, identifying those that are empty (have a value of 0) and contain the specified marker 
        /// in their marker set. It then removes the marker from such cells and collects these cells into a list, which is returned to the caller. 
        /// This operation is useful in scenarios where the presence of a marker in a cell's possible values needs to be re-evaluated, such as in puzzle solving 
        /// algorithms where certain values become impossible due to game progress.
        /// </remarks>
        private IEnumerable<Cell> RemoveMarkers(IEnumerable<Cell> cells, int marker)
        {
            List<Cell> removedMarkerCells = new List<Cell> ();

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

        /// <summary>
        /// Finds and returns the cell with the fewest markers among a collection of cells, prioritizing cells with no markers.
        /// </summary>
        /// <param name="cells">The collection of cells to search through.</param>
        /// <returns>
        /// The cell with the fewest markers. If multiple cells have the same minimum number of markers, the first encountered cell is returned.
        /// If a cell with no markers is found, it is immediately returned. If all cells have a value other than 0, or if the collection is empty, returns null.
        /// </returns>
        /// <remarks>
        /// This method iterates through the provided cell collection, identifying cells that are empty (value of 0) and counting their markers. 
        /// It looks for the cell with the smallest number of markers, which indicates the cell has fewer potential values and may be easier to solve or fill next.
        /// This is particularly useful in puzzles like Sudoku, where selecting the next cell to solve based on the least number of possibilities can 
        /// significantly streamline the solving process. A cell with no markers (indicating a contradiction or an error in puzzle setup) is considered 
        /// the minimum and is immediately returned if encountered.
        /// </remarks>
        private Cell FindMinMarkedCell(IEnumerable<Cell> cells)
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

        /// <summary>
        /// Attempts to solve the Sudoku puzzle using a backtracking algorithm, starting from a cell with the minimum number of markers.
        /// </summary>
        /// <param name="minMarkedCell">The cell with the minimum number of markers to start the backtracking process.</param>
        /// <param name="emptyCells">The total number of empty cells remaining on the board.</param>
        /// <returns>True if the puzzle is solved successfully; otherwise, false.</returns>
        /// <remarks>
        /// This method is a recursive implementation of the backtracking algorithm to solve the Sudoku puzzle. It works by:
        /// - Checking if there are no more empty cells, in which case the puzzle is solved.
        /// - Making a copy of the markers in the cell with the minimum markers and trying each marker to see if it leads to a solution.
        /// - After setting a marker, it removes this marker from the 'friend' cells to maintain the puzzle's constraints and recursively
        ///   calls itself with the next cell having the minimum markers.
        /// - If a marker does not lead to a solution, it backtracks by resetting the markers removed and trying the next marker.
        /// - The process continues until the puzzle is solved or no markers lead to a solution, indicating the puzzle is unsolvable.
        /// 
        /// The method leverages <see cref="RemoveMarkers"/> to update the state of the board based on the current marker being tested and
        /// <see cref="FindMinMarkedCell"/> to select the next cell for the backtracking process.
        /// </remarks>
        private bool SolveBackTrack(Cell minMarkedCell, int emptyCells)
        {
            if (emptyCells == 0)
                return true;

            IEnumerable<int> markersCopy = new HashSet<int>(minMarkedCell.Markers);

            foreach (int marker in markersCopy)
            {
                minMarkedCell.Value = marker;
                IEnumerable<Cell> removedMarkerCells = RemoveMarkers(minMarkedCell.Friends, marker);

                Cell nextMinMarkedCell = FindMinMarkedCell(minMarkedCell.Friends);
                if (nextMinMarkedCell == null)
                    nextMinMarkedCell = FindMinMarkedCell(board.Cells);

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

            minMarkedCell.Value = Board.EMPTY_TILE_VALUE;

            return false;
        }

    }
}
