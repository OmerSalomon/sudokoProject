using System;
using System.Collections.Generic;
using System.Text;

namespace sudoko_project
{
    internal class Cell
    {
        public int Value { get; set; }
        public HashSet<int> Markers { get; set; }
        public List<Cell> Friends { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Cell"/> class with a specified value and a set of potential markers.
        /// </summary>
        /// <param name="value">The initial value of the cell. A value of 0 indicates an empty cell that can have markers.</param>
        /// <param name="markerAmount">The total number of potential markers a cell can have. Markers are used to denote possible values for empty cells.</param>
        /// <remarks>
        /// This constructor sets up the cell with the following logic:
        /// - Initializes a list to hold 'friend' cells. Friends are other cells that are related to this cell based on the game or puzzle's rules.
        /// - Initializes a set of integers to hold markers. Markers represent potential values that an empty cell (value 0) can take.
        /// - If the initial value of the cell is 0 (indicating an empty cell), it populates the markers set with integers from 1 to markerAmount, inclusive.
        /// This approach is particularly useful in puzzles like Sudoku, where the value of a cell influences its markers and its relationship with other cells.
        /// </remarks>
        internal Cell(int value, int markerAmount)
        {
            Friends = new List<Cell>();
            Markers = new HashSet<int>();

            if (value == 0)
            {
                for (int i = 1; i <= markerAmount; i++)
                {
                    Markers.Add(i);
                }
            }

            this.Value = value;
        }

        /// <summary>
        /// Returns a string that represents the current cell's value.
        /// </summary>
        /// <returns>A string representation of the cell's value.</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }

    internal class Board
    {
        public Cell[,] CellsBoard { get; }
        public int Len { get;}

        public ICollection<Cell> Cells { get; }
        public static int EMPTY_TILE_VALUE = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Board"/> class, converting a 2D character array into a structured board of cells.
        /// </summary>
        /// <param name="charBoard">A 2D character array representing the initial state of the board, where each character corresponds to the value of a cell.</param>
        /// <exception cref="SudokoException">Thrown if the provided character board does not have equal dimensions (i.e., is not square).</exception>
        /// <remarks>
        /// This constructor performs several key initializations:
        /// - Validates that the input character board is square, throwing an exception if it is not.
        /// - Sets the board's length based on the dimension of the input array.
        /// - Initializes the <see cref="CellsBoard"/> with Cell objects, converting each character value from the input array to a numeric value for the cell.
        /// - Prepares the board for gameplay or solving algorithms by creating a graph of cell relationships and a collection of all cells.
        /// </remarks>
        public Board(char[,] charBoard)
        {
            if (charBoard.GetLength(0) != charBoard.GetLength(1))
                throw new SudokoException("Board dimension size are not equal"); // Updated to a generic exception.

            Len = charBoard.GetLength(0);
            CellsBoard = new Cell[Len, Len];

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    int cellValue = charBoard[row, column] - '0';
                    Cell cell = new Cell(cellValue, Len);
                    CellsBoard[row, column] = cell;
                }
            }

            Cells = new List<Cell>();
            CreateGraph();
            CreateBoardCollection();
        }

        /// <summary>
        /// Constructs a graph-like structure by establishing 'friend' relationships between all cells in a grid or board.
        /// </summary>
        /// <remarks>
        /// This method iterates through each cell in a grid or board, defined by its dimensions (Len x Len), and calls 
        /// <see cref="GenerateFriendsForCell"/> for each cell to populate its 'Friends' collection. The 'Friends' collections 
        /// are used to create a network of relationships between cells, where each cell is connected to others based on shared 
        /// row, column, or sub-grid membership. This graph-like structure is essential for solving or processing grid-based 
        /// puzzles or games where the relation between different cells affects the game's logic or outcome.
        /// </remarks>
        private void CreateGraph()
        {
            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    GenerateFriendsForCell(row, column);
                }
            }
        }

        /// <summary>
        /// Populates the 'Friends' collection of a specified cell with other cells that are considered its 'friends' based on their positions.
        /// </summary>
        /// <remarks>
        /// This method identifies and adds 'friend' cells to the specified cell's 'Friends' list based on three criteria:
        /// 1. All cells in the same row as the specified cell, excluding the cell itself.
        /// 2. All cells in the same column as the specified cell, excluding the cell itself.
        /// 3. All cells within the same sub-grid or box as the specified cell, based on the square root of the board length, 
        ///    ensuring to exclude the cell itself and any duplicates.
        /// This is particularly useful in grid-based puzzles or games where relationships between cells affect gameplay or logic, 
        /// such as Sudoku.
        /// </remarks>
        /// <param name="row">The row index of the cell for which 'friends' are being generated.</param>
        /// <param name="column">The column index of the cell for which 'friends' are being generated.</param>
        private void GenerateFriendsForCell(int row, int column)
        {
            Cell mainCell = CellsBoard[row, column];

            for (int i = 0; i < Len; i++)
            {
                Cell friend = CellsBoard[row, i];
                if (friend != mainCell && !mainCell.Friends.Contains(friend))
                    mainCell.Friends.Add(friend);
            }

            for (int i = 0; i < Len; i++)
            {
                Cell friend = CellsBoard[i, column];
                if (friend != mainCell && !mainCell.Friends.Contains(friend))
                    mainCell.Friends.Add(friend);
            }

            int sqrt = (int)Math.Sqrt(Len);
            int boxRowStart = row - row % sqrt;
            int boxColStart = column - column % sqrt;

            for (int rowIteration = boxRowStart; rowIteration < boxRowStart + sqrt; rowIteration++)
            {
                for (int columnIteration = boxColStart; columnIteration < boxColStart + sqrt; columnIteration++)
                {
                    Cell friend = CellsBoard[rowIteration, columnIteration];
                    if (friend != mainCell && !mainCell.Friends.Contains(friend))
                        mainCell.Friends.Add(friend);
                }
            }
        }

        /// <summary>
        /// Generates a 2D character array representation of the current game or puzzle board.
        /// </summary>
        /// <remarks>
        /// This method converts each numeric value in the board's cells to its corresponding character representation. 
        /// It assumes that the board's dimensions are square (Len x Len) and that the numeric values are within a range 
        /// that can be appropriately represented by a single character (0-9). This is useful for visualizing the board 
        /// state in a text-based format or for exporting the board's state to systems that require character input.
        /// </remarks>
        /// <returns>
        /// A 2D character array where each cell represents the numeric value of the corresponding cell in the 
        /// game or puzzle board, converted to a character. The dimensions of the array are equal to the board size (Len x Len).
        /// </returns>
        internal char[,] GetCharBoard()
        {
            char[,] charBoard = new char[Len, Len];

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    int number = CellsBoard[row, column].Value;
                    charBoard[row, column] = (char)('0' + number);
                }
            }

            return charBoard;
        }

        public override string ToString()
        {
            return Grider.ConvertGridToString(GetCharBoard());
        }

        /// <summary>
        /// Populates a collection with all cells from the board, ensuring each cell is accounted for further operations.
        /// </summary>
        /// <remarks>
        /// Iterates through each cell in the board, represented as a 2D array with dimensions of Len x Len, and adds each cell 
        /// to the <see cref="Cells"/>. This process ensures that all cells are included in a single, linear collection,
        /// facilitating operations that require iterating over each cell without navigating a 2D structure. This method is 
        /// critical for scenarios where a flat list of cells is preferable for processing tasks, such as validation, analysis, 
        /// or transformation of the board's state. Care should be taken to ensure the collection type used supports the 
        /// intended operations efficiently, whether they be iteration, search, or modification.
        /// </remarks>
        internal void CreateBoardCollection()
        {
            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    Cells.Add(CellsBoard[row, column]);
                }
            }
        }

        /// <summary>
        /// Generates a linear string representation of the Sudoku board, incrementing each cell's value up in the ASCII table for its character representation.
        /// </summary>
        /// <remarks>
        /// This method iterates through each cell on the board, row by row and column by column. For each cell, it converts the numeric value 
        /// by advancing up in the ASCII table to get the corresponding character representation. This transformation creates a linear string 
        /// that reflects the board's state, where each character directly corresponds to the cell's value adjusted in the ASCII sequence. 
        /// It is particularly useful for encoding the board's state in a compact and easily transmissible format. Note that this approach 
        /// assumes all cell values are such that when incremented in the ASCII table, they result in valid, intended characters.
        /// </remarks>
        /// <returns>
        /// A string representing the current state of the Sudoku board, with each cell's value adjusted and concatenated into a continuous sequence.
        /// </returns>
        internal string GetSudokoLinearString()
        {
            StringBuilder boardString = new StringBuilder();

            for (int row = 0; row < Len; row++)
            {
                for (int column = 0; column < Len; column++)
                {
                    Cell cell = CellsBoard[row, column];
                    boardString.Append((char)('0' + cell.Value));
                }
            }

            return boardString.ToString();
        }
    }
}
