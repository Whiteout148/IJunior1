using System;

class Program
{
    static void Main()
    {
        int[,] array = new int[3, 3];
        Random random = new Random();

        int maxRandomNumber = 10;
        int sumOfShelf = 0;
        int productOfColumn = 1;

        int indexOfShelf = 1;
        int indexOfColumn = 0;

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                array[i, j] = random.Next(0, maxRandomNumber + 1);
                Console.Write(array[i, j] + " | ");
            }

            Console.WriteLine();
        }

        for (int i = 0; i < array.GetLength(1); i++)
        {
            sumOfShelf += array[indexOfShelf, i];
        }

        for (int i = 0; i < array.GetLength(0); i++)
        {
            productOfColumn *= array[i, indexOfColumn];
        }

        Console.WriteLine("Сумма второй строки: " + sumOfShelf);
        Console.WriteLine("Произведение первого столбца: " + productOfColumn);
        Console.ReadKey();
    }
}