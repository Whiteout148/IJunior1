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

            int minValueForRandomNumber = 10;
            int maxValueForRandomNumber = 25;

            int randomNumber = random.Next(minValueForRandomNumber, maxValueForRandomNumber + 1);

            int quantityOfMultiplyNumbers = 0;

            Console.WriteLine("Рандомное число: " + randomNumber);

            for (int i = randomNumber; i <= maxValue; i += randomNumber)
            {
                if(i >= 50)
                {
                    quantityOfMultiplyNumbers++;
                    Console.WriteLine(i);
                }
            }

            Console.WriteLine($"Количество кратных чисел в диапазоне от {minValue} до {maxValue} будет: {quantityOfMultiplyNumbers}");
            Console.ReadKey();
        }
    }
}
