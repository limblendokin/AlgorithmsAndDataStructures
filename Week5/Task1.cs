using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week5
{
    class Task1
    {
        public static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                int n = int.Parse(sr.ReadLine());
                int[] array = sr.ReadLine().Split(' ').Select(x => int.Parse(x)).ToArray();
                var heap = new NonDecreasingHeap<int>(array);
                using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
                {
                    sw.WriteLine(heap.IsNotDecreasing() ? "YES" : "NO");
                }
            }
        }
        
    }
    class NonDecreasingHeap<T> where T : IComparable
    {
        private T[] heap;
        public NonDecreasingHeap(T[] array)
        {
            heap = array;
        }
        public bool IsNotDecreasing()
        {
            int heapDepth = (int)Math.Ceiling(Math.Log(heap.Length, 2));
            Stack<int> nodeStack = new Stack<int>(heapDepth);
            nodeStack.Push(0);
            int currentNodeIndex;
            int leftChildIndex;
            int rightChildIndex;
            bool isNotDecreasing = true;
            while (!nodeStack.IsEmpty() && isNotDecreasing)
            {
                currentNodeIndex = nodeStack.Pop();
                leftChildIndex = currentNodeIndex * 2 + 1;
                rightChildIndex = currentNodeIndex * 2 + 2;
                if(leftChildIndex < heap.Length)
                {
                    if(heap[currentNodeIndex].CompareTo(heap[leftChildIndex]) > 0)
                    {
                        isNotDecreasing = false;
                    }
                    nodeStack.Push(leftChildIndex);
                }
                if(rightChildIndex < heap.Length)
                {
                    if(heap[currentNodeIndex].CompareTo(heap[rightChildIndex]) > 0)
                    {
                        isNotDecreasing = false;
                    }
                    nodeStack.Push(rightChildIndex);
                }
            }
            return isNotDecreasing;
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
        public Stack(int n)
        {
            this.n = n;
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