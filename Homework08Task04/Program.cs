using System.Xml;
using System.Xml.Linq;
using Utilites;

namespace Homework08Task04
{
	internal class Program
	{
		public static void Main(string[] args)
		{
			Person person = GetPersonDataFromConsole();
			CreateXmlFile(person);
		}

		private static void CreateXmlFile(Person person)
		{
			XElement personElement = new XElement("Person");

			XAttribute nameAttribute = new XAttribute("name", person.FullName);
			personElement.Add(nameAttribute);

			XElement addressElement = new XElement("Address");
			personElement.Add(addressElement);

			XElement streetElement = new XElement("Street", person.Street);
			addressElement.Add(streetElement);
			
			XElement houseNumberElement = new XElement("HouseNumber", person.HouseNumber);
			addressElement.Add(houseNumberElement);
			
			XElement flatNumberElement = new XElement("FlatNumber", person.FlatNumber);
			addressElement.Add(flatNumberElement);
			
			XElement phonesElement = new XElement("Phones");
			personElement.Add(phonesElement);
			
			XElement mobilePhoneElement = new XElement("MobilePhone", person.MobilePhone);
			phonesElement.Add(mobilePhoneElement);
			
			XElement flatPhoneElement = new XElement("FlatPhone", person.FlatPhone);
			phonesElement.Add(flatPhoneElement);
			
			personElement.Save("person.xml");
		}

		private static Person GetPersonDataFromConsole()
		{
			Person person = new Person()
			{
				FullName = Utils.GetStringFromConsole("Enter full name: "),
				Street = Utils.GetStringFromConsole("Enter street: "),
				HouseNumber = Utils.GetIntFromConsole("Enter house number: "),
				FlatNumber = Utils.GetIntFromConsole("Enter flat number: "),
				MobilePhone = Utils.GetStringFromConsole("Enter mobile number: "),
				FlatPhone = Utils.GetStringFromConsole("Enter flat number: "),
			};

			return person;
		}
	}
}
