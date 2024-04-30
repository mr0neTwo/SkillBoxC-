using System;

namespace Homework02
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const int fieldNameSize = 10;

            string fullNaneFieldName = "Ф.И.";
            string ageFieldName = "Возраст";
            string emailFieldName = "Email";
            string programmingScoreFieldName = "Баллы по программированию";
            string mathScoreFieldName = "Баллы по математике";
            string physicsScoreFieldName = "Баллы по физике";
            
            string firstName = "Иван";
            string lastName = "Пупкин";
            string fullName = firstName + lastName;
            int age = 15;
            string email = "pupkin@mail.ru";
            float programmingScore = 4.5f;
            float mathScore = 4f;
            float physicsScore = 5f;
            
            string data = $"{fullNaneFieldName, fieldNameSize}: {fullName}\n";
            data += $"{ageFieldName, fieldNameSize}: {age}\n";
            data += $"{emailFieldName, fieldNameSize}: {email}\n";
            data += $"{programmingScoreFieldName, fieldNameSize}: {programmingScore}\n";
            data += $"{mathScoreFieldName, fieldNameSize}: {mathScore}\n";
            data += $"{physicsScoreFieldName, fieldNameSize}: {physicsScore}\n";
            
            Console.WriteLine(data);
        }
    }
}