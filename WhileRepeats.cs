using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main(string[] args)
        {
            int repeatsNumber;
            string userMessage;

            Console.Write("Введите сообщение которое хотите повторить: ");
            userMessage = Console.ReadLine();
            Console.Write("\nВведите количество повторов: ");
            repeatsNumber = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < repeatsNumber; i++)
            {
                Console.WriteLine("\n" + userMessage);
            }
        }
    }
}
