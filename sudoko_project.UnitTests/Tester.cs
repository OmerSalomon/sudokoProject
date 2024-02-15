using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sudoko_project;


namespace sudoko_project.tests
{
    internal class Tester
    {
        [TestClass]
        public class YourTestClass
        {
            [TestMethod]
            public void TestMethod1()
            {
                Solver solver = new Solver();

                string sudoko = "010002300405000060060000700080004002000000000600100090003000080070000205009600010";
                string predtictedSolution = "917862354425713869368459721581394672792586143634127598143275986876941235259638417";

                string solvedSudoko = solver.Solve(sudoko);

                Assert.AreEqual(solvedSudoko, predtictedSolution);
            }

            public void TestMethod2()
            {
                Assert.IsTrue(true);
            }
        }
    }
}
