using System;
using System.Collections.Generic;
using System.IO;

namespace Week3
{
    class Task4
    {
        public static void Main(String[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            using (StreamReader sr = new StreamReader(currentDir + @"\input.txt"))
            {
                Queue<int> queue = new Queue<int>();
                int n = int.Parse(sr.ReadLine());
                string[] line;
                int operand;
                using (StreamWriter sw = new StreamWriter(currentDir + @"\output.txt")) {
                    for (int i = 0; i < n; i++)
                    {
                        line = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (line[0] == "+")
                        {
                            operand = int.Parse(line[1]);
                            queue.Enqueue(operand);
                        }
                        else if (line[0] == "-")
                        {
                            queue.Dequeue();
                        }
                        else
                        {
                            sw.WriteLine(queue.Min());
                        }
                    }
                }
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
                return stack[head-1];
            }
            public void Push(T value)
            {
                stack[head++] = value;
            }
            public bool isEmpty()
            {
                return head <= 0;
            }
        }
        class Queue<T> where T : IComparable
        {
            private Stack<Pair<T>> s1;
            private Stack<Pair<T>> s2;
            public Queue()
            {
                s1 = new Stack<Pair<T>>();
                s2 = new Stack<Pair<T>>();
            }
            public void Enqueue(T value)
            {
                s1.Push(new Pair<T>(value, s1.isEmpty() ? value : s1.Peek().MinimumValue.CompareTo(value) < 0 ?
                    s1.Peek().MinimumValue : value));
            }
            public T Dequeue()
            {
                if (s2.isEmpty())
                {
                    while (!s1.isEmpty())
                    {
                        Pair<T> pair = s1.Pop();
                        pair.MinimumValue = s2.isEmpty() ?
                            pair.CurrentValue :
                            pair.CurrentValue.CompareTo(s2.Peek().MinimumValue) < 0 ?
                            pair.CurrentValue : s2.Peek().MinimumValue;
                        s2.Push(pair);
                    }
                }
                return s2.Pop().CurrentValue;
            }
            public bool isEmpty()
            {
                return s1.isEmpty() && s2.isEmpty();
            }
            public T Min()
            {
                return s1.isEmpty() ? s2.Peek().MinimumValue : s2.isEmpty() ? 
                    s1.Peek().MinimumValue : s1.Peek().MinimumValue.CompareTo(s2.Peek().MinimumValue) < 0 ? 
                    s1.Peek().MinimumValue : s2.Peek().MinimumValue;
            }
        }
        class Pair<T> where T : IComparable
        {
            public T CurrentValue { get; set; }
            public T MinimumValue { get; set; }

            public Pair(T currentValue, T minimumValue)
            {
                this.CurrentValue = currentValue;
                this.MinimumValue = minimumValue;
            }
        }
    }
}