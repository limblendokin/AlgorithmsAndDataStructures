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
                BinaryTree binaryTree = new BinaryTree(array);
                int numberOfSearches = int.Parse(sr.ReadLine());
                int[] searchValues = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                using(StreamWriter sw = new StreamWriter(currentDirectory + @"/output.txt"))
                {
                    for(int i = 0; i < numberOfSearches; i++)
                    {
                        Tuple tuple = binaryTree.Search(searchValues[i]);
                        sw.WriteLine(tuple.First + " " + tuple.Second);
                    }
                }
            }
        }
    }
    class BinaryTree
    {
        public Node Root { get; set; }
        public int Count { get; private set; }
        public int Length { get; private set; }
        public BinaryTree(int n)
        {
            Length = n;
        }
        public BinaryTree(int[] array)
        {
            Length = array.Length;
            for (int i = 0; i < Length; i++)
            {
                Insert(i, array[i]);
            }
        }
        public void Insert(int index, int key)
        {
            if (Root == null)
            {
                Root = new Node(index, key, null);
                return;
            }
            Node current = Root;
            while (true)
            {
                if (current.Key > key)
                {
                    if (current.LeftChild != null)
                    {
                        current = current.LeftChild;
                    }
                    else
                    {
                        Count++;
                        current.LeftChild = new Node(index, key, current);
                        return;
                    }
                }
                else
                {
                    if (current.RightChild != null)
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        Count++;
                        current.RightChild = new Node(index, key, current);
                        return;
                    }
                }
            }
        }
        public Tuple Search(int key)
        {
            if (Root == null)
            {
                return new Tuple(-1, -1);
            }
            Node current = Root;
            int first;
            int second;
            while (true)
            {
                if (current.Key == key)
                {
                    first = current.Index + 1;
                    second = FindLast(current, key) + 1;
                    return new Tuple(first, second);
                }
                else if (key < current.Key)
                {
                    if (current.LeftChild == null)
                    {
                        return new Tuple(-1, -1);
                    }
                    else
                    {
                        current = current.LeftChild;
                    }
                }
                else
                {
                    if (current.RightChild == null)
                    {
                        return new Tuple(-1, -1);
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
            }
        }
        private int FindLast(Node current, int key)
        {
            int index = current.Index;
            while (current.RightChild != null && current.RightChild.Key == key)
            {
                current = current.RightChild;
                index = current.Index;
            }
            if (current.Key > key)
            {
                while (current.LeftChild != null && current.LeftChild.Key > key)
                {
                    current = current.LeftChild;
                }
                if (current.LeftChild.Key == key)
                {
                    return FindLast(current.LeftChild, key);
                }
                else
                {
                    return index;
                }
            }
            else
            {
                return index;
            }

        }
    }
    class Node
    {
        public int Index { get; private set; }
        public int Key { get; private set; }
        public Node Parent { get; set; }
        public Node LeftChild { get; set; }
        public Node RightChild { get; set; }
        public Node(int index, int key, Node parent)
        {
            Index = index;
            Key = key;
            Parent = parent;
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