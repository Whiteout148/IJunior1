using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string name;
            string surname;
            string favouriteDrink;
            int age;

            Console.WriteLine("Введите имя: ");
            name = Console.ReadLine();
            Console.WriteLine("Введите фамилию: ");
            surname = Console.ReadLine();
            Console.WriteLine("Введите возраст: ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите любимый напиток: ");
            favouriteDrink = Console.ReadLine();

            Console.WriteLine($"Вы {name} у вас фамилия {surname} вам {age} лет и у вас любимый напиток это {favouriteDrink}");
        }
    }
}
