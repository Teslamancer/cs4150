﻿using System;
using System.Collections.Generic;
using System.IO;

namespace CartesiaPrime
{
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

        static int a, b, c, d;
        static bool[,] grid;

        static Dictionary<Tuple<int, int, int>, int> pathCache;
        static Dictionary<int, int> xCache = new Dictionary<int, int>();
        static Dictionary<int, int> yCache = new Dictionary<int, int>();
        static void Main(string[] args)
        {
            pathCache = new Dictionary<Tuple<int, int, int>, int>();
            Kattio.Scanner io = new Kattio.Scanner();
            int xGridCoord = io.NextInt() + 1000;
            int yGridCoord = io.NextInt() + 1000;

            a = io.NextInt();
            b = io.NextInt();
            c = io.NextInt();
            d = io.NextInt();

            int timeConstraint = io.NextInt();

            int numBorg = io.NextInt();

            grid = new bool[2001, 2001];

            for (int x = 0; x < 2001; x++)
                for (int y = 0; y < 2001; y++)
                    grid[x, y] = false;

            for(int i = 0; i < numBorg; i++)
            {
                grid[io.NextInt() + 1000, io.NextInt() + 1000] = true;
            }

            int result = minPath(xGridCoord, yGridCoord, timeConstraint, 1);

            if (result > timeConstraint)
                Console.WriteLine("You will be assimilated! Resistance is futile!");
            else
                Console.WriteLine("I had " + (timeConstraint - result) + " to spare! Beam me up Scotty!");
        }

        static int minPath(int prevX, int prevY, int constraint, int t)
        {
            if (pathCache.ContainsKey(Tuple.Create(prevX, prevY, t)))
                return pathCache[Tuple.Create(prevX, prevY, t)];
            if (prevX == 1000 && prevY == 1000 && t <= constraint + 1)
            {
                if (!pathCache.ContainsKey(Tuple.Create(prevX, prevY, t)))
                {
                    pathCache.Add(Tuple.Create(prevX, prevY, t), 0);
                }
                return 0;
            }
            else if (t > constraint+1 || grid[prevX, prevY])
            {
                if (!pathCache.ContainsKey(Tuple.Create(prevX, prevY, t)))
                {
                    pathCache.Add(Tuple.Create(prevX, prevY, t), 50);
                }
                return 50;
            }
            else
            {
                int minusXminusY = minPath(prevX - deltaX(t), prevY - deltaY(t), constraint, t+1) + 1;

                int minusXplusY = minPath(prevX - deltaX(t), prevY + deltaY(t), constraint, t+1) + 1;//problem here

                int plusXminusY = minPath(prevX + deltaX(t), prevY - deltaY(t), constraint, t+1) + 1;

                int plusXplusY = minPath(prevX + deltaX(t), prevY + deltaY(t), constraint, t+ 1) + 1;

                int min1 = Math.Min(minusXminusY, plusXplusY);

                int min2 = Math.Min(minusXplusY, plusXminusY);

                int minFinal = Math.Min(min1, min2);

                if (!pathCache.ContainsKey(Tuple.Create(prevX, prevY, t)))
                {
                    pathCache.Add(Tuple.Create(prevX, prevY, t), minFinal);
                }

                return minFinal;
            }
            
            return 0;
        }

        static int deltaX(int t)
        {
            if (xCache.ContainsKey(t))
            {
                return xCache[t];
            }
            else
            {
                int dX = (a * t) % b;
                xCache.Add(t, dX);
                return dX;
            }
        }

        static int deltaY(int t)
        {
            if (yCache.ContainsKey(t))
            {
                return yCache[t];
            }
            else
            {
                int dY = (c * t) % d;
                yCache.Add(t, dY);
                return dY;

            }
        }
    }
}
