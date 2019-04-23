using System;
using System.IO;
using System.Linq;

namespace Week1Lab1
{
    class Task5
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
                    int tmp = 0;
                    for (int i = 0; i < n; i++)
                    {
                        int minIndex = i;
                        for (int j = i; j < n; j++)
                        {
                            if (array[minIndex] > array[j])
                            {
                                minIndex = j;
                            }
                        }
                        if (i != minIndex)
                        {
                            tmp = array[i];
                            array[i] = array[minIndex];
                            array[minIndex] = tmp;
                            sw.WriteLine("Swap elements at indices {0} and {1}.", i + 1, minIndex + 1);
                        }
                    }
                    sw.WriteLine("No more swaps needed.");
                    for (int i = 0; i < n; i++)
                    {
                        sw.Write(array[i] + " ");
                    }
                }
            }
        }
    }
}