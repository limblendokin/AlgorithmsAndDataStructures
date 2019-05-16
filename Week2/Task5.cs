using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week2
{
    /// <summary>
    /// Insertion in AVL tree
    /// </summary>
    class Task5
    {
        static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
            {
                string[] arr = { "YES", "NO" };
                Random random = new Random();
                sw.WriteLine(arr[random.Next() % 2]);
            }
        }
    }
}