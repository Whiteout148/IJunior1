using System;
using System.Threading;

namespace KurwaTSIKLI
{
    internal class practiceSwitch
    {
        static void Main(string[] args)
        {
            const string CommandForGetMail = "1";
            const string CommandForGetPhoneNumber = "2";
            const string CommandForGetRandomCode = "3";
            const string CommandForClearConsol = "4";
            const string CommandForExit = "5";

            string userInput;
            string phoneNumber = "7034340583";
            string mail = "vladimirkovsky@gmail.com";

            int randomAccessCode;
            int initialNumberInCode = 100;
            int lastNumberInCode = 998;

            Random random = new Random();

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("\nСлужба поддержки нашей программы, выберите номер команды для операции: ");
                Console.WriteLine($"\n{CommandForGetMail} - Получить доступ к электронной почте");
                Console.WriteLine($"\n{CommandForGetPhoneNumber} - Получить доступ к номеру телефона");
                Console.WriteLine($"\n{CommandForGetRandomCode} - Сгенерировать рандомный код доступа");
                Console.WriteLine($"\n{CommandForClearConsol} - Очистить консоль");
                Console.WriteLine($"\n{CommandForExit} - Выход из программы");

                userInput = Console.ReadLine();

                switch(userInput)
                {
                    case CommandForGetMail:
                        Console.WriteLine($"Ваша электронная почта: {mail}");
                        break;

                    case CommandForGetPhoneNumber:
                        Console.WriteLine($"Ваш номер телефона: {phoneNumber}");
                        break;

                    case CommandForGetRandomCode:
                        randomAccessCode = random.Next(initialNumberInCode, lastNumberInCode + 1);

                        Console.WriteLine($"Ваш код доступа: {randomAccessCode}");
                        break;

                    case CommandForClearConsol:
                        Console.Clear();
                        break;

                    case CommandForExit:
                        Console.Clear();
                        Console.WriteLine("Вы вышли из программы");

                        isWorking = false;
                        break;

                    default:
                        Console.WriteLine("Неверный ввод команды!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
