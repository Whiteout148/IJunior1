using System;

namespace KurwaTSIKLI
{
    internal class practiceSwitch
    {
        static void Main()
        {
            string userSymbol;
            string userName;

            int x;
            int y;

            Console.WriteLine("Введите имя: ");
            userName = Console.ReadLine();

            Console.WriteLine("\nВведите символ: ");
            userSymbol = Console.ReadLine();

            Console.WriteLine("\n\n\n");

            for(x = 0; x < userName.Length + 2; x++)
            {
                Console.Write(userSymbol);
            }
            Console.WriteLine("\n" + userSymbol + userName + userSymbol);
            for (x = 0; x < userName.Length + 2; x++)
            {
                Console.Write(userSymbol);
            }

            Console.ReadKey();
        }
    }
}
