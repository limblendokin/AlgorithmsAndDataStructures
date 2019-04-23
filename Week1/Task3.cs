using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Lab1
{
    class Task3
    {
        static void Main(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
                {
                    int n = int.Parse(sr.ReadLine());
                    int[] array = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    int j = 0;
                    int tmp = 0;
                    for (int i = 0; i<n; i++)
                    {
                        j = i - 1;
                        while(j >= 0 && array[j] > array[j+1])
                        {
                            tmp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = tmp;
                            j--;
                        }
                        sw.Write(j + 2 + " ");
                    }
                    sw.WriteLine();
                    for(int i = 0; i < n; i++)
                    {
                        sw.Write(array[i] + " ");
                    }
                }
            }
        }
        
    }
}
