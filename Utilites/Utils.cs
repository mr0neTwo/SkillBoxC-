using System;
using System.Globalization;

namespace Utilites
{
	public static class Utils
	{
		public static int GetIntFromConsole(string message)
		{
			while (true)
			{
				Console.Write(message);
				string input = Console.ReadLine();
				
				if (int.TryParse(input, out int number))
				{
					return number;
				}
				
				Console.WriteLine("Entered value is not integer");
			}
		}

		public static string GetStringFromConsole(string message)
		{
			Console.Write(message);

			return Console.ReadLine();
		}

		public static DateTime GetDateFromConsole(string message)
		{
			string format = "dd.MM.yyyy";
			
			while (true)
			{
				Console.Write(message);
				string input = Console.ReadLine();
				
				if (DateTime.TryParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
				{
					return dateTime;
				}
				
				Console.WriteLine($"Enter the date in the format {format}");
			}
		}
		
		public static long DateTimeToUnixTimestamp(DateTime dateTime)
		{
			return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
		}
		
		public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
			
			return dateTime;
		}
	}
}
