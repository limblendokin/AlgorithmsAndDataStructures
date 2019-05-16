using System;
using System.IO;
using System.Linq;

namespace Week2
{
    public class Task5
    {
        public static void Main()
        {
            string currentDir = Environment.CurrentDirectory;
            int n, k, m;
            int[] args;
            int[] array;
            int[][] matrix;
            bool isAscending = true;
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                args = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                n = args[0];
                k = args[1];
                array = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
            }
            if (k != 1)
            {
                matrix = new int[k][];
                for (int i = 0; i < k; i++)
                {
                    m = (int)Math.Ceiling((double)(n-i) / k);
                    matrix[i] = new int[m];
                }
                for (int s = 0, i = 0, j = 0; s < n; s++, i++)
                {
                    if (i >= k)
                    {
                        j++;
                        i = 0;
                    }
                    matrix[i][j] = array[s];
                }
                for (int i = 0; i < k; i++)
                {
                    QuickSort(matrix[i], 0, matrix[i].Length - 1);
                }

                int x = 0;
                int y = 0;
                int prev = matrix[x][y];

                while (isAscending)
                {
                    x++;
                    if(x >= k)
                    {
                        x = 0;
                        y++;
                    }
                    if(y >= matrix[x].Length)
                    {
                        break;
                    }
                    if (prev > matrix[x][y])
                    {
                        isAscending = false;
                    }
                    prev = matrix[x][y];
                }
            }
            using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
            {
                sw.WriteLine(isAscending ? "YES" : "NO");
            }
        }
        public static int[] ArrayGenerator(int n)
        {
            int[] array = new int[n];
            Random r = new Random();
            for(int i = 0; i < n; i++)
            {
                array[i] = r.Next();
            }
            return array;
        }
        private static int[] QuickSort(int[] array, int left, int right)
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
            
            if (j > left)
            {
                QuickSort(array, left, j);
            }
            if (i < right)
            {
                QuickSort(array, i, right);
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