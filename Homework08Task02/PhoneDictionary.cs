using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Homework08Task02
{
	public sealed class PhoneDictionary
	{
		private static readonly string DataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.xml");

		private Dictionary<string, List<string>> _phoneDictionary = new();

		public PhoneDictionary()
		{
			LoadData();
		}
		
		public void AddNote(string name, List<string> phones)
		{
			if (_phoneDictionary.ContainsKey(name))
			{
				foreach (string phone in phones)
				{
					if (_phoneDictionary[name].Contains(phone))
					{
						return;
					}
				
					_phoneDictionary[name].Add(phone);
				}
			}
			else
			{
				_phoneDictionary.Add(name, phones);
			}

			SaveData();
		}

		public Dictionary<string, List<string>> GetAll()
		{
			return new Dictionary<string, List<string>>(_phoneDictionary);
		}

		public Dictionary<string, List<string>> FindByName(string searchWord)
		{
			Dictionary<string, List<string>> searchResult = new();
			
			foreach (string name in _phoneDictionary.Keys)
			{
				if (name.ToLower().Contains(searchWord.ToLower()))
				{
					searchResult.Add(name, _phoneDictionary[name]);
				}
			}

			return searchResult;
		}
		
		public Dictionary<string, List<string>> FindByPhone(string searchWord)
		{
			Dictionary<string, List<string>> searchResult = new();
			
			foreach (string name in _phoneDictionary.Keys)
			{
				foreach (string phone in _phoneDictionary[name])
				{
					if (phone.Contains(searchWord))
					{
						searchResult.Add(name, _phoneDictionary[name]);
						
						break;
					}
				}
			}

			return searchResult;
		}
		
		private void SaveData()
		{
			SerializableDictionary<string, List<string>> serializableDictionary = new(_phoneDictionary);
			
			XmlSerializer xmlSerializer = new(typeof(SerializableDictionary<string, List<string>>));
			Stream fStream = new FileStream(DataPath, FileMode.Create, FileAccess.Write);
			xmlSerializer.Serialize(fStream, serializableDictionary);
			fStream.Close();
		}

		private void LoadData()
		{
			if (!File.Exists(DataPath))
			{
				return;
			}
			
			XmlSerializer xmlSerializer = new(typeof(SerializableDictionary<string, List<string>>));
			Stream fStream = new FileStream(DataPath, FileMode.Open, FileAccess.Read);

			if (xmlSerializer.Deserialize(fStream) is SerializableDictionary<string, List<string>> serializableDictionary)
			{
				_phoneDictionary = serializableDictionary.ToDictionary();
			}

			fStream.Close();
		}
	}
}
