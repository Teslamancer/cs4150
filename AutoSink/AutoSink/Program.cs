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
            //This is an adjecency list of edges from parent to child nodes
            private Dictionary<string, HashSet<string>> edgeList = new Dictionary<string, HashSet<string>>();
            private Dictionary<string, HashSet<string>> reverseList = new Dictionary<string, HashSet<string>>();
            public Dictionary<string, int> Toll = new Dictionary<string, int>();
            //public Dictionary<string, int>
            //public Dictionary<string, int> PreList = new Dictionary<string, int>();
            public List<string> PostList;
            private Dictionary<string, int> postDict;
            private int clock = 1;
            public int numCities
            {
                get; private set;
            }

            public Graph(int numVertices)
            {
                this.numCities = numVertices;
            }
            public void addEdge(string Source, string Terminal)
            {
                if (edgeList.ContainsKey(Source))
                {
                    edgeList[Source].Add(Terminal);
                }
                else
                {
                    edgeList.Add(Source, new HashSet<string>());
                    edgeList[Source].Add(Terminal);
                }

                if (reverseList.ContainsKey(Terminal))
                {
                    reverseList[Terminal].Add(Source);
                }
                else
                {
                    reverseList.Add(Terminal, new HashSet<string>());
                    reverseList[Terminal].Add(Source);
                }

            }

            private void doDFS(string source)
            {
                Dictionary<string, bool> visited = new Dictionary<string, bool>();
                this.PostList = new List<string>();
                this.postDict = new Dictionary<string, int>();
                foreach (string city in edgeList.Keys)
                {
                    if (!visited.ContainsKey(city))
                    {
                        visited.Add(city, false);
                    }
                }
                recursiveDFS(source, visited);
                for(int i = 0; i < PostList.Count; i++)
                {
                    postDict.Add(PostList[i], i);
                }
            }
            private void recursiveDFS(string root, Dictionary<string, bool> visited)
            {
                visited[root] = true;
                if(edgeList.ContainsKey(root))
                    foreach(string child in edgeList[root])
                    {
                        if (!visited.ContainsKey(child) || !visited[child])
                        {
                            recursiveDFS(child, visited);
                        }
                    }
                PostList.Add(root);
            }

            public string toDOT()
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("digraph G {\n");
                foreach (string parent in edgeList.Keys)
                {
                    if (!parent.Equals(""))
                    {
                        sb.Append('\"' + parent + '\"');
                        sb.Append(" -> {");
                        foreach (string child in edgeList[parent])
                        {
                            if (!child.Equals(""))
                            {
                                sb.Append('\"' + child + '\"');
                            }
                        }
                        sb.Append("}\n");
                    }
                }
                sb.Append("}");
                return sb.ToString();
            }
            public int tripCost(string source, string sink)
            {
                this.doDFS(source);
                int cost = 0;
                if (source == sink)
                    return 0;
                else
                {
                    
                    string current = sink;
                    while(current != source)
                    {
                        cost += Toll[current];
                        int minToll = int.MaxValue;
                        string minCity = source;
                        if (!reverseList.ContainsKey(current))
                            return int.MaxValue;
                        foreach (string city in reverseList[current])
                        {
                            if (postDict.ContainsKey(city) && postDict[city] < postDict[source])
                            {
                                if (Toll[city] < minToll)
                                {
                                    minToll = Toll[city];
                                    minCity = city;
                                }
                            }
                        }
                        current = minCity;

                        
                    }
                    if (current == source)
                        return cost;
                }
                
                return int.MaxValue;
            }
        }

        static void Main(string[] args)
        {
            string n = Console.ReadLine();
            
            int numCities = int.Parse(n);
            Graph map = new Graph(numCities);
            
            for(int i = 0; i < numCities; i++)
            {
                string[] cityData = Console.ReadLine().Split(' ');
                map.Toll.Add(cityData[0], int.Parse(cityData[1]));
            }

            int numHighways = int.Parse(Console.ReadLine());
            
            for(int i = 0; i < numHighways; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                map.addEdge(edge[0],edge[1]);
            }

            int numTrips = int.Parse(Console.ReadLine());
            List<int> TripCost = new List<int>();
            for (int i = 0; i < numTrips; i++)
            {
                string[] cities = Console.ReadLine().Split(' ');
                TripCost.Add(map.tripCost(cities[0], cities[1]));
            }

            for (int i = 0; i < TripCost.Count; i++)
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
            //Console.Read();
        }
    }
}
