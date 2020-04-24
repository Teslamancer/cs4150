using System;
using System.Collections.Generic;
using System.IO;

namespace GridGobble
{

    class Program
    {
        //public static Dictionary<int, int> simplePenaltyCache = new Dictionary<int, int>();
        //public static Dictionary<int, int> penaltyCache = new Dictionary<int, int>();
        public static Dictionary<Tuple<int, int>, int> cache = new Dictionary<Tuple<int, int>, int>();
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
            int numCols = io.NextInt();

            List<int[]> values = new List<int[]>();
            for (int i = numRows - 1; i >= 0; i--)
                values.Add(getRow(numCols, io));
            values.Reverse();
            //maxValue(values, numRows, 0,-1,5);
            int max = int.MinValue;
            for(int i = 0; i < numCols; i++)
            {
                int temp = maxValue(values, numRows-1, i);
                if (temp > max)
                {
                    max = temp;
                }
            }
            Console.WriteLine(max);

        }

        static int[] getRow(int numCols, Kattio.Scanner io)
        {
            int[] toReturn = new int[numCols];
            for (int i = 0; i < numCols; i++)
                toReturn[i] = io.NextInt();
            return toReturn;
        }



        static int maxValue(List<int[]> values, int rowNum, int prevCol)
        {
            if (cache.ContainsKey(Tuple.Create(rowNum, prevCol)))
            {
                return cache[Tuple.Create(rowNum, prevCol)];
            }
            //int toReturn = 0;
            //if (rowNum == 0)
            //    return values[0][prevCol];
            if (rowNum < 0)
                return 0;
            else
            {
                int leftCol, rightCol;
                if (prevCol == 0)
                    leftCol = values[0].Length - 1;
                else
                {
                    leftCol = prevCol - 1;
                }
                if (prevCol == values[0].Length - 1)
                {
                    rightCol = 0;
                }
                else
                {
                    rightCol = prevCol + 1;
                }
                int goLeft = maxValue(values, rowNum - 1, leftCol) - values[rowNum][prevCol];
                int goRight = maxValue(values, rowNum - 1, rightCol) - values[rowNum][prevCol];
                int stayStraight = maxValue(values, rowNum - 1, prevCol) + values[rowNum][prevCol];
                int max =  Max(goLeft, goRight, stayStraight);
                cache.Add(Tuple.Create(rowNum, prevCol), max);
                return max;
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