using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using sudoko_project;
using System.Text;

namespace sudoko_project.UnitTests
{


    [TestClass]
    public class SudokoTester
    {
        [TestMethod]
        public void TestMethod()
        {
            string sudoko = "010002300405000060060000700080004002000000000600100090003000080070000205009600010";
            Solver solver = new Solver();
            string solvedSudoko = solver.Solve(sudoko);
            SudokoValidator sudokoValidator = new SudokoValidator();

            bool isSudokoSolutionValid = sudokoValidator.validateSudokoSolution(sudoko);

            Assert.IsTrue(isSudokoSolutionValid);
        }
    }



}