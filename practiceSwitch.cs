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

        int walls = 2;
        int lineLength = name.Length + walls;
        int strokeLenght = 3;

        for (int i = 0; i < strokeLenght; i++)
        {
            if (i == 1)
            {
                Console.Write(symbol);
                Console.Write(name);
                Console.WriteLine(symbol);
            }
            else
            {
                Console.WriteLine(new string(symbol, lineLength));
            }
        }
    }
}