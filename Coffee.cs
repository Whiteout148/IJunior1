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

            Console.WriteLine($"Имя: {name}\nФамилия: {surname}\nБудете пить {tea}? или же кофе {coffee}");

            name = "Владимир";
            surname = "Буянов";

            tea = "Индийский чай";
            coffee = "капучино";

            Console.WriteLine($"\nИмя: {name}\nФамилия: {surname}\nБудете пить {tea}? или же кофе {coffee}");

            Console.ReadKey();
        }
    }
}
