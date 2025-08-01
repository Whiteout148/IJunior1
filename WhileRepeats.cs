using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            string password;
            string userInput;
            string secretMessage;

            int attempts = 3;

            Console.Write("Введите секретное сообщение:");
            secretMessage = Console.ReadLine();

            Console.Write("Создайте пароль:");
            password = Console.ReadLine();

            Console.WriteLine("Пароль создан!");

            while (attempts != 0)
            {
                Console.WriteLine($"\nВведите пароль");
                userInput = Console.ReadLine();

                if (userInput == password)
                {
                    Console.WriteLine("Пароль верный!");
                    Console.WriteLine($"Секретное сообщение: {secretMessage}");
                }
                else
                {
                    attempts--;
                    if(attempts == 0)
                    {
                        Console.WriteLine("Попытки закончились, программма закрывается");
                    }
                    else
                    {
                        Console.Write($"Неверный пароль! попробуйте снова, осталось попыток: {attempts}");
                    }
                }
            }
        }
    }
}
