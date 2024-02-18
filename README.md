## Sudoko Project
Hello dear Omegon, I hope you're having a great day. 
I've put a lot of effort into this project, and I hope it turns out to be good enough. Enjoy! :)

## Description
This Sudoku project aims to solve puzzles of various sizes and difficulty levels as swiftly as possible, 
while ensuring the code remains reusable and generic.

## Features
* solve puzzles of various sizes and difficulty levels.
* support input as file and from the CLI.
* Identify invalid and unsolvable Sudoku boards.
* Teaches us about Master Ogwe's mighty wisdom.

## Getting Started
Before you begin, ensure you have the following installed on your system:
* .NET SDK: The project is developed in C#, so you'll need the .NET SDK to compile and run the application. You can download it from the official .NET website.
* An IDE or text editor: While you can use any text editor, we recommend Visual Studio, Visual Studio Code, or Rider for the best development experience with C# projects.

1) Clone the repository to your local machine
2) Open the solution in preferd IDE.
3) Run the application.

* How your input should look like?
  Your input should be a single-line string that represents every cell on your Sudoku board.
  The length of the string should be a perfect square of a positive integer.
  The characters in your input should range from '0' upwards in the ASCII table,
  with the highest ASCII value character not exceeding '0'
  plus the dimension of your board (the square root of your string's length).
  
## Usage 
1) choose input type:
-file
-CLI

2)
For a file write the full path for your file
For CLI write your string in the CLI.

3) press enter:
   The solved sudoko grid will be printed on your CLI and full path to an output file will be printed too.
