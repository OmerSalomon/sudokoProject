using sudoko_project;
using System;


namespace TestSudoku
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            List<string> solvableSudokos = new List<string>();
            solvableSudokos.Add("000000000000000000000000000000000000000000000000000000000000000000000000000000000");
            solvableSudokos.Add("10023400<06000700080007003009:6;0<00:0010=0;00>0300?200>000900<0=000800:0<201?000;76000@000?005=000:05?0040800;0@0059<00100000800200000=00<580030=00?0300>80@000580010002000=9?000<406@0=00700050300<0006004;00@0700@050>0010020;1?900=002000>000>000;0200=3500<");
            solvableSudokos.Add("0010");

            SudokoValidator validator = new SudokoValidator();
            Solver solver = new Solver();
            bool isALLsudokosValid = true;

            foreach (string sudoko in solvableSudokos)
            {
                string solvedSudoko = solver.Solve(sudoko);
                bool isValid = validator.IsSudokoValid(solvedSudoko);
                if (!isValid)
                    isALLsudokosValid = false;
            }     
            Assert.IsTrue(isALLsudokosValid);
        }
    }
}