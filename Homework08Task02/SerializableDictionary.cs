using System.Collections.Generic;
using System.Xml.Serialization;

namespace Homework08Task02;

[XmlRoot("Dictionary")]
public class SerializableDictionary<TKey, TValue>
{
	[XmlElement("Item")] 
	public List<SerializableKeyValuePair<TKey, TValue>> Items { get; set; }

	public SerializableDictionary()
	{
	}

	public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
	{
		Items = new List<SerializableKeyValuePair<TKey, TValue>>();

		foreach (var kvp in dictionary)
		{
			Items.Add(new SerializableKeyValuePair<TKey, TValue> { Key = kvp.Key, Value = kvp.Value });
		}
	}

	public Dictionary<TKey, TValue> ToDictionary()
	{
		var dictionary = new Dictionary<TKey, TValue>();

		foreach (var kvp in Items)
		{
			dictionary[kvp.Key] = kvp.Value;
		}

		return dictionary;
	}
}

public sealed class SerializableKeyValuePair<TKey, TValue>
{
	[XmlElement("Key")] 
	public TKey Key { get; set; }

	[XmlElement("Value")] 
	public TValue Value { get; set; }
}
