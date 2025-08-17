using System;
using System.Security.Principal;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            int[] numbers =
            {
                5, 2, 1, 6, 4, 8, 10
            };

            Console.WriteLine("\nМассив до сортировки:\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            for (int i = 0; i < numbers.Length; i++)
            {
                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    int nextNumberIndex = j + 1;

                    if (numbers[nextNumberIndex] < numbers[j])
                    {
                        int tempElement = numbers[j];
                        numbers[j] = numbers[nextNumberIndex];
                        numbers[nextNumberIndex] = tempElement;
                    }
                }
            }

            Console.WriteLine("\nМассив после сортировки:\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }
        }
    }
}
