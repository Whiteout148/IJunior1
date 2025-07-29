using System;
using System.Threading;

namespace KurwaTSIKLI
{
    internal class practiceSwitch
    {
        static void Main()
        {
            Random random = new Random();

            int lastPossibleRandomNumber = 100;
            int randomNumber = random.Next(1, lastPossibleRandomNumber + 1);
            int firstNumberToDivide = 3;
            int secondNumberToDivide = 5;
            int sum = 0;

            for (int i = 1; i <= randomNumber; i++)
            {
                if (i % firstNumberToDivide == 0 || i % secondNumberToDivide == 0)
                {
                    Console.WriteLine($"Добавлено число: {i}");
                    sum += i;
                }
            }

            Console.WriteLine($"рандомное число: {randomNumber}");
            Console.WriteLine($"сумма чисел кратных {firstNumberToDivide} или {secondNumberToDivide}: {sum}");
        }
    }
}
