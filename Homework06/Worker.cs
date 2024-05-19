using System;
using Utilites;

namespace Homework06
{
	public struct Worker
	{
		public int Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public string FullName { get; set; }
		public int Age { get; set; }
		public int Height { get; set; }
		public DateTime BirthDate { get; set; }
		public string BirthPlace { get; set; }

		public Worker(string data)
		{
			string[] values = data.Split(Repository.Separator);
			
			Id = int.Parse(values[0]);
			CreatedAt = Utils.UnixTimeStampToDateTime(long.Parse(values[1]));
			FullName = values[2];
			Age = int.Parse(values[3]);
			Height = int.Parse(values[4]);
			BirthDate = Utils.UnixTimeStampToDateTime(long.Parse(values[5]));
			BirthPlace = values[6];
		}

		public override string ToString()
		{
			string[] values = new string[7];

			values[0] = Id.ToString();
			values[1] = Utils.DateTimeToUnixTimestamp(CreatedAt).ToString();
			values[2] = FullName;
			values[3] = Age.ToString();
			values[4] = Height.ToString();
			values[5] = Utils.DateTimeToUnixTimestamp(BirthDate).ToString();
			values[6] = BirthPlace;

			string separator = Repository.Separator.ToString();

			return string.Join(separator, values);
		}
	}
}
