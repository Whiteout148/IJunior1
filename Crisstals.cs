using System;

internal class Crisstals
{
    static void Main(string[] args)
    {
        string userInput;

        int resultNumber;

        bool isWork = true;
        bool isConverted = false;

        while (isWork)
        {
            Console.WriteLine("Введите число для конвертации в int");
            userInput = Console.ReadLine();

            isConverted = ReadInt(userInput, out resultNumber);

            if (isConverted)
            {
                isWork = false;
            }
            
            Console.ReadKey();
            Console.Clear();
        }
    }

    static bool ReadInt(string userInput, out int resultNumber)
    {
        if (int.TryParse(userInput, out resultNumber))
        {
            Console.WriteLine("Конвертация удалась!");
            Console.WriteLine("Ваше число: " + resultNumber);

            return true;
        }
        else
        {
            Console.WriteLine("Не удалось конвертировать в int");

            return false;
        }
    }
}

