using System;
using System.IO;
using System.Linq;

namespace Week3
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
                    String str = sr.ReadLine();
                    int lengthA = int.Parse(str.Split(' ').ElementAt(0));
                    int lengthB = int.Parse(str.Split(' ').ElementAt(1));
                    int[] a = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    int[] b = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    a = Sort(a);
                    int[] mergedArray = MergeProducts(a, b);
                    long result = 0;
                    for (int i = 0; i < mergedArray.Length; i += 10)
                    {
                        result += mergedArray[i];
                    }
                    sw.Write(result);
                }
            }
        }
        public static int[] Sort(int[] array)
        {
            int[] temp = new int[40001];
            for (int i = 0; i < array.Length; i++)
            {
                temp[array[i]]++;
            }
            for (int i = 1; i < temp.Length; i++)
            {
                temp[i] += temp[i - 1];
            }
            int[] result = new int[array.Length];
            for (int i = array.Length - 1; i >= 0; i--)
            {
                result[--temp[array[i]]] = array[i];
            }
            return result;
        }
        public static int[] MergeProducts(int[] sortedArray, int[] b)
        {
            int[] temp = new int[b.Length];
            int[] tempIndexes = new int[b.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = b[i] * sortedArray[0];
                tempIndexes[i] = 1;
            }
            int[] result = new int[sortedArray.Length * b.Length];
            for (int i = 0; i < result.Length; i++)
            {
                int index = IndexOfMin(temp);
                result[i] = temp[index];
                if (tempIndexes[index] < sortedArray.Length)
                {
                    temp[index] = b[index] * sortedArray[tempIndexes[index]++];
                }
                else
                {
                    temp[index] = 1600000001;
                }
            }
            return result;
        }
        public static int IndexOfMin(int[] array)
        {
            int min = array[0];
            int index = 0;
            for (int i = 1; i < array.Length; i++)
            {
                if (array[i] < min)
                {
                    min = array[i];
                    index = i;
                }
            }
            return index;
        }
    }
}
