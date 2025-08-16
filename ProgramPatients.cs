using System;
using System.Security.Principal;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            int[] numbers = new int[30];

            int biggiestRepeats = 0;
            int currentRepeats = 0;
            int biggiestRepeatNumber = 0;
            int numberWithBiggiestRepeats = 0;

            int maxRandomElement = 10;
            int minRandomElement = -10;

            Random random = new Random();

            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i] = random.Next(minRandomElement, maxRandomElement + 1);
            }

            for (int i = 1; i < numbers.Length - 1; i++)
            {
                int nextElementIndex = i + 1;
                int lastElementIndex = i - 1;

                if (numbers[i] == numbers[nextElementIndex])
                {
                    currentRepeats++;

                    if(biggiestRepeats < currentRepeats)
                    {
                        biggiestRepeats = currentRepeats;
                        biggiestRepeatNumber = numbers[i];
                    }
                }
                else
                {
                    currentRepeats = 0;
                }
            }

            Console.WriteLine($"число: {biggiestRepeatNumber} повторяется {biggiestRepeats + 1} количество раз");
            Console.WriteLine("Весь массив: ");

            for (int i = 0;i < numbers.Length;i++)
            {
                Console.WriteLine(numbers[i]);
            }

            Console.ReadKey();
        }
    }
}
