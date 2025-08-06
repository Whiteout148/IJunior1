using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите символ: ");
        char symbol = Console.ReadKey().KeyChar;
        Console.WriteLine();

        Console.Write("Введите имя: ");
        string name = Console.ReadLine();

        string middleLine = symbol + name + symbol;
 
        string frameLine = new string(symbol, middleLine.Length);

        Console.WriteLine(frameLine);
        Console.WriteLine(middleLine);
        Console.WriteLine(frameLine);
    }
}