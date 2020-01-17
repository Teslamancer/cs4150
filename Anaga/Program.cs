using System;
using System.Collections.Generic;
using System.IO;

namespace Anaga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
        }

        protected static int numUniqueWords(int n, int k, IEnumerable<string> words)
        {
            HashSet<string> solutions = new HashSet<string>();
            HashSet<string> rejects = new HashSet<string>();

            foreach (string word in words)
            {
                string sortedWord = sortString(word);
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
            return solutions.Count;
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
