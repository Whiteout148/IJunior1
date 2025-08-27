using System;
using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using System.Threading;

class Program
{
    static void Main()
    {
        Random random = new Random();

        int[] numbers = { 1, 2, 3, 4, 5 };

        PrintArrayAndMessage(numbers, "Массив до рандома");

        Console.WriteLine();
        Shuffle(numbers, random);

        PrintArrayAndMessage(numbers, "Массив после рандома");
    }

    static void Shuffle(int[] numbers, Random random)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int randomIndex = random.Next(0, numbers.Length);

            int tempElement = numbers[randomIndex];
            numbers[randomIndex] = numbers[i];
            numbers[i] = tempElement;
        }
    }

    static void PrintArrayAndMessage(int[] numbers, string message)
    {
        Console.WriteLine($"\n{message}\n");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i]);
        }
    }
}