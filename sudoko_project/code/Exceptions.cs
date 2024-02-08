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
}