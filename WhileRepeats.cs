using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            bool isOpenTheProgram = true;

            while (isOpenTheProgram)
            {
                Console.WriteLine("Введите слово exit для выхода");

                Console.ReadKey();
                string userMessage = Console.ReadLine();

                if(userMessage == "exit")
                {
                    Console.WriteLine("Выход из программы...");
                    isOpenTheProgram = false;
                }
                else
                {
                    continue;
                }
            }
        }
    }
}
