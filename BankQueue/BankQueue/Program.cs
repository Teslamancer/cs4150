using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetShorty
{
    public static class listExt
    {
        public static void AddSorted<T>(this List<T> list, T value)
        {
            int x = list.BinarySearch(value);
            list.Insert((x >= 0) ? x : ~x, value);
        }
    }
    class Program
    {
        class Kattio
        {
            public class NoMoreTokensException : Exception
            {
            }

            public class Tokenizer
            {
                string[] tokens = new string[0];
                private int pos;
                StreamReader reader;

                public Tokenizer(Stream inStream)
                {
                    var bs = new BufferedStream(inStream);
                    reader = new StreamReader(bs);
                }

                public Tokenizer() : this(Console.OpenStandardInput())
                {
                    // Nothing more to do
                }

                private string PeekNext()
                {
                    if (pos < 0)
                        // pos < 0 indicates that there are no more tokens
                        return null;
                    if (pos < tokens.Length)
                    {
                        if (tokens[pos].Length == 0)
                        {
                            ++pos;
                            return PeekNext();
                        }
                        return tokens[pos];
                    }
                    string line = reader.ReadLine();
                    if (line == null)
                    {
                        // There is no more data to read
                        pos = -1;
                        return null;
                    }
                    // Split the line that was read on white space characters
                    tokens = line.Split(null);
                    pos = 0;
                    return PeekNext();
                }

                public bool HasNext()
                {
                    return (PeekNext() != null);
                }

                public string Next()
                {
                    string next = PeekNext();
                    if (next == null)
                        throw new NoMoreTokensException();
                    ++pos;
                    return next;
                }
            }


            public class Scanner : Tokenizer
            {

                public int NextInt()
                {
                    return int.Parse(Next());
                }

                public long NextLong()
                {
                    return long.Parse(Next());
                }

                public float NextFloat()
                {
                    return float.Parse(Next());
                }

                public double NextDouble()
                {
                    return double.Parse(Next());
                }
            }


            public class BufferedStdoutWriter : StreamWriter
            {
                public BufferedStdoutWriter() : base(new BufferedStream(Console.OpenStandardOutput()))
                {
                }
            }
        }

        
        class OptimalQueue
        {
            //class DescendingComparer<TKey>: IComparer<int>
            //{
            //    public int Compare(int x, int y)
            //    {
            //        return y.CompareTo(x);
            //    }
            //}

            //class DescendingOrEqualComparer<TKey> : IComparer<int>
            //{
            //    public int Compare(int x, int y)
            //    {
            //        int result = y.CompareTo(x);
            //        if (result == 0)
            //            return 1;
            //        else
            //            return result;
            //    }
            //}
            private List<List<int>> data;
            private int TimeRemaining;

            public OptimalQueue(int time)
            {
                data = new List<List<int>>();
                for(int i = 0; i < time; i++)
                {
                    data.Add(new List<int>());
                }
                TimeRemaining = time;
            }

            public void Enqueue(int cash, int time)
            {

                data[time].AddSorted<int>(cash);
            }
            public int MaxCash()
            {
                int toReturn = 0;
                for(int currentSlot = TimeRemaining - 1; currentSlot >= 0; currentSlot--)
                {
                    int currMax = 0;
                    int maxSlot = data.Count() - 1;
                    for (int findMax = maxSlot; findMax >= currentSlot; findMax--)
                    {
                        if (data[findMax].Count == 0)
                        {
                            //data.RemoveAt(findMax);
                            continue;
                        }
                        int MaxAtI = data[findMax][data[findMax].Count()-1];
                        if (MaxAtI > currMax)
                        {
                            currMax = MaxAtI;
                            maxSlot = findMax;
                        }
                    }
                    toReturn += currMax;
                    if(currMax != 0)
                        data[maxSlot].RemoveAt(data[maxSlot].Count()-1);
                }
                return toReturn;
            }

            
        }
        
        static void Main(string[] args)
        {
            
            Kattio.Scanner io = new Kattio.Scanner();
            int n = io.NextInt();
            int t = io.NextInt();

            OptimalQueue q = new OptimalQueue(t);

            for(int i = 0; i < n; i++)
            {
                int cash = io.NextInt();
                int ttl = io.NextInt();
                q.Enqueue(cash, ttl);
            }

            //Kattio.BufferedStdoutWriter writer = new Kattio.BufferedStdoutWriter();

            Console.WriteLine(q.MaxCash());
            //Console.ReadLine();
        }
    }
}