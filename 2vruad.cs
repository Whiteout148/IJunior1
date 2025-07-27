using System;

namespace _3_vryad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int album = 52;
            int row = 3;

            int rowNumber = album / row;
            int remainder = album % row;

            Console.WriteLine($"Количество рядов {rowNumber} остаток фотографий {remainder}");

            Console.ReadLine();
        }
    }
}
