using System;
using System.IO;
using System.Linq;

namespace Week3
{
    class Task1
    {
        #region Static Fields

        public static int[] operandStack;
        public static int head;

        #endregion

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
                case '/':
			        return a / b;
                default:
                    return 0;
            }
        }
        public static void Main(String[] args)
        {
            String currentDir = Environment.GetCurrentDirectory();
            using (StreamReader sr = new StreamReader(currentDir +@"/input.txt"))
            {
                char[] temp = new char[10];
                int count = 0;
                char currentChar;
                while (sr.Peek() >= 0)
                {
                    while (currentChar = sr.Read() >= 0 && currentChar != ' ')
                    {
                        temp[count++] = currentChar;
                    }
                    int operand;
                    if (int.TryParse(new String(temp, 0, count), out operand))
                    {
                        PushOperand(operand);
                    }
                    else
                    {
                        int a = OperandStackIsEmpty() ? 0 : PopOperand();
                        int b = OperandStackIsEmpty() ? 0 : PopOperand();
                        PushOperand(Calculate(a, b, temp[0]));
                    }
                    count = 0;

            }
        }
    }


    public static void PushOperand(int operand)
    {
        operandStack[head++] = operand;
    }

    public static int PopOperand()
    {
        return operandStack[--head];
    }

    public static bool OperandStackIsEmpty()
    {
        return head <= 0;
    }
}