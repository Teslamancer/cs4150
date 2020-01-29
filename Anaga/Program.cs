using System;
using System.Collections.Generic;
using System.IO;

namespace Anaga
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string variables = Console.ReadLine();//gets n and k as input
            string[] tokens = variables.Split(' ');//splits up input line by spaces
            int n = int.Parse(tokens[0]);//initialize n and k to correct input values
            int k = int.Parse(tokens[1]);            

            HashSet<string> solutions = new HashSet<string>();//hashset of all words without an anagram
            HashSet<string> rejects = new HashSet<string>();//hashset of all words with anagram

            for(int i = 0; i < n; i++)//runs for n words
            {
                string sortedWord = sortString(Console.ReadLine());//gets word from line, then sorts alphabetically
                if (solutions.Contains(sortedWord))//if solutions has the same sort, remove and add to rejects
                {
                    solutions.Remove(sortedWord);
                    rejects.Add(sortedWord);
                }
                else if (!rejects.Contains(sortedWord))//if rejects doesn't have the sorted word, add to solutions
                {
                    solutions.Add(sortedWord);
                }
                                  
            }
            Console.Out.WriteLine(solutions.Count);//print number of unique words
            //Console.Read();
        }

        protected static string sortString(string input)//sorts an input string alphabetically
        {            
            char[] ToSort = input.ToCharArray();
            Array.Sort(ToSort);
            return new string(ToSort);
        }
    }
}
