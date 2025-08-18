using System;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7 };

            Console.WriteLine("Исходный массив:");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " | ");
            }

            Console.WriteLine("\n\nКакое количество раз хотите сдвинуть массив влево?");
            int userSteps = Convert.ToInt32(Console.ReadLine());

            userSteps = userSteps % numbers.Length;

            for (int i = 0; i < userSteps; i++)
            {
                int first = numbers[0];

                for (int j = 0; j < numbers.Length - 1; j++)
                {
                    int toNextElementIndex = j + 1;
                    numbers[j] = numbers[toNextElementIndex];
                }

                numbers[numbers.Length - 1] = first;
            }

            Console.WriteLine("\nМассив после сдвига:\n");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.Write(numbers[i] + " | ");
            }
        }
    }
}
