using System;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            Random random = new Random();

            int[,] array = new int[10, 10];

            int maxNumber = 0;
            int maxRandomNumber = 1000;

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = random.Next(0, maxRandomNumber + 1);

                    if (maxNumber < array[i, j])
                    {
                        maxNumber = array[i, j];
                    }

                    Console.Write(array[i, j] + " | ");
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nМаксимальное число: " + maxNumber + "\n");

            for (int i = 0;i < array.GetLength(0); i++)
            {
                for(int j = 0;j < array.GetLength(1); j++)
                {
                    if (array[i, j] == maxNumber)
                    {
                        array[i, j] = 0;
                    }

                    Console.Write(array[i, j] + " | ");
                }

                Console.WriteLine();
            }
        }
    }
}
