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

                    if (maxNumber < numbers[i, j])
                    {
                        maxNumber = numbers[i, j];
                    }

                    Console.Write(numbers[i, j] + " | ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nМаксимальное число: " + maxNumber + "\n");

            for(int i = 0; i < numbers.GetLength(0); i++)
            {
                for(int j = 0;j < numbers.GetLength(1); j++)
                {
                    if (numbers[i, j] == maxNumber)
                    {
                        numbers[i, j] = resetMaxNumber;
                    }

                    Console.Write(numbers[i, j] + " | ");
                }

                Console.WriteLine();
            }
            
            Console.ReadKey();
        }
    }
}
