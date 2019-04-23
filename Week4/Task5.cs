using System;
using System.Collections.Generic;
using System.IO;

namespace Week4
{
    class Task5
    {
        public static void Main(string[] args)
        {
            string currentDir = Environment.CurrentDirectory;
            Quack quack = new Quack(currentDir);
            quack.Start();
        }
        
    }
    class Quack
    {
        private bool halt = false;
        private Registers registers;
        private CodeReader codeReader;
        private Printer printer;
        private Queue<ushort> operandQueue;
        private Dictionary<char, Action> commandsDictionary;
        public Quack(string dirPath)
        {
            codeReader = new CodeReader(dirPath + @"/input.txt");
            printer = new Printer(dirPath);
            operandQueue = new Queue<ushort>();
            registers = new Registers();
            commandsDictionary = new Dictionary<char, Action>();
            commandsDictionary.Add('+', () =>
            {
                ushort a = operandQueue.Dequeue();
                ushort b = operandQueue.Dequeue();
                ushort result = (ushort)((a + b) % 65536);
                operandQueue.Enqueue(result);
            });
            commandsDictionary.Add('-', () =>
            {
                ushort a = operandQueue.Dequeue();
                ushort b = operandQueue.Dequeue();
                ushort result = (ushort)((a - b) % 65536);
                operandQueue.Enqueue(result);
            });
            commandsDictionary.Add('*', () =>
            {
                ushort a = operandQueue.Dequeue();
                ushort b = operandQueue.Dequeue();
                ushort result = (ushort)((a * b) % 65536);
                operandQueue.Enqueue(result);
            });
            commandsDictionary.Add('/', () =>
            {
                ushort a = operandQueue.Dequeue();
                ushort b = operandQueue.Dequeue();
                ushort result;
                if (b == 0)
                    result = 0;
                else
                    result = (ushort)((a / b) % 65536);
                operandQueue.Enqueue(result);
            });
            commandsDictionary.Add('%', () =>
            {
                ushort a = operandQueue.Dequeue();
                ushort b = operandQueue.Dequeue();
                ushort result;
                if (b == 0)
                    result = 0;
                else
                    result = (ushort)((a % b) % 65536);
                operandQueue.Enqueue(result);
            });
            commandsDictionary.Add('>', () =>
            {
                registers.Store(codeReader.CurrentLine[1], operandQueue.Dequeue());
            });
            commandsDictionary.Add('<', () =>
            {
                operandQueue.Enqueue(registers.GetValueOf(codeReader.CurrentLine[1]));
            });
            commandsDictionary.Add('P', () =>
            {
                if (codeReader.CurrentLine.Length == 1)
                {
                    printer.WriteLine(operandQueue.Dequeue());
                }
                else
                {
                    printer.WriteLine(registers.GetValueOf(codeReader.CurrentLine[1]));
                }
            });
            commandsDictionary.Add('C', () => 
            {
                ushort a;
                if(codeReader.CurrentLine.Length == 1)
                {
                    a = operandQueue.Dequeue();
                }
                else
                {
                    a = registers.GetValueOf(codeReader.CurrentLine[1]);
                }
                a %= 256;
                printer.Write((char)a);
            });
            commandsDictionary.Add(':', () =>
            {
                
            });
            commandsDictionary.Add('J', () =>
            {
                codeReader.JumpToLabel(codeReader.CurrentLine.Substring(1));
            });
            commandsDictionary.Add('Z', () =>
            {
                if(registers.GetValueOf(codeReader.CurrentLine[1]) == 0)
                {
                    codeReader.JumpToLabel(codeReader.CurrentLine.Substring(2));
                }
            });
            commandsDictionary.Add('E', () =>
            {
                ushort reg1Value = registers.GetValueOf(codeReader.CurrentLine[1]);
                ushort reg2Value = registers.GetValueOf(codeReader.CurrentLine[2]);
                if(reg1Value == reg2Value)
                {
                    codeReader.JumpToLabel(codeReader.CurrentLine.Substring(3));
                }
            });
            commandsDictionary.Add('G', () => 
            {
                ushort reg1Value = registers.GetValueOf(codeReader.CurrentLine[1]);
                ushort reg2Value = registers.GetValueOf(codeReader.CurrentLine[2]);
                if (reg1Value > reg2Value)
                {
                    codeReader.JumpToLabel(codeReader.CurrentLine.Substring(3));
                }
            });
            commandsDictionary.Add('Q', () =>
            {
                halt = true;
            });
        }
        public void Start()
        {
            while (!halt)
            {
                string command = codeReader.NextCommand();
                ExecuteCommand(command);
                halt = codeReader.EndOfCodeReached;
            }
            printer.Dispose();
        }
        private void ExecuteCommand(string line)
        {
            if(line[0] >= '0' && line[0] <= '9')
            {
                operandQueue.Enqueue(ushort.Parse(line));
            }
            else
            {
                Action command;
                commandsDictionary.TryGetValue(line[0], out command);
                command.Invoke();
            }
        }
    }
    class Registers
    {
        private ushort[] registers;
        private int n;
        public Registers()
        {
            n = 27;
            registers = new ushort[n];
        }
        public ushort GetValueOf(char regName)
        {
            return registers[regName - 'a'];
        }
        public void Store(char regName, ushort value)
        {
            registers[regName - 'a'] = value;
        }
    }
    class Printer : IDisposable
    {
        private StreamWriter sw;
        public Printer(string dirPath)
        {
            sw = new StreamWriter(dirPath + @"/output.txt");
        }

        public void Dispose()
        {
            sw.Dispose();
        }

        public void WriteLine(ushort value)
        {
            sw.WriteLine(value);
        }

        public void WriteLine(string value)
        {
            sw.WriteLine(value);
        }
        public void Write(char value)
        {
            sw.Write(value);
        }
    }
    class Queue<T> where T : IComparable
    {
        private int n;
        private int head = 0;
        private int tail = 0;
        private T[] queue;
        public Queue()
        {
            n = 100000;
            queue = new T[n];
        }
        public void Enqueue(T value)
        {
            queue[tail++] = value;
        }
        public T Dequeue()
        {
            return queue[head++];
        }
        public bool isEmpty()
        {
            return head >= tail;
        }
    }
    class CodeReader
    {
        private string[] code;
        private LabelSet labels;
        public string CurrentLine { get; private set; }
        public int CurrentLineIndex { get; private set; } = 0;
        public bool EndOfCodeReached { get; private set; } = false;

        public CodeReader(string path)
        {
            labels = new LabelSet();
            if (File.Exists(path))
            {
                code = File.ReadAllLines(path);
            }
            for(int i = 0; i < code.Length; i++)
            {
                if(code[i][0] == ':')
                {
                    labels.Add(code[i].Substring(1), i);
                }
            }
        }

        public string NextCommand()
        {
            CurrentLine = code[CurrentLineIndex++];
            EndOfCodeReached = CurrentLineIndex >= code.Length;
            return CurrentLine;
        }
        
        public void JumpToLabel(string labelName)
        {
            Label label = labels.FindByName(labelName);
            CurrentLineIndex = label.Pointer;
        }
    }
    class Label
    {
        public string Name { get; private set; }
        public int Pointer { get; private set; }
        public Label(string name, int pointer)
        {
            this.Name = name;
            this.Pointer = pointer;
        }
    }
    class LabelSet
    {
        private Label[] labels;
        private int n;
        public int Count { get; private set; }

        public LabelSet()
        {
            n = 1000;
            labels = new Label[n];
        }

        public void Add(string labelName, int pointer)
        {
            labels[Count++] = new Label(labelName, pointer);
        }

        public Label FindByName(string labelName)
        {
            for(int i = 0; i < Count; i++)
            {
                if (labels[i].Name.Equals(labelName))
                    return labels[i];
            }
            return null;
        }
    }
}