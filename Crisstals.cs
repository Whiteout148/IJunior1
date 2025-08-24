using System;

internal class Crisstals
{
    static void Main(string[] args)
    {
        string userInput;

        int resultNumber;

        ReadInt(out userInput, out resultNumber);
    }

    static void ReadInt(out string userInput, out int resultNumber)
    {
        while (true)
        {
            Console.Write("Введите число: ");
            userInput = Console.ReadLine();

            if(int.TryParse(userInput, out resultNumber))
            {
                Console.WriteLine("Число удалось конвертировать");
                Console.WriteLine("Ваше число: " + resultNumber);

                break;
            }
            else
            {
                Console.WriteLine("Число не удалось конвертировать");
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}

