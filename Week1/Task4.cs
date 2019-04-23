using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1Lab1
{
    class Task4
    {
        static void Main(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
                {
                    int n = int.Parse(sr.ReadLine());
                    float[] array = sr.ReadLine().Split(' ').Select(x => float.Parse(x)).ToArray();
                    int[] indexes = new int[n];
                    for(int i = 0; i < n; i++)
                    {
                        indexes[i] = i + 1;
                    }
                    int j = 0;
                    float floatTmp = 0;
                    int intTmp = 0;
                    for (int i = 0; i < n; i++)
                    {
                        j = i - 1;
                        while (j >= 0 && array[j] > array[j + 1])
                        {
                            floatTmp = array[j];
                            array[j] = array[j + 1];
                            array[j + 1] = floatTmp;
                            intTmp = indexes[j];
                            indexes[j] = indexes[j + 1];
                            indexes[j + 1] = intTmp;
                            j--;
                        }
                    }
                    sw.Write(indexes[0] + " " + indexes[n / 2] + " " + indexes[n - 1]);
                }
            }
        }

    }
}
