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

            int nonAnagrams = 0;

            HashSet<string> words = new HashSet<string>();

            for(int i = 0; i < n; i++)
            {
                string word = sortString(Console.ReadLine());
                if (words.Contains(word))
                {
                    nonAnagrams--;
                    continue;
                }
                else
                {
                    words.Add(word);
                    nonAnagrams++;
                }                    
            }
            Console.Out.WriteLine(nonAnagrams);
            Console.Read();
        }

        protected static string sortString(string input)
        {            
            char[] ToSort = input.ToCharArray();
            Array.Sort(ToSort);
            return new string(ToSort);
        }
    }
}
