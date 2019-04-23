using System;
using System.IO;
using System.Linq;

namespace Week6
{
    public class Task3
    {
        public static void Main(string[] args)
        {
            string currentDirectory = Environment.CurrentDirectory;
            using (StreamReader sr = new StreamReader(currentDirectory + @"/input.txt"))
            {
                int n = int.Parse(sr.ReadLine());
                BinaryTree tree = new BinaryTree(n);
                int[][] nodes = new int[n][];
                for (int i = 0; i < n; i++)
                {
                    nodes[i] = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                }
                tree.Insert(nodes);
                using (StreamWriter sw = new StreamWriter(currentDirectory + @"/output.txt"))
                {
                    sw.WriteLine(tree.Depth());
                }
            }
        }
    }
    class BinaryTree
    {
        public Node Root { get; set; }
        public int Count { get; private set; } = 0;
        public int Length { get; private set; }
        public BinaryTree(int n)
        {
            Length = n;
        }
        public void Insert(int[][] nodes)
        {
            if (nodes.GetLength(0) == 0)
            {
                return;
            }
            int[] node = nodes[0];
            int key = node[0];
            Root = new Node(key, null);
            Node current = Root;
            int leftIndex = nodes[0][1] - 1;
            int rightIndex = nodes[0][2] - 1;
            Triad triad = new Triad(current, leftIndex, rightIndex);
            Stack<Triad> stack = new Stack<Triad>();
            stack.Push(triad);
            while (!stack.IsEmpty())
            {
                triad = stack.Pop();
                current = triad.Node;
                leftIndex = triad.LeftIndex;
                rightIndex = triad.RightIndex;
                if (leftIndex != -1)
                {
                    current.LeftChild = new Node(nodes[leftIndex][0], current);
                    stack.Push(new Triad(current.LeftChild, nodes[leftIndex][1] - 1, nodes[leftIndex][2] - 1));
                }
                if (rightIndex != -1)
                {
                    current.RightChild = new Node(nodes[rightIndex][0], current);
                    stack.Push(new Triad(current.RightChild, nodes[rightIndex][1] - 1, nodes[rightIndex][2] - 1));
                }
            }
        }
        public int Depth()
        {
            if (Root == null)
                return 0;
            Stack<Tuple<Node, int>> stack = new Stack<Tuple<Node, int>>();
            stack.Push(new Tuple<Node, int>(Root, 1));
            int maxDepth = 0;
            while (!stack.IsEmpty())
            {
                Tuple<Node, int> tuple = stack.Pop();
                Node node = tuple.First;
                int depth = tuple.Second;
                maxDepth = maxDepth < depth ? depth : maxDepth;
                if (node.LeftChild != null)
                {
                    stack.Push(new Tuple<Node, int>(node.LeftChild, depth + 1));
                }
                if (node.RightChild != null)
                {
                    stack.Push(new Tuple<Node, int>(node.RightChild, depth + 1));
                }
            }
            return maxDepth;
        }
        private int Depth(Node node)
        {
            if (node == null)
                return 0;
            int leftDepth = Depth(node.LeftChild);
            int rightDepth = Depth(node.RightChild);
            return rightDepth > leftDepth ? rightDepth + 1 : leftDepth + 1;
        }
        public Tuple<int,int> Search(int key)
        {
            if (Root == null)
            {
                return new Tuple<int,int>(-1, -1);
            }
            Node current = Root;
            int first;
            int second;
            while (true)
            {
                if (current.Key == key)
                {
                    first = current.Index;
                    second = FindLastKeyOccurrence(current, key);
                    return new Tuple<int, int>(first, second);
                }
                else if (key < current.Key)
                {
                    if (current.LeftChild == null)
                    {
                        return new Tuple<int, int>(-1, -1);
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
                        return new Tuple<int, int>(-1, -1);
                    }
                    else
                    {
                        current = current.RightChild;
                    }
                }
            }
        }
        private int FindLastKeyOccurrence(Node current, int key)
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
                    return FindLastKeyOccurrence(current.LeftChild, key);
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
        public Node(int key, Node parent)
        {
            Parent = parent;
            Key = key;
        }
    }
    class Tuple <T, V>
    {
        public T First { get; private set; }
        public V Second { get; private set; }
        public Tuple(T first, V second)
        {
            First = first;
            Second = second;
        }
    }
    class Triad
    {
        public Node Node { get; set; }
        public int LeftIndex { get; set; }
        public int RightIndex { get; set; }
        public Triad(Node node, int leftIndex, int rightIndex)
        {
            Node = node;
            LeftIndex = leftIndex;
            RightIndex = rightIndex;
        }
    }
    class Stack<T>
    {
        private int n = 1000000;
        private T[] stack;
        private int head = 0;
        public Stack()
        {
            stack = new T[n];
        }
        public T Pop()
        {
            return stack[--head];
        }
        public T Peek()
        {
            return stack[head - 1];
        }
        public void Push(T value)
        {
            stack[head++] = value;
        }
        public bool IsEmpty()
        {
            return head <= 0;
        }
    }
}