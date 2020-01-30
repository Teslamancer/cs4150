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
            Stack<Star> PU = new Stack<Star>();
            
            //if(numStars % 2 != 0)
            //{
            //    loopCount--;
            //}

            for (int i = 0; i < numStars; i++)
            {
                string star = Console.ReadLine();
                string[] starCoordinates = star.Split(' ');

                PU.Push(new Star(int.Parse(starCoordinates[0]),int.Parse(starCoordinates[1])));
            }
            List<Star> immutablePU = new List<Star>(PU);
            Star CandidateStar = getMajorityGalaxyStarCandidate(PU);
            Console.WriteLine("Candidate Star - x=" + CandidateStar.x + " y=" + CandidateStar.y);

            hasMajorityGalaxy(CandidateStar, immutablePU);

            Console.Read();
        }

        static void hasMajorityGalaxy(Star Candidate, List<Star> PU)
        {
            Stack<Star> toReturn = new Stack<Star>();
            //toReturn.Push(Candidate);
            foreach (Star s in PU)
            {
                if (Candidate.inGalaxy(s))
                {
                    toReturn.Push(s);
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

        static Star getMajorityGalaxyStarCandidate(Stack<Star> candidateGalaxy)
        {
            int loopCount = candidateGalaxy.Count - 1;
            Stack<Star> toReturn = new Stack<Star>();
            if (candidateGalaxy.Count == 0)
                return null;
            else if (candidateGalaxy.Count == 1)
                return candidateGalaxy.Pop();
            if (candidateGalaxy.Count % 2 != 0)
            {
                loopCount--;
                toReturn.Push(candidateGalaxy.Pop());
            }
            for(int i=0;i<loopCount;i+=2)
            {
                Star a = candidateGalaxy.Pop();
                if (a.inGalaxy(candidateGalaxy.Pop()))
                {
                    toReturn.Push(a);
                }
            }
            return getMajorityGalaxyStarCandidate(toReturn);
        }
                
    }
}
