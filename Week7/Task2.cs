using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week7
{
    class Task2
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
                tree.Root = tree.Balance(tree.Root);
                string[] treeRepresentation = tree.TreeRepresentation();
                using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
                {
                    sw.WriteLine(treeRepresentation.Length);
                    foreach (string s in treeRepresentation)
                    {
                        sw.WriteLine(s);
                    }
                }
            }
        }
    }

    class AVLTree
    {
        public class Node
        {
            public int Key { get; private set; }
            public int Height { get; set; } = 0;
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
            public Node(int key)
            {
                Key = key;
            }
        }
        public Node Root { get; set; }
        public int Count;

        public Node Insert(int key)
        {
            Root = new Node(key);
            Count++;
            return Root;
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
        public string[] TreeRepresentation()
        {
            string[] nodes = new string[Count];
            int currentNodeIndex = 0;
            int currentLeftChildIndex;
            int currentRightChildIndex;
            int lineCount = 0;
            Node currentNode = Root;
            Stack<int> leftChildIndexes = new Stack<int>(Count);
            Stack<Node> leftChilds = new Stack<Node>(Count);
            Stack<int> rightChildIndexes = new Stack<int>(Count);
            Stack<Node> rightChilds = new Stack<Node>(Count);
            while (currentNode != null)
            {
                if (currentNode.LeftChild != null)
                {
                    leftChilds.Push(currentNode.LeftChild);
                    currentLeftChildIndex = ++lineCount;
                    leftChildIndexes.Push(currentLeftChildIndex++);
                }
                else
                {
                    currentLeftChildIndex = 0;
                }
                if (currentNode.RightChild != null)
                {
                    rightChilds.Push(currentNode.RightChild);
                    currentRightChildIndex = ++lineCount;
                    rightChildIndexes.Push(currentRightChildIndex++);
                }
                else
                {
                    currentRightChildIndex = 0;
                }
                nodes[currentNodeIndex] = currentNode.Key + " " + currentLeftChildIndex + " " + currentRightChildIndex;
                if (!leftChilds.IsEmpty())
                {
                    currentNode = leftChilds.Pop();
                    currentNodeIndex = leftChildIndexes.Pop();
                }
                else if (!rightChilds.IsEmpty())
                {
                    currentNode = rightChilds.Pop();
                    currentNodeIndex = rightChildIndexes.Pop();
                }
                else
                {
                    currentNode = null;
                }
            }
            return nodes;
        }
        public Node Balance(Node node)
        {
            if (GetBalance(node) == 2)
            {
                if (GetBalance(node.RightChild) == -1)
                {
                    node.RightChild = RightTurn(node.RightChild);
                }
                return LeftTurn(node);
            }
            return node;
        }
        private Node RightTurn(Node node)
        {
            Node leftChild = node.LeftChild;
            Node t2 = leftChild.RightChild;
            node.LeftChild = t2;
            leftChild.RightChild = node;
            return leftChild;
        }
        private Node LeftTurn(Node node)
        {
            Node rightChild = node.RightChild;
            Node t2 = rightChild.LeftChild;
            node.RightChild = t2;
            rightChild.LeftChild = node;
            return rightChild;
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
        public void CalculateHeights()
        {
            int leftIndex = 0;
            int levelCount = 1;
            Node[] nodes = new Node[Count];
            nodes[0] = Root;
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
