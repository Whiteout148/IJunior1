using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace @switch
{
    internal class Coffee
    {
        static void Main(string[] args)
        {
            string name = "Буянов";
            string surname = "Владимир";

            string tea = "капучино";
            string coffee = "индийский чай";

            Console.WriteLine($"Имя: {name}\nФамилия: {surname}\nБудете пить {tea}? или же кофе {coffee}?");

            string lateName = name;
            name = surname;
            surname = lateName;

            string lateDrinkName = tea;
            tea = coffee;
            coffee = lateDrinkName;

            Console.WriteLine($"\nИмя: {name}\nФамилия: {surname}\nБудете пить {tea}? или же кофе {coffee}?");

            Console.ReadKey();
        }
    }
}
