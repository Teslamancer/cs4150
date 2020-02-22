using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GetShorty
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
            //This is an adjecency matrix of edges from parent to child nodes
            private float[,] matrix;
            //private Dictionary<string, HashSet<string>> reverseList = new Dictionary<string, HashSet<string>>();
            //public Dictionary<string, int> Toll = new Dictionary<string, int>();
            //public Dictionary<string, int>
            //public Dictionary<string, int> PreList = new Dictionary<string, int>();
            //public List<string> PostList;
            //private Dictionary<string, int> postDict;
            //private int clock = 1;

            public class ByWeight : IComparer<Tuple<int, float>>
            {
                float xExt, yExt;
                //x is the tuple already in the set
                public int Compare(Tuple<int, float> x, Tuple<int, float> y)
                {
                    xExt = x.Item2;
                    yExt = y.Item2;

                    if (xExt > yExt)
                        return -1;
                    else if (xExt < yExt)
                        return 1;
                    else
                    {
                        if (x.Item1 == y.Item1)
                            return 0;
                        else
                            return -1;
                    }
                }
            }
            private class PriorityQueue
            {
                private SortedSet<Tuple<int, float>> items;
                private Dictionary<int, float> previousWeight;
                //private HashSet<int> seen;

                public int Count
                {
                    get;private set;
                }
                
                public bool isEmpty()//TODO: Reimplement this to be fast
                {
                    return this.Count == 0;
                }

                public PriorityQueue()
                {
                    this.items = new SortedSet<Tuple<int, float>>(new ByWeight());
                    this.previousWeight = new Dictionary<int, float>();
                    //this.seen = new HashSet<int>();
                }

                public int deleteMax()
                {
                    int maxIntersection = items.Last().Item1;
                    items.Remove(items.Last());
                    this.Count--;
                    return maxIntersection;
                }

                public void insertOrChange(int intersection, float distance)
                {
                    Tuple<int, float> newItem = Tuple.Create(intersection, distance);
                    if (previousWeight.ContainsKey(intersection) && previousWeight[intersection] != distance)
                    {
                        Tuple<int, float> oldItem = Tuple.Create(intersection, previousWeight[intersection]);
                        items.Remove(oldItem);
                        previousWeight[intersection] = distance;
                    }
                    else
                    {
                        previousWeight.Add(intersection, distance);
                        this.Count++;
                    }
                    items.Add(newItem);

                }
            }
            public int numIntersections
            {
                get; private set;
            }

            public Graph(int numVertices)
            {
                this.numIntersections = numVertices;
                this.matrix = new float[numVertices, numVertices];
                for(int i = 0; i < numVertices; i++)
                {
                    for(int x = 0; x < numVertices; x++)
                    {
                        matrix[i, x] = float.NaN;
                    }
                }
            }
            public void addEdge(int v1, int v2, float weight)
            {
                if(float.IsNaN(this.matrix[v1, v2]) || this.matrix[v1, v2] < weight)
                {
                    this.matrix[v1, v2] = weight;
                    this.matrix[v2, v1] = weight;
                }
            }

            public float findMaxSize()
            {
                return dijkstra();
                
                 
            }

            private float dijkstra()
            {
                Dictionary<int, int> prev = new Dictionary<int, int>();
                List<float> dist = new List<float>();
                dist.Add(1);
                for (int i = 1; i < numIntersections; i++)
                {
                    dist.Add(float.MinValue);
                }
                

                PriorityQueue pq = new PriorityQueue();
                pq.insertOrChange(0, 1);

                //float MaxSize = 1;

                while (!pq.isEmpty())
                {
                    int currentIntersection = pq.deleteMax();
                    for (int i = 0; i < numIntersections; i++)
                    {
                        float currentWeight = matrix[currentIntersection, i];
                        if (!float.IsNaN(currentWeight))
                        {
                            if (dist[i] < dist[currentIntersection] * currentWeight)
                            {
                                dist[i] = dist[currentIntersection] * currentWeight;
                                prev[i] = currentIntersection;
                                pq.insertOrChange(i, dist[i]);
                            }
                        }
                    }
                }
                return dist[numIntersections - 1];
            }
        }

        static Tuple<int, int, float> getEdge()
        {
            
            string n = Console.ReadLine();
            string[] values = n.Split(" ");
            int v1 = int.Parse(values[0]);
            int v2 = int.Parse(values[1]);
            float weight = float.Parse(values[2]);
            var toReturn = Tuple.Create(v1, v2, weight);
            return toReturn;
        }
        static void Main(string[] args)
        {
            List<float> reports = new List<float>();
            //StringBuilder sb = new StringBuilder("2 15000\n");
            //Random r = new Random();
            //for(int i = 0; i < 15000; i++)
            //{
            //    sb.Append("0 1 ");
            //    double weight = r.NextDouble();
            //    sb.Append(weight.ToString("0.0000"));
            //    sb.Append("\n");
            //}
            //sb.Append("0 0");
            //string test = sb.ToString()
            while (true)
            {
                string n = Console.ReadLine();
                string[] values = n.Split(" ");
                int numIntersections = int.Parse(values[0]);
                int numCorridors = int.Parse(values[1]);
                if (numIntersections == 0 && numCorridors == 0)
                    break;
                else
                {
                    Graph dungeon = new Graph(numIntersections);
                    for (int i = 0; i < numCorridors; i++)
                    {
                        var edgeData = getEdge();
                        int v1 = edgeData.Item1;
                        int v2 = edgeData.Item2;
                        float weight = edgeData.Item3;
                        dungeon.addEdge(v1, v2, weight);
                    }
                    reports.Add(dungeon.findMaxSize());
                }
            }
            for(int i = 0; i < reports.Count; i++)
            {
                Console.WriteLine(reports[i].ToString("0.0000"));
            }
                        
            //Console.Write(map.toDOT());
            Console.Read();
        }
    }
}