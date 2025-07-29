using System;
using System.Threading;

namespace KurwaTSIKLI
{
    internal class practiceSwitch
    {
        static void Main()
        {
            Random random = new Random();
            
            int randomNumber = random.Next(1, 101);
            int firstMultiplicationNumber = 3;
            int secondMultiplicationNumber = 5;
            int sum = 0;

            for (int i = 1; i <= randomNumber; i++)
            {
                if (i % firstMultiplicationNumber == 0 || i % secondMultiplicationNumber == 0)
                {
                    Console.WriteLine($"Добавлено число: {i}");
                    sum += i;
                }
            }

            Console.WriteLine($"рандомное число: {randomNumber}");
            Console.WriteLine($"сумма чисел кратных {firstMultiplicationNumber} или {secondMultiplicationNumber}: {sum}");
        }
    }
}
