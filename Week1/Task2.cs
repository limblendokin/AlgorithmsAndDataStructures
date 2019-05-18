using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Lab1
{
    // В данной задаче требуется вычислить значение выражения a+b^2.
    class Task2
    {
        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string[] arguments;
            long a, b, result;

            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                arguments = sr.ReadLine().Split(new char[] { ' ' }).ToArray();
            }

            a = long.Parse(arguments[0]);
            b = long.Parse(arguments[1]);
            result = a + b * b;

            using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
            {
                sw.WriteLine(result);
            }
        }
    }
}
