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
            int gold;
            int amethysts;
            int amethystsPrice = 10;

            Console.WriteLine("Введите количество золото: ");
            gold = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество аметистов которые хотите купить: ");
            amethysts = Convert.ToInt32(Console.ReadLine());
            int resultPriсe = amethysts * amethystsPrice;
            gold -= resultPriсe;

            Console.WriteLine($"Вы купили {amethysts} аметистов по цене {resultPriсe}. у вас осталось {gold} золото");

            Console.ReadKey();
        }
    }
}
