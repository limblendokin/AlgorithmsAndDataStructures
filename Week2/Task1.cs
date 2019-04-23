using System;
using System.IO;
using System.Linq;

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
                    int n = int.Parse(sr.ReadLine());
                    int[] array = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                    int[] result = MergeSort(array, 0, array.Length - 1, sw);
                    for (int i = 0; i < result.Length; i++)
                    {
                        sw.Write(result[i] + " ");
                    }
                }
            }
        }
        public static int[] MergeSort(int[] array, int leftIndex, int rightIndex, StreamWriter sw)
        {
            int n = array.Length;
            if (n == 1)
            {
                return array;
            }
            int leftLength = n / 2;
            int[] leftArray = new int[leftLength];
            Array.Copy(array, 0, leftArray, 0, leftLength);
            leftArray = MergeSort(leftArray, leftIndex, leftIndex + leftLength - 1, sw);
            int rightLength = n - leftLength;
            int[] rightArray = new int[rightLength];
            Array.Copy(array, leftLength, rightArray, 0, rightLength);
            rightArray = MergeSort(rightArray, leftIndex + leftLength, rightIndex, sw);
            array = Merge(leftArray, rightArray);
            sw.WriteLine((leftIndex + 1) + " " + (rightIndex + 1) + " " + array.First() + " " + array.Last());
            return array;
        }

        private static int[] Merge(int[] leftArray, int[] rightArray)
        {
            int[] array = new int[leftArray.Length + rightArray.Length];
            int j = 0;
            int k = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (k == rightArray.Length || (j < leftArray.Length && leftArray[j] < rightArray[k]))
                {
                    array[i] = leftArray[j++];
                }
                else
                {
                    array[i] = rightArray[k++];
                }
            }
            return array;
        }
    }
}
