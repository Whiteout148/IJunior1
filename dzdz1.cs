using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            const string CommandForPrintSum = "sum";
            const string CommandForExit = "exit";

            List<int> numbers = new List<int>();

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("** Программа для добавление элементов **");
                Console.WriteLine($"Команда для получение суммы всех элементов: {CommandForPrintSum}");
                Console.WriteLine($"Команда для выхода: {CommandForExit}");
                Console.Write("Введите команду или целое число: ");
                string userInput = Console.ReadLine();


                switch (userInput)
                {
                    case CommandForPrintSum:
                        PrintSum(numbers);
                        break;

                    case CommandForExit:
                        isWork = false;
                        break;

                    default:
                        AddNumber(numbers, userInput);
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        static List<int> AddNumber(List<int> numbers, string userInput)
        {
            int resultNumber;

            if (int.TryParse(userInput, out resultNumber))
            {
                numbers.Add(resultNumber);
                Console.WriteLine("Добавлено число: " + resultNumber);
            }
            else
            {
                Console.WriteLine("Неправильная команда");
            }

            return numbers;
        }

        static void PrintSum(List<int> numbers)
        {
            int sum = 0;

            for (int i = 0; i < numbers.Count; i++)
                sum += numbers[i];

            Console.WriteLine("Cумма всех элементов: " + sum);
        }
    }
}
