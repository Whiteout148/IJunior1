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

            Console.Write("Введите секретное сообщение:");
            secretMessage = Console.ReadLine();

            Console.Write("Создайте пароль:");
            password = Console.ReadLine();

            Console.WriteLine("Пароль создан!");

            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine("\nВведите пароль чтобы получить кодовое сообщение");
                userInput = Console.ReadLine();

                if(userInput == password)
                {
                    Console.WriteLine($"Пароль верный! кодовое сообщение: {secretMessage}");
                }
                else
                {
                    Console.WriteLine($"Пароль не верный! осталось попыток: {i}");
                }
            }

            Console.WriteLine("закончились попытки");
        }
    }
}
