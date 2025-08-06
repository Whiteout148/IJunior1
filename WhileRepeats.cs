using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Random random = new Random();

            int minValue = 50;
            int maxValue = 150;
            int minValueRandomNumber = 10;
            int maxValueRandomNumber = 25;

            int randomNumber = random.Next(minValueRandomNumber, maxValueRandomNumber + 1);

            Console.WriteLine("Рандомное число: " + randomNumber);
            Console.WriteLine("Числа, кратные " + randomNumber + " в диапазоне от " + minValue + " до " + maxValue + ":");

            for (int i = minValue; i <= maxValue; i++)
            {
                if (i % randomNumber == 0)
                {
                    Console.WriteLine(i);
                }
            }

            Console.ReadKey();
        }
    }
}
