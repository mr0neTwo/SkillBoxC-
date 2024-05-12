using System;
using System.Linq;

namespace Homework05Task02
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Console.Write("Enter any sentence: ");
			string sentence = Console.ReadLine();
			string reversedSentence = ReversWords(sentence);

			Console.WriteLine(sentence);
			Console.WriteLine(reversedSentence);
		}
		
		private static string ReversWords(string sentence)
		{
			string[] words = SplitSentence(sentence);
			string[] reversedWords = words.Reverse().ToArray();
			string reversedSentence = string.Join(" ", reversedWords);

			return reversedSentence;
		}
		
		private static string[] SplitSentence(string sentence)
		{
			string[] words = sentence.Split(' ');

			return words;
		}
	}
}
