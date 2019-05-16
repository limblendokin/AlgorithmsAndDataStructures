using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week7
{
    /// <summary>
    /// Insertion in AVL tree
    /// </summary>
    class Task3
    {
        static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            int n;
            int[][] nodes;
            int key;
            AVLTree tree = new AVLTree();
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                n = int.Parse(sr.ReadLine());
                if (n > 0)
                {
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
                }
                key = int.Parse(sr.ReadLine());
                tree.Insert(key);
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
            public int Key { get; set; }
            public int Height { get; set; } = 0;
            public Node LeftChild { get; set; }
            public Node RightChild { get; set; }
            public Node(int key)
            {
                Key = key;
            }
        }
        /// <summary>
        /// Root node
        /// </summary>
        public Node Root { get; private set; }
        /// <summary>
        /// Total node count
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// Inserts node with a key into the tree
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Node Insert(int key)
        {
            if (Root == null)
            {
                Root = new Node(key);
                Count++;
                return Root;
            }
            Node currentNode = Root;
            Node addedNode = null;
            Stack<Node> stack = new Stack<Node>();
            while (addedNode == null)
            {
                stack.Push(currentNode);
                if (key > currentNode.Key)
                {
                    if (currentNode.RightChild != null)
                    {
                        currentNode = currentNode.RightChild;
                    }
                    else
                    {
                        currentNode.RightChild = new Node(key);
                        Count++;
                        addedNode = currentNode.RightChild;
                    }
                }
                else
                {
                    if (currentNode.LeftChild != null)
                    {
                        currentNode = currentNode.LeftChild;
                    }
                    else
                    {
                        currentNode.LeftChild = new Node(key);
                        Count++;
                        addedNode = currentNode.LeftChild;
                    }
                }
            }
            while (!stack.IsEmpty())
            {
                currentNode = stack.Pop();
                GetHeight(currentNode);
                if (key > currentNode.Key)
                {
                    currentNode.RightChild = Balance(currentNode.RightChild);
                }
                else
                {
                    currentNode.LeftChild = Balance(currentNode.LeftChild);
                }
            }
            Root = Balance(Root);
            return addedNode;
        }
        /// <summary>
        /// Adds node with key to parent node
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public Node Insert(int key, Node parent)
        {
            if (key > parent.Key)
            {
                parent.RightChild = new Node(key);
                Count++;
                return parent.RightChild;
            }
            else
            {
                parent.LeftChild = new Node(key);
                Count++;
                return parent.LeftChild;
            }
        }
        
        /// <summary>
        /// Calculates heights of entire tree
        /// </summary>
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
        /// <summary>
        /// Balances the tree, if needed, and returns top node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node Balance(Node node)
        {
            if (GetBalance(node) == 2)
            {
                if (GetBalance(node.RightChild) == -1)
                {
                    node.RightChild = RightTurn(node.RightChild);
                }
                return LeftTurn(node);
            }
            if (GetBalance(node) == -2)
            {
                if (GetBalance(node.LeftChild) == 1)
                {
                    node.LeftChild = LeftTurn(node.LeftChild);
                }
                return RightTurn(node);
            }
            return node;
        }
        /// <summary>
        /// Calculates balance factor of node, if heights calculated correctly
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public int GetBalance(Node node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.RightChild) - GetHeight(node.LeftChild);
        }
        /// <summary>
        /// Performs rigth turn around node and returns top node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node RightTurn(Node node)
        {
            Node leftChild = node.LeftChild;
            Node t2 = leftChild.RightChild;
            node.LeftChild = t2;
            leftChild.RightChild = node;
            GetHeight(node);
            GetHeight(t2);
            return leftChild;
        }
        /// <summary>
        /// Performs left turn node and returns top node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private Node LeftTurn(Node node)
        {
            Node rightChild = node.RightChild;
            Node t2 = rightChild.LeftChild;
            node.RightChild = t2;
            rightChild.LeftChild = node;
            GetHeight(node);
            GetHeight(t2);
            return rightChild;
        }
        /// <summary>
        /// Calculates node height, if children heights calculated correctly
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
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
        /// Returns string representation of tree, where each line has node key and indexes of left and right children respectively.
        /// If node has no child, then index is 0
        /// </summary>
        /// <returns></returns>
        public string[] TreeRepresentation()
        {
            // array of strings to represent the tree
            string[] output = new string[Count];
            int currentNodeIndex;
            int currentLeftChildIndex;
            int currentRightChildIndex;
            int lineCount = 0;
            Node currentNode;
            Stack<int> childrenIndexes = new Stack<int>(Count);
            Stack<Node> children = new Stack<Node>(Count);
            children.Push(Root);
            childrenIndexes.Push(0);
            while (!children.IsEmpty())
            {
                currentNode = children.Pop();
                currentNodeIndex = childrenIndexes.Pop();
                // if node has left child, then reserve line in output for it and process it later
                if (currentNode.LeftChild != null)
                {
                    children.Push(currentNode.LeftChild);
                    currentLeftChildIndex = ++lineCount;
                    childrenIndexes.Push(currentLeftChildIndex++);
                }
                else
                {
                    currentLeftChildIndex = 0;
                }
                // if node has right child, then reserve line in output for it and process it later
                if (currentNode.RightChild != null)
                {
                    children.Push(currentNode.RightChild);
                    currentRightChildIndex = ++lineCount;
                    childrenIndexes.Push(currentRightChildIndex++);
                }
                else
                {
                    currentRightChildIndex = 0;
                }
                output[currentNodeIndex] = currentNode.Key + " " + currentLeftChildIndex + " " + currentRightChildIndex;
            }
            return output;
        }
    }
    /// <summary>
    /// Implementation of stack data-structure
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
