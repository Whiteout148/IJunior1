using System;

namespace _3_vryad
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int photoAlbum = 52;
            int rowWithPhotographs = 3;

            int rowInPhotographsNumber = photoAlbum / rowWithPhotographs;
            int remainderPhotographs = photoAlbum % rowWithPhotographs;

            Console.WriteLine($"Количество рядов в ряду {rowInPhotographsNumber} остаток фотографий {remainderPhotographs}");

            Console.ReadLine();
        }
    }
}
