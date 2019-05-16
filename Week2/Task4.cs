using System;
using System.IO;
using System.Linq;

namespace Week7
{
    public class MainClass
    {
        public static void Main()
        {
            string currentDir = Environment.CurrentDirectory;
            int n, k1, k2, a, b, c, a0, a1;
            int[] args;
            int[] array;
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                args = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                n = args[0];
                k1 = args[1] - 1;
                k2 = args[2] - 1;
                args = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                a = args[0];
                b = args[1];
                c = args[2];
                a0 = args[3];
                a1 = args[4];
            }

            array = ArrayGenerator(n, a, b, c, a0, a1);
            PartialQuickSort(array, 0, n - 1, k1, k2);
                
            using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
            {
                for(int i = k1; i <= k2; i++)
                {
                    sw.Write(array[i] + " ");
                }
            }
        }
        public static int[] ArrayGenerator(int n, int a, int b, int c, int a0, int a1)
        {
            int[] array = new int[n];
            array[0] = a0;
            array[1] = a1;
            for(int i = 2; i < n; i++)
            {
                array[i] = array[i - 2] * a + array[i - 1] * b + c;
            }
            return array;
        }
        private static int[] PartialQuickSort(int[] array, int left, int right, int k1, int k2)
        {
            int key = array[(left + right) / 2];
            int i = left;
            int j = right;
            do
            {
                while (array[i] < key)
                {
                    i++;
                }
                while (array[j] > key)
                {
                    j--;
                }
                if (i <= j)
                {
                    Swap(ref array[i], ref array[j]);
                    i++;
                    j--;
                }
            } while (i <= j);
            int index = array[(i + j) / 2] == key ? (i + j) / 2 : array[i] == key ? i : j;
            if(index < k1)
            {
                left = index;
            }
            else if(index > k2)
            {
                right = index;
            }
            if (j > left)
            {
                PartialQuickSort(array, left, j, k1, k2);
            }
            if (i < right)
            {
                PartialQuickSort(array, i, right, k1, k2);
            }
            return array;
        }
        private static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
    }
}