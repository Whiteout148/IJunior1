using System;

class Program
{
    static void Main()
    {
        int[,] array = new int[3, 3];
        Random random = new Random();

        int maxRandomNumber = 10;
        int sumOfSecondShelf = 0;
        int productOfFirstColumn = 1;

        int secondShelf = 1;
        int firstColumn = 0;

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
            sumOfSecondShelf += array[secondShelf, j];
        }

        for (int i = 0; i < array.GetLength(0); i++)
        {
            productOfFirstColumn *= array[i, firstColumn];
        }

        Console.WriteLine("Сумма второй строки: " + sumOfSecondShelf);
        Console.WriteLine("Произведение первого столбца: " + productOfFirstColumn);
        Console.ReadKey();
    }
}