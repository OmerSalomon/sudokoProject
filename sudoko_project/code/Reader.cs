using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sudoko_project.code
{
    abstract class Reader
    {
        public abstract string Read();

        protected void ValidateString(string sudokoString)
        {
            double stringLenSquareRoot = Math.Sqrt(sudokoString.Length);
            if (stringLenSquareRoot != (int)stringLenSquareRoot)
                throw new ReaderException("String len is not a square root of a whole number");

            foreach (char c in sudokoString)
                if (c - '0' > stringLenSquareRoot)
                    throw new ReaderException($"{c} ascii value is not between {Convert.ToByte(c)} and {Convert.ToByte('0') + stringLenSquareRoot + 1}");
        }
    }

    class TextFileReader : Reader
    {
        public override string Read()
        {
            string content;

            Console.WriteLine("Enter filePath");
            string filePath = Console.ReadLine();
            content = System.IO.File.ReadAllText(filePath);
            ValidateString(content);

            return content;
        }
    }

    class CLIReader : Reader
    {
        public override string Read()
        {
            Console.WriteLine("Enter String");
          
            string str = Console.ReadLine();
            ValidateString(str);

            return str;
        }
    }
}
