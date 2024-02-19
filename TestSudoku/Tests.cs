using sudoko_project;
using sudoko_project.Test;
using System;


namespace TestSudoku
{
    [TestClass]
    public class Tests
    {

        /// <summary>
        /// Tests the ability of the Solver to solve solvable Sudoku puzzles and validates the solutions.
        /// </summary>
        /// <remarks>
        /// This method reads a list of Sudoku puzzles from a file using the SudukoFileReader.ReadSudokosStringsFromFile method.
        /// Each puzzle is then solved using the Solver's Solve method. The solved puzzle is validated to ensure it meets
        /// the criteria for a valid Sudoku puzzle using the SudokoValidator's IsSudokoValid method.
        /// This test asserts that all solvable puzzles are correctly solved by the Solver, and that each solution is valid.
        /// </remarks>
        /// <exception cref="SudokoException">
        /// Throws a SudokoException if the Solver fails to solve a puzzle or if the solved puzzle does not pass validation.
        /// Note: The actual mechanism for throwing SudokoException should be implemented within the Solver or Validator
        /// classes, and this documentation assumes such an implementation is in place.
        /// </exception>
        [TestMethod]
        public void Solve_SolvableSudokos_SudokoException()
        {
            List<string> solvableSudokos = SudukoFileReader.ReadSudokosStringsFromFile(SudukoFileReader.SUDOKOS_FILE_RELATIVE_PATH);

            SudokoValidator validator = new SudokoValidator();
            Solver solver = new Solver();

            foreach (string sudoko in solvableSudokos)
            {
                string solvedSudoko = solver.Solve(sudoko);
                Assert.IsTrue(validator.IsSudokoValid(solvedSudoko));
            }     
            
        }

        /// <summary>
        /// Tests that the Solver correctly identifies and throws an exception for unsolvable Sudoku puzzles.
        /// </summary>
        /// <remarks>
        /// This method defines a list of Sudoku puzzles known to be unsolvable. Each puzzle is processed by the Solver's
        /// Solve method. The test validates that an UnsolvableSudokoException is thrown for each unsolvable puzzle,
        /// indicating the solver's correct handling of such cases.
        /// 
        /// The purpose of this test is to ensure the robustness of the Solver in recognizing puzzles that do not have a
        /// valid solution and to verify that it appropriately signals this situation through exceptions, rather than
        /// failing silently or returning incorrect or incomplete solutions.
        /// </remarks>
        /// <exception cref="UnsolvableSudokoException">
        /// Expects an UnsolvableSudokoException to be thrown for each unsolvable Sudoku puzzle. This exception should
        /// indicate that the Solver has correctly identified the puzzle as unsolvable and is unable to proceed with
        /// finding a solution.
        /// </exception>
        [TestMethod]
        public void Solve_UnsolveablesSudokos_ThrowsUnsolvableSudokoException()
        {
            List<string> unsolvableSudokos = new List<string>()
            {
                "100000000000100000000000005000000100000000000000000000000000000000000010000000000"
            };

            Solver solver = new Solver();
            foreach (string sudoko in unsolvableSudokos)
                Assert.ThrowsException<UnsolvableSudokoException>(() => solver.Solve(sudoko));

        }

        /// <summary>
        /// Tests that the Solver correctly identifies and throws an exception for invalid Sudoku puzzles.
        /// </summary>
        /// <remarks>
        /// This method tests the Solver's ability to handle invalid Sudoku puzzles by ensuring that an 
        /// InvalidSudokoException is thrown for each case. Invalid puzzles may include those with incorrect lengths,
        /// illegal characters, or configurations that violate Sudoku rules from the outset.
        ///
        /// The list of invalidSudokos contains examples of such invalid puzzles, including but not limited to,
        /// puzzles with too few characters, illegal digits, or those that break the basic rules of Sudoku in other ways.
        /// This test ensures that the solver is robust enough to recognize these invalid inputs and alert the user
        /// by throwing a specific exception, rather than proceeding with an attempt to solve or failing silently.
        /// </remarks>
        /// <exception cref="InvalidSudokoException">
        /// Expects an InvalidSudokoException to be thrown for each invalid Sudoku puzzle. This exception indicates
        /// that the Solver has correctly identified the puzzle as invalid due to structural issues, format errors,
        /// or rule violations, and is thus unable to attempt a solution.
        /// </exception>
        [TestMethod]
        public void Solve_InvalidSudokos_ThrowsUnsolvableSudokoException()
        {
            List<string> invalidSudokos = new List<string>()
            {
                "1100",
                "000000000000000000000000000000009990000000000000000000000000000000000000000000000"
            };

            Solver solver = new Solver();
            foreach (string sudoko in invalidSudokos)
                Assert.ThrowsException<InvalidSudokoException>(() => solver.Solve(sudoko));

        }
    }
}