using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week7
{
    class Task1
    {
        static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            int n;
            int[][] nodes;
            AVLTree tree = new AVLTree();
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                n = int.Parse(sr.ReadLine());
                nodes = new int[n][];
                for (int i = 0; i < n; i++)
                {
                    nodes[i] = sr.ReadLine().Split(' ').Select(str => int.Parse(str)).ToArray();
                }
                Stack<int> indexes = new Stack<int>(n);
                AVLTree.Node[] nodeLinks = new AVLTree.Node[n];
                nodeLinks[0] = tree.Insert(nodes[0][0]);
                indexes.Push(0);
                while (!indexes.IsEmpty())
                {
                    int currentIndex = indexes.Pop();
                    int leftIndex = nodes[currentIndex][1] - 1;
                    int rightIndex = nodes[currentIndex][2] - 1;
                    if (leftIndex != -1)
                    {
                        int leftKey = nodes[leftIndex][0];
                        indexes.Push(leftIndex);
                        nodeLinks[leftIndex] = tree.Insert(leftKey, nodeLinks[currentIndex]);
                    }
                    if (rightIndex != -1)
                    {
                        int rightKey = nodes[rightIndex][0];
                        indexes.Push(rightIndex);
                        nodeLinks[rightIndex] = tree.Insert(rightKey, nodeLinks[currentIndex]);
                    }
                }
                tree.CalculateHeights();
                using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
                {
                    for (int i = 0; i < n; i++)
                    {
                        int balance = tree.GetBalance(nodeLinks[i]);
                        sw.WriteLine(balance);
                    }
                }
            }
        }
    }

    class AVLTree
    {
        public class Node
        {
            public int Key { get; set; }
            public int Height { get; set; } = 0;
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
            public Node(int key)
            {
                Key = key;
            }
        }
        private Node root;
        public int Count;

        public Node Insert(int key)
        {
            root = new Node(key);
            Count++;
            return root;
        }
        public Node Insert(int key, Node parent)
        {
            Count++;
            if (key > parent.Key)
            {
                parent.RightChild = new Node(key);
                return parent.RightChild;
            }
            else
            {
                parent.LeftChild = new Node(key);
                return parent.LeftChild;
            }
        }
        public int GetBalance(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.RightChild) - GetHeight(node.LeftChild);
        }
        public int GetHeight(Node node)
        {
            if (node == null)
                return -1;
            int leftSubtreeHeight = -1;
            int rightSubtreeHeight = -1;
            if (node.LeftChild != null)
                leftSubtreeHeight = node.LeftChild.Height;
            if (node.RightChild != null)
                rightSubtreeHeight = node.RightChild.Height;
            node.Height = Math.Max(leftSubtreeHeight, rightSubtreeHeight) + 1;
            return node.Height;
        }
        /// <summary>
        /// Calculates heights of entire tree
        /// </summary>
        public void CalculateHeights()
        {
            int leftIndex = 0;
            int levelCount = 1;
            Node[] nodes = new Node[Count];
            nodes[0] = root;
            while (levelCount != 0)
            {
                int rightIndex = leftIndex + levelCount;
                levelCount = 0;
                for (int i = leftIndex; i < rightIndex; i++)
                {
                    if (nodes[i].LeftChild != null)
                    {
                        nodes[rightIndex + levelCount++] = nodes[i].LeftChild;
                    }
                    if (nodes[i].RightChild != null)
                    {
                        nodes[rightIndex + levelCount++] = nodes[i].RightChild;
                    }
                }
                leftIndex = rightIndex;
            }
            for (int i = Count - 1; i >= 0; i--)
            {
                GetHeight(nodes[i]);
            }
        }
    }
    class Stack<T>
    {
        private T[] stack;
        private int pointer = 0;
        public Stack(int n)
        {
            stack = new T[n];
        }
        public Stack()
        {
            stack = new T[1000000];
        }
        public T Pop()
        {
            return stack[--pointer];
        }
        public T Peek()
        {
            return stack[pointer];
        }
        public void Push(T value)
        {
            stack[pointer++] = value;
        }
        public bool IsEmpty()
        {
            return pointer <= 0;
        }
    }
}
