using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project
{
    public class GridException : ArgumentException
    {
        public GridException() { }

        public GridException(string message)
            : base(message) { }

    }
}
