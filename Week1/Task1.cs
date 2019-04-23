using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Lab1
{
    class Task1
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
                        int a = int.Parse(line.Split(new char[] { ' ' }).ElementAt(0));
                        int b = int.Parse(line.Split(new char[] { ' ' }).ElementAt(1));
                        sw.WriteLine(a + b);
                    }
                }
            }
        }
    }
}
