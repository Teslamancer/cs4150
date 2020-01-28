using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            ArrayList candidateGalaxy = new ArrayList();
            ArrayList PU = new ArrayList();
            //if(numStars % 2 != 0)
            //{
            //    loopCount--;
            //}

            for (int i = 0; i < numStars; i++)
            {
                string star = Console.ReadLine();
                string[] starCoordinates = variables.Split(' ');

                PU.Add(new Star(int.Parse(starCoordinates[0]),int.Parse(starCoordinates[1])));
            }
            
            
        }

        static int hasMajority(ArrayList candidateGalaxy)
        {
            int loopCount = candidateGalaxy.Count - 1;
            if (candidateGalaxy.Count == 0)
                return 0;
            else if (candidateGalaxy.Count == 1)
                return 1;
            if (candidateGalaxy.Count % 2 != 0)
            {
                loopCount--;
            }
            for(int i=0;i<loopCount;i+=2)
            {
                if((Star)(candidateGalaxy[i]).in)
            }

        }
                
    }
}
