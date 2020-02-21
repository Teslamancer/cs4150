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
            //This is an adjecency list of edges from parent to child nodes
            private Dictionary<string, HashSet<string>> edgeList = new Dictionary<string, HashSet<string>>();
            public List<string> Students = new List<string>();
            //private Dictionary<string, HashSet<string>> reverseList = new Dictionary<string, HashSet<string>>();
            //public Dictionary<string, int> Toll = new Dictionary<string, int>();
            //public Dictionary<string, int>
            //public Dictionary<string, int> PreList = new Dictionary<string, int>();
            public List<string> PostList;
            private Dictionary<string, int> postDict;
            //private int clock = 1;
            public int numStudents
            {
                get; private set;
            }

            public Graph(int numVertices)
            {
                this.numStudents = numVertices;
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

                //if (reverseList.ContainsKey(Terminal))
                //{
                //    reverseList[Terminal].Add(Source);
                //}
                //else
                //{
                //    reverseList.Add(Terminal, new HashSet<string>());
                //    reverseList[Terminal].Add(Source);
                //}

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
                for (int i = 0; i < PostList.Count; i++)
                {
                    postDict.Add(PostList[i], i);
                }
            }
            private void recursiveDFS(string root, Dictionary<string, bool> visited)
            {
                visited[root] = true;
                if (edgeList.ContainsKey(root))
                    foreach (string child in edgeList[root])
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

            public string generateReport(string starter)
            {
                StringBuilder sb = new StringBuilder();
                //sb.Append(starter + " ");
                Dictionary<string, bool> visited = new Dictionary<string, bool>();
                //visited.Add(starter, true);

                foreach (string student in BFS(starter, visited))
                {
                    sb.Append(student + " ");
                }


                List<string> remainingStudents = new List<string>();
                foreach (string student in Students)
                {
                    if (!visited.ContainsKey(student))
                    {
                        visited.Add(student, true);
                        remainingStudents.Add(student);
                    }
                }
                remainingStudents.Sort();
                foreach (string student in remainingStudents)
                {
                    sb.Append(student + " ");

                }
                sb.Remove(sb.Length - 1, 1);


                return sb.ToString();
            }

            private List<string> BFS(string root, Dictionary<string, bool> visited)
            {
                //List<List<string>> toReturn = new List<List<string>>();

                Dictionary<string, int> level = new Dictionary<string, int>();
                Queue<string> que = new Queue<string>();
                que.Enqueue(root);
                level.Add(root, 0);
                visited.Add(root, true);
                while (que.Count > 0)
                {
                    root = que.Peek();
                    que.Dequeue();
                    if (edgeList.ContainsKey(root))
                        foreach (string child in edgeList[root])
                        {
                            if (!visited.ContainsKey(child))
                            {
                                que.Enqueue(child);
                                if (level.ContainsKey(root))
                                {
                                    if (level.ContainsKey(child))
                                    {
                                        if (level[child] > level[root] + 1)
                                        {
                                            level[child] = level[root] + 1;
                                        }
                                    }
                                    else
                                    {
                                        level.Add(child, level[root] + 1);
                                    }
                                }
                                visited.Add(child, true);

                            }
                        }
                }
                Dictionary<int, List<string>> levelToStrings = new Dictionary<int, List<string>>();
                foreach (string student in level.Keys)
                {
                    if (levelToStrings.ContainsKey(level[student]))
                    {
                        levelToStrings[level[student]].Add(student);
                    }
                    else
                    {
                        levelToStrings.Add(level[student], new List<string>());
                        levelToStrings[level[student]].Add(student);
                    }
                }
                foreach (int x in levelToStrings.Keys)
                    levelToStrings[x].Sort();
                List<string> toReturn = new List<string>();
                for (int i = 0; i < levelToStrings.Count; i++)
                {
                    for (int x = 0; x < levelToStrings[i].Count; x++)
                    {
                        toReturn.Add(levelToStrings[i][x]);
                    }
                }
                return toReturn;
            }
        }

        static Tuple<int, int, float> getEdge()
        {
            
            string n = Console.ReadLine();
            string[] values = n.Split(" ");
            int v1 = int.Parse(values[0]);
            int v2 = int.Parse(values[1]);
            float weight = int.Parse(values[2]);
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
                    Graph dungeon = new Graph(numIntersections, numCorridors);
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