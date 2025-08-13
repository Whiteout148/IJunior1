using System;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            Random random = new Random();

            int[,] numbers = new int[10, 10];

            int maxNumber = int.MinValue;
            int maxRandomNumber = 10;
            int resetMaxNumber = 0;

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    numbers[i, j] = random.Next(0, maxRandomNumber + 1);
                }
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write(numbers[i, j] + " | ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nНачальное состояние.\n");

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (maxNumber < numbers[i, j])
                    {
                        maxNumber = numbers[i, j];
                    }
                }
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == maxNumber)
                    {
                        numbers[i, j] = resetMaxNumber;
                    }
                }    
            }

            for (int i = 0; i < numbers.GetLength(0); i++)
            {
                for (int j = 0; j < numbers.GetLength(1); j++)
                {
                    Console.Write(numbers[i, j] + " | ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("Состояние при замене всех чисел");
            Console.ReadKey();
        }
    }
}
