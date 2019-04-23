using System;
using System.IO;
using System.Linq;

namespace Week6
{
    public class Task1
    {
        public static void Main(string[] args)
        {
            string currentDirectory = Environment.CurrentDirectory;
            using(StreamReader sr = new StreamReader(currentDirectory + @"/input.txt"))
            {
                int n = int.Parse(sr.ReadLine());
                int[] array = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                int numberOfSearches = int.Parse(sr.ReadLine());
                int[] searchValues = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                using(StreamWriter sw = new StreamWriter(currentDirectory + @"/output.txt"))
                {
                    for(int i = 0; i < numberOfSearches; i++)
                    {
                        Tuple tuple = BinarySearch.Search(array, searchValues[i]);
                        int first = tuple.First + 1;
                        int second = tuple.Second + 1;
                        sw.WriteLine(first + " " + second);
                    }
                }
            }
        }
    }
    class BinarySearch
    {
        public static Tuple Search(int[] array, int key)
        {
            int leftIndex = -1;
            int rightIndex = array.Length;
            int first = -2;
            int second = -2;
            int c = 0; 
            while (leftIndex + 1 < rightIndex)
            {
                c = (leftIndex + rightIndex) / 2;
                if(key <= array[c])
                {
                    rightIndex = c;
                }
                else
                {
                    leftIndex = c;
                }
            }
            if (rightIndex < array.Length && array[rightIndex] == key)
            {
                first = rightIndex;
            }
            leftIndex = -1;
            rightIndex = array.Length;
            while (leftIndex + 1 < rightIndex)
            {
                c = (leftIndex + rightIndex) / 2;
                if (key < array[c])
                {
                    rightIndex = c;
                }
                else
                {
                    leftIndex = c;
                }
            }
            if (leftIndex >= 0 && array[leftIndex] == key)
            {
                second = leftIndex;
            }
            return new Tuple(first, second);
        }
    }
    class Tuple
    {
        public int First { get; private set; }
        public int Second { get; private set; }
        public Tuple(int first, int second)
        {
            First = first;
            Second = second;
        }
    }


}