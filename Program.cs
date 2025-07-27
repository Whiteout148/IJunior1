using System;
using System.Collections.Generic;
using System.Linq;
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
            int age;
            string favouriteDrink;

            Console.Write("Введите имя: ");
            name = Console.ReadLine();
            Console.Write("\nВведите фамилию: ");
            surname = Console.ReadLine();
            Console.Write("\nВведите возраст: ");
            age = Convert.ToInt32(Console.ReadLine());
            Console.Write("\nВведите любимый напиток: ");
            favouriteDrink = Console.ReadLine();

            Console.WriteLine($"\nИмя {name}, Фамилия {surname}, возраст {age}, любимый напиток {favouriteDrink}");

            Console.ReadKey();
        }
    }
}
