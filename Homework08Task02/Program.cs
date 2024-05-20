using System;
using System.Collections.Generic;
using Utilites;

namespace Homework08Task02
{
	internal class Program
	{
		private static PhoneDictionary phoneDictionary;
		
		public static void Main(string[] args)
		{
			phoneDictionary = new PhoneDictionary();

			bool exitRequired = false;

			while (!exitRequired)
			{
				PrintMenu();
				string command = Console.ReadLine();

				switch (command)
				{
					case "0":
					{
						exitRequired = true;
						
						break;
					}
					case "1":
					{
						ShowAll();
						
						break;
					}
					case "2":
					{
						AddNote();
						
						break;
					}
					case "3":
					{
						FindNoteByName();
						
						break;
					}
					case "4":
					{
						FindNoteByPhone();
						
						break;
					}
					default:
					{
						Console.WriteLine("Incorrect command");
						PrintMenu();
						
						break;
					}
				}
			}
		}

		private static void ShowAll()
		{
			Dictionary<string, List<string>> notes = phoneDictionary.GetAll();
			PrintNotes(notes);
		}

		private static void AddNote()
		{
			string name = Utils.GetStringFromConsole("Enter name: ");
			List<string> phones = new();
			bool finished = false;

			while (!finished)
			{
				string phone = Utils.GetStringFromConsole("Enter phone or press Enter button to finish: ");

				if (string.IsNullOrEmpty(phone))
				{
					finished = true;
				}
				else
				{
					phones.Add(phone);
				}
			}
			
			phoneDictionary.AddNote(name, phones);
		}

		private static void FindNoteByName()
		{
			string searchWord = Utils.GetStringFromConsole("Enter name or part of name: ");
			Dictionary<string, List<string>> notes = phoneDictionary.FindByName(searchWord);
			PrintNotes(notes);
		}

		private static void FindNoteByPhone()
		{
			string searchWord = Utils.GetStringFromConsole("Enter phone number or part of it: ");
			Dictionary<string, List<string>> notes = phoneDictionary.FindByPhone(searchWord);
			PrintNotes(notes);
		}

		private static void PrintMenu()
		{
			Console.WriteLine("0 - exit\n1 - show all\n2 - add note\n3 - find note by name\n4 - find note by phone number");
		}

		private static void PrintNotes(Dictionary<string, List<string>> notes)
		{
			foreach (KeyValuePair<string,List<string>> pair in notes)
			{
				Console.WriteLine($"{pair.Key}: [{string.Join(", ", pair.Value)}]");
			}
		}
	}
}
