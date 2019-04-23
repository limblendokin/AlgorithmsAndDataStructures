using System;
using System.IO;

namespace Week3
{
    class Task1
    {

        public static int Calculate(int a, int b, char operation)
        {
            switch (operation)
            {
                case '+':
                    return a + b;
                case '-':
                    return a - b;
                case '*':
                    return a * b;
                default:
                    return 0;
            }
        }
        public static void Main(String[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            using (StreamReader sr = new StreamReader(currentDir + @"\input.txt"))
            {
                Stack<int> stack = new Stack<int>();
                int n = int.Parse(sr.ReadLine());
                string[] ops = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for(int i = 0; i < n; i++)
                {
                    int operand;
                    if (int.TryParse(ops[i], out operand))
                    {
                        stack.Push(operand);
                    }
                    else
                    {
                        int b = stack.IsEmpty() ? 0 : stack.Pop();
                        int a = stack.IsEmpty() ? 0 : stack.Pop();
                        stack.Push(Calculate(a, b, ops[i][0]));
                    }
                }
                using(StreamWriter sw = new StreamWriter(currentDir + @"\output.txt"))
                {
                    sw.Write(stack.Pop());
                }
            }
        }
        class Stack<T> where T : IComparable
        {
            private T[] stack;
            public int Count { get; private set; } = 0;

            public Stack()
            {
                stack = new T[1000000];
            }
            public void Push(T value)
            {
                stack[Count++] = value;
            }
            public T Pop()
            {
                return stack[--Count];
            }

            internal bool IsEmpty()
            {
                return Count <= 0;
            }
        }
        
    }
}