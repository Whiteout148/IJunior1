using System;
using System.Net.Http.Headers;
using System.Threading;

class Program
{
    static void Main()
    {
        const int CommandForCheckSum = 1;
        const int CommandForExit = 2;

        int sum = 0;

        int userInput;
        int userNumbersSum = 0;

        int[] numbers = new int[0];
        int[] tempNumbers;

        bool isWork = true;

        while (isWork)
        {
            Console.WriteLine("** Программа для вычисление всех написанных чисел **\n");
            Console.WriteLine("\nВаши числа: ");

            for (int i = 0; i < numbers.Length; i++)
            {
                Console.WriteLine(numbers[i]);
            }

            Console.WriteLine($"Команда для показа суммы всех чисел: {CommandForCheckSum}");
            Console.WriteLine($"Команда для выхода из программы: {CommandForExit}");
            userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
            {
                case CommandForCheckSum:
                    Console.WriteLine("Ваши числа: \n");

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        Console.WriteLine(numbers[i]);
                        sum += numbers[i];
                    }

                    Console.WriteLine("\nСумма всех чисел: " + sum);
                    break;

                case CommandForExit:
                    isWork = false;
                    break;

                default:
                    userNumbersSum++;
                    tempNumbers = new int[userNumbersSum];

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        tempNumbers[i] = numbers[i];
                    }

                    tempNumbers[tempNumbers.Length - 1] = userInput;
                    numbers = tempNumbers;
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}