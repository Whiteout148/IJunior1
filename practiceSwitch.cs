using System;

class Program
{
    static void Main()
    {
        int[,] array = new int[3, 3];
        Random random = new Random();

        int maxRandomNumber = 10;
        int sumOfSecondRow = 0;
        int productOfFirstColumn = 1;

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = random.Next(0, maxRandomNumber + 1);
                Console.Write(array[i, j] + " | ");
            }
            Console.WriteLine();
        }

        for (int j = 0; j < array.GetLength(1); j++)
        {
            sumOfSecondRow += array[1, j];
        }

        for (int i = 0; i < array.GetLength(0); i++)
        {
            productOfFirstColumn *= array[i, 0];
        }

        Console.WriteLine("Сумма второй строки: " + sumOfSecondRow);
        Console.WriteLine("Произведение первого столбца: " + productOfFirstColumn);
    }
}