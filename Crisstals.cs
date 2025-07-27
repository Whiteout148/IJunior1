using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Домашка_кристалы
{
    internal class Crisstals
    {
        static void Main(string[] args)
        {
            int gold;
            int amethysts;
            int amethystsPrice = 10;

            Console.WriteLine("Сколько у вас золото?");
            gold = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество аметистов которые хотите купить");
            amethysts = Convert.ToInt32(Console.ReadLine());

            int resultPrice = amethysts * amethystsPrice;
            gold -= resultPrice;

            Console.WriteLine($"Вы купили {amethysts} аметистов за цену {resultPrice} золото и осталось у вас {gold} золото.");
            Console.ReadLine();
        }
    }
}
