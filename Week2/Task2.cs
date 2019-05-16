using System;
using System.IO;
using System.Linq;

namespace Week2
{
    public class Task2
    {
        public static long inversionsCount;
        static void Main(string[] args)
        {
            String currentDirectory = Directory.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(currentDirectory + @"\input.txt"))
            {
                using (StreamWriter sw = new StreamWriter(currentDirectory + @"\output.txt"))
                {
                    int n = int.Parse(sr.ReadLine());
                    int[] array = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                    int[] result = MergeSort(array, 0, array.Length - 1);
                    sw.WriteLine(inversionsCount);
                }
            }
        }
        public static int[] MergeSort(int[] array, int leftIndex, int rightIndex)
        {
            int n = array.Length;
            if (n == 1)
            {
                return array;
            }
            int leftLength = n / 2;
            int[] leftArray = new int[leftLength];
            Array.Copy(array, 0, leftArray, 0, leftLength);
            leftArray = MergeSort(leftArray, leftIndex, leftIndex + leftLength - 1);

            int rightLength = n - leftLength;
            int[] rightArray = new int[rightLength];
            Array.Copy(array, leftLength, rightArray, 0, rightLength);
            rightArray = MergeSort(rightArray, leftIndex + leftLength, rightIndex);

            array = Merge(leftArray, rightArray);

            return array;
        }

        private static int[] Merge(int[] leftArray, int[] rightArray)
        {
            int[] array = new int[leftArray.Length + rightArray.Length];
            int j = 0;
            int k = 0;
            for (int i = 0; i < array.Length; i++)
            {
                if (k == rightArray.Length || (j < leftArray.Length && leftArray[j] <= rightArray[k]))
                {
                    array[i] = leftArray[j++];
                }
                else
                {
                    array[i] = rightArray[k++];
                    inversionsCount += leftArray.Length - j;
                }
            }
            return array;
        }
    }
}
