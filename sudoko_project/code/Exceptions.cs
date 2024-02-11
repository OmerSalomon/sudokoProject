using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    public class BoardException : ArgumentException
    {
        public BoardException() { }

        public BoardException(string message)
            : base(message) { }

    }

    public class ReaderException : ArgumentException
    {
        public ReaderException() { }

        public ReaderException(string message)
            : base(message) { }
    }

    public class SudokoException : ArgumentException
    {
        public SudokoException() { }

        public SudokoException(string message)
            : base(message) { }
    }

}