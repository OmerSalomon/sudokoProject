using sudoko_project;
using sudoko_project.Test;
using System;


namespace TestSudoku
{
    [TestClass]
    public class Tests
    {

        /// <summary>
        /// Check if the solver successfully solves every solvable sudoku.
        /// </summary>
        [TestMethod]
        public void Solve_SolvableSudokos_SudokoException()
        {
            List<string> solvableSudokos = SudukoFileReader.ReadSudokosStringsFromFile();

            SudokoValidator validator = new SudokoValidator();
            Solver solver = new Solver();

            foreach (string sudoko in solvableSudokos)
            {
                string solvedSudoko = solver.Solve(sudoko);
                Assert.IsTrue(validator.IsSudokoValid(solvedSudoko));
            }     
            
        }

        /// <summary>
        /// Check if the solver successfully detects unsolvable sudokus.
        /// </summary>
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
        /// Check if the solver successfully detects invalid sudokus.
        /// </summary>
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