using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            private class PriorityQueue
            {
                private SortedDictionary<float, Queue<int>> pq;
                private HashSet<int> seen;
                
                public bool isEmpty()//TODO: Reimplement this to be fast
                {
                    if(pq.Keys.Count != 0)
                    {
                        foreach(float f in pq.Keys)
                        {
                            foreach(int intersection in pq[f])
                            {
                                if (!seen.Contains(intersection))
                                    return false;
                            }
                        }
                    }
                    return true;
                }

                public PriorityQueue()
                {
                    this.pq = new SortedDictionary<float, Queue<int>>();
                    this.seen = new HashSet<int>();
                }

                public int deleteMax()
                {
                    var keys = pq.Keys;
                    float minWeight = keys.Last();
                    int minIntersection = pq[keys.Last()].Dequeue();
                    while (seen.Contains(minIntersection))
                    {
                        if (pq[keys.Last()].Count == 0)
                        {
                            pq.Remove(minWeight);
                        }
                        keys = pq.Keys;
                        minWeight = keys.Last();
                        if (pq.Count != 0 && pq[keys.Last()].Count != 0)
                            minIntersection = pq[keys.Last()].Dequeue();
                        else if (pq.Count != 0)
                            pq.Remove(keys.Last());                                                
                    }
                    
                    seen.Add(minIntersection);
                    if (pq[keys.Last()].Count == 0)
                        pq.Remove(keys.Last());
                    return minIntersection;
                }

                public void insertOrChange(int intersection, float distance)
                {
                    if (pq.ContainsKey(distance))
                    {
                        pq[distance].Enqueue(intersection);
                    }
                    else
                    {
                        Queue<int> newQueue = new Queue<int>();
                        newQueue.Enqueue(intersection);
                        pq.Add(distance, newQueue);
                    }
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
                this.matrix[v1, v2] = weight;
                this.matrix[v2, v1] = weight;
            }

            public float findMaxSize()
            {
                return dijkstra();
                
                 
            }

            private float dijkstra()
            {
                Dictionary<int, int> prev = new Dictionary<int, int>();
                Dictionary<int, float> dist = new Dictionary<int, float>();
                for (int i = 1; i < numIntersections; i++)
                {
                    dist.Add(i, float.MinValue);
                }
                dist[0] = 1;

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