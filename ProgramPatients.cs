using System;
using System.Security.Principal;

namespace XDproject
{
    internal class ProgramPatients
    {
        static void Main()
        {
            string message = "Привет как дела?";
            string[] dividedMessageToWords = message.Split(' ');

            Console.WriteLine("\nСообщение:\n");
            Console.WriteLine(message);
            Console.WriteLine("\nСообщение разделенное на слова:\n");

            for (int i = 0; i < dividedMessageToWords.Length; i++)
            {
                Console.WriteLine(dividedMessageToWords[i]);
            }
        }
    }
}
