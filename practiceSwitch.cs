using System;
using System.Net.Http.Headers;
using System.Threading;

class Program
{
    static void Main()
    {
        const int CommandForAddNumber = 1;
        const int CommandForCheckSum = 2;
        const int CommandForExit = 3;

        int sum = 0;

        int userNumber;
        int userNumbersSum = 0;

        int[] numbers = new int[1];
        int[] tempNumbers;

        bool isWork = true;

        int oneSecondWithMs = 3000;

        while (isWork)
        {
            Console.WriteLine("** Программа для вычисление всех написанных чисел **\n");
            Console.WriteLine($"Команда для добавление числа в сумму: {CommandForAddNumber}");
            Console.WriteLine($"Команда для показа суммы всех чисел: {CommandForCheckSum}");
            Console.WriteLine($"Команда для выхода из программы: {CommandForExit}");

            switch(Convert.ToInt32(Console.ReadLine()))
            {
                case CommandForAddNumber:
                    Console.Write("Введите число которое хотите добавить: ");
                    userNumber = Convert.ToInt32(Console.ReadLine());
                    userNumbersSum++;
                    tempNumbers = new int[userNumbersSum];

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        tempNumbers[i] = numbers[i];
                    }

                    tempNumbers[tempNumbers.Length - 1] = userNumber;
                    numbers = tempNumbers;

                    Console.WriteLine("Ваши числа: ");

                    for (int i = 0; i < numbers.Length; i++)
                    {
                        Console.WriteLine(numbers[i]);
                    }
                    break;

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
                    Console.WriteLine("Выход из программы.");
                    Thread.Sleep(oneSecondWithMs);
                    isWork = false;
                    break;

                default:
                    Console.WriteLine("Нету такой команды.");
                    break;
            }

            Console.ReadKey();
            Console.Clear();
        }
    }
}