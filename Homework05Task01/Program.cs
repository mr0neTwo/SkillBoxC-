using System;

namespace Homework05Task01
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Console.Write("Enter any sentence: ");
			string sentence = Console.ReadLine();
			string[] words = SplitSentence(sentence);
			PrintWords(words);
		}

		private static string[] SplitSentence(string sentence)
		{
			string symbolsToRemove = ".,;:)({}!?";

			foreach (char symbol in symbolsToRemove)
			{
				sentence = sentence.Replace(symbol.ToString(), "");
			}
			
			string[] words = sentence.Split(' ');

			return words;
		}

		private static void PrintWords(string[] words)
		{
			foreach (string word in words)
			{
				Console.WriteLine(word);
			}
		}
	}
}
