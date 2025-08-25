using System;

internal class Crisstals
{
    static void Main(string[] args)
    {
        int resultNumber;

        bool isConverted;

        resultNumber = ReadInt(out isConverted);

        Console.WriteLine("Число удалось конвертировать\nВаше число:");
        Console.WriteLine(resultNumber);
    }

    static int ReadInt(out bool isConverted)
    {
        string userInput = "";

        int resultNumber = 0;

        isConverted = true;

        while (isConverted = false)
        {
            Console.WriteLine("Введите число для конвертации");
            userInput = Console.ReadLine();

            if (int.TryParse(userInput, out resultNumber))
            {
                isConverted = true;
            }
            else
            {
                Console.WriteLine("Не получилось конвертировать");
            }

            Console.ReadKey();
            Console.Clear();
        }

        return resultNumber;
    }
}

