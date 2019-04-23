using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Week5
{
    class Task2
    {
        public static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            using (StreamReader sr = new StreamReader(currentDir + @"/input.txt"))
            {
                using (StreamWriter sw = new StreamWriter(currentDir + @"/output.txt"))
                {
                    string line = sr.ReadLine();
                    int n = int.Parse(line);
                    PriorityQueue priorityQueue = new PriorityQueue();
                    for (int i = 0; i < n; i++)
                    {
                        line = sr.ReadLine();
                        switch (line[0])
                        {
                            case 'A':
                                int key = int.Parse(line.Substring(2));
                                priorityQueue.Insert(key);
                                break;
                            case 'X':
                                int min;
                                if(priorityQueue.TryExtractMin(out min))
                                {
                                    sw.WriteLine(min);
                                }
                                else
                                {
                                    sw.WriteLine("*");
                                }
                                break;
                            case 'D':
                                string[] lineArgs = line.Split(new char[] {' '}, 
                                    StringSplitOptions.RemoveEmptyEntries);
                                int index = int.Parse(lineArgs[1]);
                                int newKey = int.Parse(lineArgs[2]);
                                priorityQueue.DecreaseKey(index, newKey);
                                break;
                            default:
                                break;
                        }
                    }

                }
            }
        }
        
    }
    class Node : IComparable
    {
        public int Key { get; set; }
        //public Node Parent { get; set; }
        //public Node LeftChild { get; set; }
        //public Node RightChild { get; set; }

        public Node(int key)
        {
            this.Key = key;
        }

        public int CompareTo(object obj)
        {
            Node node = (Node)obj;
            return this.Key.CompareTo(node.Key);
        }
    }
    class PriorityQueue
    {
        public int Length { get; private set; }

        private int lastIndex = 0;
        private Node[] heap;
        private int insertionOrderedHeapLastIndex = 0;
        private Node[] insertionOrderedHeap;

        public PriorityQueue()
        {
            Length = 1000000;
            heap = new Node[Length];
            insertionOrderedHeap = new Node[Length];
        }
        public void Insert(int value)
        {
            Node newNode = new Node(value);
            insertionOrderedHeap[insertionOrderedHeapLastIndex++] = newNode;
            heap[lastIndex] = newNode;
            int parentIndex = (lastIndex - 1) / 2;
            //Node parent = heap[parentIndex];
            //newNode.Parent = parent;
            //if (parentIndex * 2 == lastIndex - 1)
            //{
            //    parent.LeftChild = newNode;
            //}
            //else
            //{
            //    parent.RightChild = newNode;
            //}

            SiftUp(lastIndex);

            lastIndex++;
        }


        public bool TryExtractMin(out int key)
        {
            if(lastIndex == 0)
            {
                key = 0;
                return false;
            }
            key = heap[0].Key;
            if (lastIndex > 0)
            {
                heap[0] = heap[--lastIndex];
                Heapify(0);
            }
            // TODO: things
            return true;
        }
        public void DecreaseKey(int index, int newKey)
        {
            insertionOrderedHeap[index - 1].Key = newKey;
            SiftUp(index - 1);
        }

        private void SiftUp(int index)
        {
            int currentNodeIndex = index;
            Node currentNode = heap[currentNodeIndex];
            int parentIndex = (currentNodeIndex - 1) / 2;
            while (currentNode.Key < heap[parentIndex].Key)
            {
                parentIndex = (currentNodeIndex - 1) / 2;
                Node parent = heap[parentIndex];
                Node tmp = currentNode;
                heap[currentNodeIndex] = heap[parentIndex];
                heap[parentIndex] = tmp;
                currentNodeIndex = parentIndex;
                currentNode = parent;
            }
        }
        private void Heapify(int index)
        {
            int currentNodeIndex = index;
            Node currentNode = heap[currentNodeIndex];
            bool heapifyDone = false;
            while (!heapifyDone)
            {
                int leftChildIndex = currentNodeIndex * 2 + 1;
                int rightChildIndex = currentNodeIndex * 2 + 2;
                if (leftChildIndex < lastIndex && heap[leftChildIndex].Key < currentNode.Key)
                {
                    heap[currentNodeIndex] = heap[leftChildIndex];
                    heap[leftChildIndex] = currentNode;
                    currentNodeIndex = leftChildIndex;
                }
                else if (rightChildIndex < lastIndex && heap[rightChildIndex].Key < currentNode.Key)
                {
                    heap[currentNodeIndex] = heap[rightChildIndex];
                    heap[rightChildIndex] = currentNode;
                    currentNodeIndex = rightChildIndex;
                }
                else
                {
                    heapifyDone = true;
                }
            }
        }
    }
    
    
}