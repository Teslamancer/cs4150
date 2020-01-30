using System;
using System.Collections.Generic;

namespace GalaxyQuest
{
    class Program
    {

        class Star
        {
            public static int distance
            {
                get;set;
            }
            public int x
            {
                get;private set;
            }
            public int y
            {
                get; private set;
            }
            public Star(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public bool inGalaxy(Star other)
            {
                return distance >= (this.x - other.x) * (this.x - other.x) + (this.y - other.y) * (this.y - other.y);
            }
        }
        static void Main(string[] args)
        {
            string variables = Console.ReadLine();
            string[] tokens = variables.Split(' ');
            Star.distance = int.Parse(tokens[0]);
            int numStars = int.Parse(tokens[1]);
            //Stack<Star> candidateGalaxy = new Stack<Star>();
            List<Star> PU = new List<Star>();
            
            //if(numStars % 2 != 0)
            //{
            //    loopCount--;
            //}

            for (int i = 0; i < numStars; i++)
            {
                string star = Console.ReadLine();
                string[] starCoordinates = star.Split(' ');

                PU.Add(new Star(int.Parse(starCoordinates[0]),int.Parse(starCoordinates[1])));
            }
            //Star CandidateStar = getMajorityGalaxyStarCandidate(PU);
            //Console.WriteLine("Candidate Star - x=" + CandidateStar.x + " y=" + CandidateStar.y);

            //hasMajorityGalaxy(CandidateStar, PU);
            getMajorityGalaxyStarCandidate(PU, PU);
            //Console.Read();
        }

        static void hasMajorityGalaxy(Star Candidate, List<Star> PU)
        {
            List<Star> toReturn = new List<Star>();            
            //toReturn.Push(Candidate);
            foreach (Star s in PU)
            {
                if (Candidate.inGalaxy(s))
                {
                    toReturn.Add(s);
                }
            }
            if(toReturn.Count > PU.Count / 2)
            {
                Console.WriteLine(toReturn.Count);
            }
            else
            {
                Console.WriteLine("NO");
            }
        }

        static Star getMajorityGalaxyStarCandidate(List<Star> candidateGalaxy, List<Star> PU)
        {
            int loopCount = candidateGalaxy.Count - 1;
            List<Star> toReturn = new List<Star>();
            Star y = null;
            if (candidateGalaxy.Count == 0)
                return null;
            else if (candidateGalaxy.Count == 1)
                return candidateGalaxy[0];
            //else if(candidateGalaxy.Count == 2)
            //{
            //    //candidateGalaxy.Pop();
            //    return candidateGalaxy.Pop();
            //}
            else
            {
                if (candidateGalaxy.Count % 2 != 0)
                {
                    loopCount--;
                    //toReturn.Add(candidateGalaxy[0]);
                    y = candidateGalaxy[candidateGalaxy.Count - 1];
                }            
                for(int i=0;i<=loopCount;i+=2)
                {
                    Star a = candidateGalaxy[i];
                    Star b = candidateGalaxy[i + 1];
                    if (a.inGalaxy(b))
                    {
                        //toReturn.Push(a);//TODO: Fix this, as pushing both on can form an infinite loop, but only pushing one can cause a false negative
                        toReturn.Add(b);
                    }
                }
                Star x = getMajorityGalaxyStarCandidate(toReturn, PU);

                if(x == null)
                {
                    if (PU.Count % 2 != 0)
                    {
                        hasMajorityGalaxy(y, PU);
                    }
                    else
                    {
                        Console.WriteLine("NO");
                    }
                }
                else
                {
                    hasMajorityGalaxy(x, PU);
                }
                return null;
            }
        }
                
    }
}
