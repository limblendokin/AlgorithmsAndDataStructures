using System;
using System.IO;

namespace Week7
{
    public class MainClass
    {
        public static int comparisonCount = 0;
        public static void Main()
        {
            string currentDir = Environment.CurrentDirectory;
            int n;
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                n = int.Parse(sr.ReadLine());
            }
            int[] array = AntiQuickSort(n);
            using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
            {
                for(int i = 0; i < n; i++)
                {
                    sw.Write(array[i] + " ");
                }
            }
        }
        public static int[] AntiQuickSort(int n)
        {
            int[] array = new int[n];
            for(int i = 0; i < n; i++)
            {
                array[i] = i + 1;
            }
            int left = 0;
            for(int right = 2; right < n; right++)
            {
                int index = (left + right) / 2;
                Swap(ref array[index], ref array[right]);
            }

            return array;
        }
        public static int[] QuickSort(int[] array)
        {
            return QuickSort(array, 0, array.Length - 1);
        }
        private static int[] QuickSort(int[] array, int left, int right)
        {
            int key = array[(left + right) / 2];
            int i = left;
            int j = right;
            do
            {
                comparisonCount++;
                while (array[i] < key)
                {
                    i++;
                    comparisonCount++;
                }
                comparisonCount++;
                while (array[j] > key)
                {
                    j--;
                    comparisonCount++;
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