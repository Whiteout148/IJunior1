using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KurwaTSIKLI
{
    internal class practiceSwitch
    {
        static void Main(string[] args)
        {
            string userInput;
            string phoneNumber = "7034340583";
            string mail = "vladimirkovsky@gmail.com";
            int accessCode;

            Random random = new Random();

            bool isWorking = true;

            while (isWorking)
            {
                Console.WriteLine("\nСлужба поддержки нашей программы, выберите номер команды для операции: ");
                Console.WriteLine("\n1 - Получить доступ к электронной почте");
                Console.WriteLine("\n2 - Получить доступ к номеру телефона");
                Console.WriteLine("\n3 - Сгенерировать рандомный код доступа");
                Console.WriteLine("\n4 - Очистить консоль");
                Console.WriteLine("\n5 - Выход из программы");

                userInput = Console.ReadLine();
                switch(userInput)
                {
                    case "1":
                        Console.WriteLine($"Ваша электронная почта: {mail}");
                        break;
                    case "2":
                        Console.WriteLine($"Ваш номер телефона: {phoneNumber}");
                        break;
                    case "3":
                        accessCode = random.Next(100, 999);

                        Console.WriteLine($"Ваш код доступа: {accessCode}");
                        break;
                    case "4":
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        Console.WriteLine("Выход из программы");
                        Thread.Sleep(400);
                        Console.Clear();
                        Console.WriteLine("Выход из программы.");
                        Thread.Sleep(400);
                        Console.Clear();
                        Console.WriteLine("Выход из программы..");
                        Thread.Sleep(400);
                        Console.Clear();
                        Console.WriteLine("Выход из программы...");
                        Thread.Sleep(300);
                        Console.Clear();
                        isWorking = false;
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
