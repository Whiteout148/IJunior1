using System;

namespace _3_vryad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int PhotoAlbum = 52;
            int rowWithPhotographs = 3;

            int rowInPhotographsNumber = PhotoAlbum / rowWithPhotographs;
            int remainderPhotographs = PhotoAlbum % rowWithPhotographs;

            Console.WriteLine($"Количество рядов в ряду {rowInPhotographsNumber} остаток фотографий {remainderPhotographs}");

            Console.ReadLine();
        }
    }
}
