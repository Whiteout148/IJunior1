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

            int randomNumber = random.Next(minValue, maxValue + 1);

            Console.WriteLine("Рандомное число: " + randomNumber);

            for (int i = minValue;i <= maxValue; i++)
            {
                if(i % randomNumber == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
