using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderTheRainbow
{
    
    class Program
    {
        public static Dictionary<int, int> penalties = new Dictionary<int, int>();
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

        static void Main(string[] args)
        {

            Kattio.Scanner io = new Kattio.Scanner();
            int n = io.NextInt();
            int[] distance = new int[n+1];

            for (int i = 0; i < n+1; i++)
            {
                distance[i] = io.NextInt();
            }
            Console.WriteLine(penalty(distance));
            
        }

        public static int simplePenalty(int startDistance, int stopDistance)
        {
            
            int traveled = stopDistance - startDistance;
            if (penalties.ContainsKey(traveled))
                return penalties[traveled];
            else
            {
                int penalty = (400 - traveled) * (400 - traveled);
                penalties.Add(traveled, penalty);
                return penalty;

            }
        }

        public static int penalty(int[] distance)
        {
            int minPenalty = int.MaxValue;
            //for(int i = distance.Length - 1; i >= 0; i--)
            //{
            //    minPenalty = Math.Min(minPenalty, penalty(distance, i));
            //}
            minPenalty = penalty(distance, 0);
            return minPenalty;
        }

        public static int penalty(int[] distance, int i)
        {
            int minPenalty = int.MaxValue;
            if (i == distance.Length - 1)
                return 0;
            for (int j = i+1; j < distance.Length; j++)
            {
                minPenalty = Math.Min(minPenalty, simplePenalty(distance[i],distance[j]) + penalty(distance, j));
            }
            return minPenalty;
        }
    }
}