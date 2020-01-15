using System;
using System.Collections.Generic;
using System.IO;

namespace Anaga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string variables = Console.ReadLine();
            string[] tokens = variables.Split(' ');
            int n = int.Parse(tokens[0]);
            int k = int.Parse(tokens[1]);            

            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejects = new HashSet<string>();

            for(int i = 0; i < n; i++)
            {
                string sortedWord = sortString(Console.ReadLine());
                if (solutions.Contains(sortedWord))
                {
                    solutions.Remove(sortedWord);
                    rejects.Add(sortedWord);
                }
                else if (!rejects.Contains(sortedWord))
                {
                    solutions.Add(sortedWord);
                }
                                  
            }
            Console.Out.WriteLine(solutions.Count);
            //Console.Read();
        }

        protected static string sortString(string input)
        {            
            char[] ToSort = input.ToCharArray();
            Array.Sort(ToSort);
            return new string(ToSort);
        }
    }
}
