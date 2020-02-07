using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSink
{
    class Program
    {
        //class City
        //{
        //    public string Name
        //    {
        //        get; private set;
        //    }
        //    public int Toll
        //    {
        //        get; private set;
        //    }
        //    public City(string name, int toll)
        //    {
        //        this.Name = name;
        //        this.Toll = toll;
        //    }
        //}
        class Graph
        {

            int[,] matrix;
            List<string> PostList = new List<string>();
            public Dictionary<string, int> IndexForName = new Dictionary<string, int>();
            public Dictionary<int, string> NameFromIndex = new Dictionary<int, string>();
            //List<string> Topological;

            public Dictionary<string, int> Toll = new Dictionary<string, int>();
            //private int clock = 1;
            public int numCities
            {
                get; private set;
            }

            public Graph(int numVertices)
            {
                numCities = numVertices;
                matrix = new int[numVertices, numVertices];
            }
            public void addEdge(string Source, string Terminal)
            {
                if (IndexForName.ContainsKey(Source))
                {
                    if (IndexForName.ContainsKey(Terminal))
                    {
                        matrix[IndexForName[Source], IndexForName[Terminal]] = Toll[Terminal];
                    }
                }
            }

            public void doDFS(string source)
            {
                Dictionary<string, bool> visited = new Dictionary<string, bool>();
                
                foreach(string city in IndexForName.Keys)
                {
                    if (!visited.ContainsKey(city))
                    {
                        visited.Add(city, false);
                    }
                }
                recursiveDFS(source, visited);
                //PostList.Reverse();

            }
            private void recursiveDFS(string root, Dictionary<string, bool> visited)
            {
                visited[root] = true;
                //PreList.Add(root, clock++);
                if(IndexForName.ContainsKey(root))
                    for(int i=0;i<this.numCities;i++)
                    {
                        if (!visited.ContainsKey(NameFromIndex[i]) || !visited[NameFromIndex[i]])
                        {
                            recursiveDFS(NameFromIndex[i], visited);
                        }
                    }
                PostList.Add(root);
            }

            public int tripCost(string source, string sink)
            {

                int cost = 0;
                bool beginadd = false;
                if (source == sink)
                    return 0;
                for(int i = 0; i < PostList.Count; i++)
                {
                    if (PostList[i] == sink)
                    {
                        beginadd = true;
                        cost += this.Toll[sink];
                        continue;
                    }
                    if (PostList[i] == source)
                    {
                        beginadd = false;
                        break;
                    }
                    if (beginadd)
                    {
                        cost += this.Toll[PostList[i]];
                    }
                }
                if (beginadd)
                    return int.MaxValue;
                else
                    return cost;
            }

            //public string toDOT()
            //{
            //    StringBuilder sb = new StringBuilder();
            //    sb.Append("digraph G {\n");
            //    foreach (string parent in IndexForName.Keys)
            //    {
            //        if (!parent.Equals(""))
            //        {
            //            sb.Append('\"' + parent + '\"');
            //            sb.Append(" -> {");
            //            foreach (string child in matrix[parent].Keys)
            //            {
            //                if (!child.Equals(""))
            //                {
            //                    sb.Append('\"' + child + '\"');
            //                }
            //            }
            //            sb.Append("}\n");
            //        }
            //    }
            //    sb.Append("}");
            //    return sb.ToString();
            //}
        }

        static void Main(string[] args)
        {
            string n = Console.ReadLine();
            
            int numCities = int.Parse(n);
            //Dictionary<string, int> cityTolls = new Dictionary<string, int>();
            Graph map = new Graph(numCities);
            for (int i = 0; i < numCities; i++)
            {
                string[] cityData = Console.ReadLine().Split(' ');
                map.Toll.Add(cityData[0], int.Parse(cityData[1]));
                map.IndexForName.Add(cityData[0], i);
                map.NameFromIndex.Add(i, cityData[0]);
            }

            int numHighways = int.Parse(Console.ReadLine());
            
            for(int i = 0; i < numHighways; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                map.addEdge(edge[0],edge[1]);
            }
            
            int numTrips = int.Parse(Console.ReadLine());
            List<int> TripCost = new List<int>();
            for(int i = 0; i < numTrips; i++)
            {
                string[] cities = Console.ReadLine().Split(' ');
                map.doDFS(cities[0]);
                TripCost.Add(map.tripCost(cities[0], cities[1]));
            }

            for(int i = 0; i < TripCost.Count; i++)
            {
                if (TripCost[i] == int.MaxValue)
                {
                    Console.WriteLine("NO");
                }
                else
                {
                    Console.WriteLine(TripCost[i]);
                }
            }


            //Console.Write(map.toDOT());
            Console.Read();
        }
    }
}
