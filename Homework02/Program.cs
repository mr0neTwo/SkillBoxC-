using System;

namespace Homework02
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            const int fieldNameSize = 32;

            string fullNaneFieldName = "Ф.И.";
            string ageFieldName = "Возраст";
            string emailFieldName = "Email";
            string programmingScoreFieldName = "Баллы по программированию";
            string mathScoreFieldName = "Баллы по математике";
            string physicsScoreFieldName = "Баллы по физике";
            string avgProgrammingScoreFieldName = "Средний бал по программированию";
            string avgMathScoreFieldName = "Средний бал по математике";
            string avgPhysicsScoreFieldName = "Средний бал по физике";
            
            string firstName = "Иван";
            string lastName = "Пупкин";
            string fullName = firstName + lastName;
            int age = 15;
            string email = "pupkin@mail.ru";
            float programmingScore1 = 4, programmingScore2 = 5;
            float mathScore1 = 3, mathScore2 = 5;
            float physicsScore1 = 5, physicsScore2 = 4;
            
            float programmingScoreSum = programmingScore1 + programmingScore2;
            float mathScoreSum = mathScore1 + mathScore2;
            float physicsScoreSum = physicsScore1 + physicsScore2;

            float avgProgrammingScore = programmingScoreSum / 2;
            float avgMathScore = mathScoreSum / 2;
            float avgPhysicsScore = physicsScoreSum / 2;
            
            string mainData = $"{fullNaneFieldName, fieldNameSize}: {fullName}\n";
            mainData += $"{ageFieldName, fieldNameSize}: {age}\n";
            mainData += $"{emailFieldName, fieldNameSize}: {email}\n";
            mainData += $"{programmingScoreFieldName, fieldNameSize}: {programmingScoreSum}\n";
            mainData += $"{mathScoreFieldName, fieldNameSize}: {mathScoreSum}\n";
            mainData += $"{physicsScoreFieldName, fieldNameSize}: {physicsScoreSum}\n";

            string avgData = $"{avgProgrammingScoreFieldName, fieldNameSize}: {avgProgrammingScore}\n";
            avgData += $"{avgMathScoreFieldName, fieldNameSize}: {avgMathScore}\n";
            avgData += $"{avgPhysicsScoreFieldName, fieldNameSize}: {avgPhysicsScore}\n";
            
            Console.WriteLine(mainData);
            Console.ReadKey();
            
            Console.WriteLine(avgData);
            Console.ReadKey();
        }
    }
}