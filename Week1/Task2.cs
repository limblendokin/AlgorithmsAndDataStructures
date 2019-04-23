using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Lab1
{
    class Task2
    {
        static void Main(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        long a = long.Parse(line.Split(' ').ElementAt(0));
                        long b = long.Parse(line.Split(' ').ElementAt(1));
                        long result = a + b * b;
                        sw.WriteLine(result);
                    }
                }
            }
        }
    }
}
