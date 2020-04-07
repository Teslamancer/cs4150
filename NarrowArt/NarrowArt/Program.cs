using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarrowArt
{

    class Program
    {
        //public static Dictionary<int, int> simplePenaltyCache = new Dictionary<int, int>();
        //public static Dictionary<int, int> penaltyCache = new Dictionary<int, int>();
        public static Dictionary<Tuple<int, int, int>, int> cache = new Dictionary<Tuple<int, int, int>, int>();
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
            int numRows = io.NextInt();
            int numToClose = io.NextInt();

            int[,] values = new int[numRows, 2];

            for(int i = 0; i < numRows; i++)
            {
                values[i, 0] = io.NextInt();
                values[i, 1] = io.NextInt();
            }
            //maxValue(values, numRows, 0,-1,5);
            Console.WriteLine(maxValue(values,numRows,0,-1,numToClose));

        }

        

        static int maxValue(int[,] values, int numRows,int r, int uncloseable, int k)
        {
            if (cache.ContainsKey(Tuple.Create(r, uncloseable, k)))
            {
                return cache[Tuple.Create(r, uncloseable, k)];
            }
            int closeZero = int.MinValue;
            int closeOne = int.MinValue;
            int closeNone = int.MinValue;
            int max = 0;
            if (r > numRows - 1)
            {
                max = 0;
                cache.Add(Tuple.Create(r, uncloseable, k), max);
                return max;
            }
            switch(uncloseable)
            {
                case 0:
                    closeOne = values[r, 0] + maxValue(values, numRows, r + 1, 0, k - 1);
                    if (k <= numRows -1- r)
                        closeNone = values[r, 0] + values[r, 1] + maxValue(values, numRows, r + 1, -1, k);
                    else
                        closeNone = int.MinValue;                    
                    max = Math.Max(closeOne, closeNone );
                    cache.Add(Tuple.Create(r, uncloseable, k), max);
                    return max;
                case 1:
                    closeZero = values[r, 1] + maxValue(values, numRows, r + 1, 1, k - 1);
                    if (k<=numRows-1-r)
                        closeNone = values[r, 0] + values[r, 1] + maxValue(values, numRows, r + 1, -1, k);
                    else
                        closeNone = int.MinValue;                    
                    max = Math.Max(closeZero,closeNone );
                    cache.Add(Tuple.Create(r, uncloseable, k), max);
                    return max;
                case -1:
                    closeZero = values[r, 1] + maxValue(values, numRows, r + 1, 1, k - 1);
                    closeOne = values[r, 0] + maxValue(values, numRows, r + 1, 0, k - 1);//This is not right
                    if (k <= numRows -1- r)
                        closeNone = values[r, 0] + values[r, 1] + maxValue(values, numRows, r + 1, -1, k);//This is not right
                    else
                        closeNone = int.MinValue;
                    max = Max(closeZero,closeOne ,closeNone );
                    cache.Add(Tuple.Create(r, uncloseable, k), max);
                    return max;
                default:
                    throw new Exception("Unexpected Error");
            }
            
        }
        static int Max(int x, int y, int z)
        {
            long X = x;
            long Y = y;
            long Z = z;
            long D = Y - X;
            X = Y - ((D >> 63) & D);
            D = X - Z;
            return (int)(X - ((D >> 63) & D));
        }

    }
}