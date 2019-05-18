using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1
{
    // В данной задаче требуется вычислить сумму двух заданных чисел.
    class Task1
    {
        static void Main(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();

            string[] arguments;
            int a, b, result;

            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                arguments = sr.ReadLine().Split(new char[] { ' ' }).ToArray();
            }

            a = int.Parse(arguments[0]);
            b = int.Parse(arguments[1]);
            result = a + b;

            using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
            {
                sw.WriteLine(result);
            }
        }
    }
}
