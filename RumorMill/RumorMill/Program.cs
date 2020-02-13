using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumorMill
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
                sb.Append(starter + " ");
                Dictionary<string, bool> visited = new Dictionary<string, bool>();
                visited.Add(starter, true);
                List<string> dayOneFriends = new List<string>();
                if (edgeList.ContainsKey(starter))
                {
                    foreach(string friend in edgeList[starter])
                    {
                        if (!visited.ContainsKey(friend))
                        {
                            visited.Add(friend, true);
                            dayOneFriends.Add(friend);
                        }
                    }
                    dayOneFriends.Sort();
                    foreach(string friend in dayOneFriends)
                    {
                        sb.Append(friend + " ");
                    }

                    List<string> dayTwoFriends = new List<string>();
                    foreach(string friendOne in dayOneFriends)
                    {
                        foreach (string friend in edgeList[friendOne])
                        {
                            if (!visited.ContainsKey(friend))
                            {
                                visited.Add(friend, true);
                                dayTwoFriends.Add(friend);
                            }
                        }
                        dayTwoFriends.Sort();
                        foreach (string friend in dayTwoFriends)
                        {
                            sb.Append(friend + " ");
                        }

                    }

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
                for(int i = 0; i < remainingStudents.Count; i++)
                {
                    if (i == remainingStudents.Count - 1)
                    {
                        sb.Append(remainingStudents[i]);
                    }
                    else
                    {
                        sb.Append(remainingStudents[i]+" ");
                    }
                }


                return sb.ToString();
            }
        }

        static void Main(string[] args)
        {
            string n = Console.ReadLine();

            int numStudents = int.Parse(n);
            Graph students = new Graph(numStudents);

            for (int i = 0; i < numStudents; i++)
            {
                students.Students.Add(Console.ReadLine());
            }

            int numFriendships = int.Parse(Console.ReadLine());

            for (int i = 0; i < numFriendships; i++)
            {
                string[] edge = Console.ReadLine().Split(' ');
                students.addEdge(edge[0], edge[1]);
                students.addEdge(edge[1], edge[0]);
            }
            int numRumors = int.Parse(Console.ReadLine());
            List<string> reports = new List<string>();
            for (int i = 0; i < numRumors; i++)
            {
                string starter = Console.ReadLine();
                reports.Add(students.generateReport(starter));
            }

            for (int i = 0; i < reports.Count; i++)
            {
                Console.WriteLine(reports[i]);
            }



            //Console.Write(map.toDOT());
            Console.Read();
        }
    }
}