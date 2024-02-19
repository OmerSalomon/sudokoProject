## Sudoku Project
Hello dear Omegon, I hope you're having a great day. I've put a lot of effort into this project, and I hope it turns out to be good enough. Enjoy! :)

By the way, only now, two days before the submission, I found out that it should be spelled "Sudoku" and not "Sudoko." I think it will be best to keep it as "Sudoko" for sentimental purposes.

## Description
This Sudoku project aims to solve puzzles of various sizes and difficulty levels as swiftly as possible, while ensuring the code remains reusable and generic.

## Features
- Solve puzzles of various sizes and difficulty levels.
- Support input as a file and from the CLI.
- Identify invalid and unsolvable Sudoku boards.
- Teaches us about Master Oogway's mighty wisdom.

## Getting Started
Before you begin, ensure you have the following installed on your system:
- **.NET SDK**: The project is developed in C#, so you'll need the .NET SDK to compile and run the application. You can download it from the official .NET website.
- **An IDE or text editor**: While you can use any text editor, we recommend Visual Studio, Visual Studio Code, or Rider for the best development experience with C# projects.

1. Clone the repository to your local machine.
2. Open the solution in your preferred IDE.
3. Run the application.

### How Your Input Should Look Like?
Your input should be a single-line string that represents every cell on your Sudoku board. The length of the string should be a perfect square of a positive integer. The characters in your input should range from '0' upwards in the ASCII table, with the highest ASCII value character not exceeding '0' plus the dimension of your board (the square root of your string's length).

## Usage Instructions
This section guides you through the process of using the Sudoku Solver, whether you prefer to input your Sudoku puzzle via a file or directly through the Command Line Interface (CLI).

### Step 1: Choose Your Input Method
You have two options for inputting the Sudoku puzzle you wish to solve:
- **File**: Provide the path to a file containing the puzzle.
- **CLI**: Directly input the puzzle as a string through the CLI.

### Step 2: Input Your Sudoku Puzzle
- **For File Input**: Type the full path to your file that contains the Sudoku puzzle. Ensure the file format and puzzle representation meet the application's requirements.
- **For CLI Input**: Directly input your Sudoku puzzle as a string into the CLI. Make sure to follow the correct format for representing your puzzle.

### Step 3: Solve and View Results
After entering your Sudoku puzzle through your chosen method, press **Enter** to initiate the solving process. The application will process your input and:
- Display the solved Sudoku grid directly in your CLI.
- Provide the full path to an output file containing the solved puzzle for your convenience and future reference.

## Algorithm
### Initial Setup
The algorithm begins with two key pieces of information: `minMarkedCell`, which is the cell with the fewest allowed values (or "markers") indicating it has the highest probability of being correctly filled without causing a conflict, and `emptyCells`, the total number of cells that are yet to be filled on the Sudoku board. This setup is crucial for minimizing guesswork and potentially reducing the solution space the algorithm needs to explore.

### Base Case
The first step in the recursive function is to check for the base case, which is when there are no empty cells left (`emptyCells == 0`). This condition signifies that the puzzle has been successfully solved, and the function can return true, propagating this success back up through the recursive calls.

### Trying Possible Values
For the current `minMarkedCell`, the algorithm iterates through each of its possible values (markers). These markers represent the numbers that can potentially be placed in the cell without violating Sudoku rules based on the current state of the board.

### Recursive Exploration
Upon selecting a marker for `minMarkedCell`, the algorithm proceeds to remove this value from the list of possible values for all cells that are in the same row, column, or sqrt(len) x sqrt(len) subgrid (these cells are referred to as `minMarkedCell.Friends`). This action reflects the constraint propagation typical in Sudoku, where placing a number in a cell eliminates that number as a possibility for its related cells.

### Choosing the Next Cell
After updating the board state, the algorithm selects the next cell to work on, ideally the one with the fewest remaining markers post-update, to continue the solving process. This selection is made first among the friends of the current cell and, if not applicable, among all remaining empty cells on the board.

### Recursive Call
The algorithm then makes a recursive call with the newly chosen `minMarkedCell` and decrements `emptyCells` by one. If this call returns true, indicating that the subsequent placements led to a solution, the current state is correct, and the algorithm can propagate this success back up the call stack.

### Backtracking
If the recursive call does not lead to a solution, the algorithm "backtracks" by undoing the last placement.

## The process of writing the project
The process of creating the Sudoku project was an intricate and educational journey that required meticulous planning, coding, and problem-solving. Initially, the project began with a deep dive into understanding the rules and logic of Sudoku puzzles, ensuring a solid foundation upon which to build the solving algorithm. The next phase involved designing the architecture of the project, carefully structuring the code into manageable, modular components such as the board representation, the puzzle reader (capable of ingesting puzzles from files and the CLI), and the solver itself. Implementing the solver necessitated the application of algorithmic concepts, particularly focusing on efficient ways to navigate the complexities of Sudoku puzzles, which led to experimenting with various strategies, including backtracking and rule-based elimination. Throughout the development process, refining the user interface for ease of use and incorporating features like validating puzzles and offering hints were also key considerations. Debugging and testing formed a crucial part of the later stages, ensuring the solver worked accurately across a wide range of puzzle difficulties. 
