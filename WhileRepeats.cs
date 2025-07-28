using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            bool isWork = true;
            string messageForExit = "exit";

            while (isWork)
            {
                Console.WriteLine($"Введите слово {messageForExit} для выхода");

                Console.ReadKey();
                string userMessage = Console.ReadLine();

                if(userMessage == messageForExit)
                {
                    Console.WriteLine("Выход из программы...");
                    isWork = false;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
