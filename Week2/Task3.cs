using System;
using System.IO;
using System.Linq;

namespace Week1Lab1
{
    public class Task1
    {
        public static int comparisonCount;
        static void Main(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            int n;
            int[] array = new int[] { 1, 3, 2 };
            array = QuickSort(array, 0, 2);
            Console.WriteLine(comparisonCount);
            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                n = int.Parse(sr.ReadLine());
                array = AntiQuickSort(n);
            }
            using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
            {
                for (int i = 0; i < n; i++)
                {
                    sw.Write(array[i]);
                }
            }
        }
        public static int[] AntiQuickSort(int n)
        {
            int[] array = new int[n];
            AntiQuickSort(array, 0, n - 1, 1, n);
            return array;
        }
        public static int[] AntiQuickSort(int[] array, int left, int right, int min, int max)
        {
            int mid = (left + right) / 2;
            if (right - mid > mid - left)
            {
                array[mid] = min++;
            }
            else
            {
                array[mid] = max--;
            }
            return array;
        }
        public static void Swap(ref int a, ref int b)
        {
            int tmp = a;
            a = b;
            b = tmp;
        }
        public static int[] QuickSort(int[] array, int left, int right)
        {
            int key = array[(left + right) / 2];
            int i = left;
            int j = right;
            do
            {
                for (; array[i] < key; comparisonCount++)
                {
                    i++;
                }
                for (; array[j] > key; comparisonCount++)
                {
                    j--;
                }
                if (i <= j)
                {
                    int tmp = array[i];
                    array[i] = array[j];
                    array[j] = tmp;
                    i++;
                    j--;
                }
            } while (i <= j);
            if (left < j)
            {
                array = QuickSort(array, left, j);
            }
            if (i < right)
            {
                array = QuickSort(array, i, right);
            }
            return array;
        }
    }
}
