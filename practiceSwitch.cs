using System;
using System.Net.Http.Headers;
using System.Threading;

class Program
{
    static void Main()
    {
        Random random = new Random();

        int[] numbers = { 1, 2, 3, 4, 5 };

        Console.WriteLine("\nМассив до рандома:\n");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i]);
        }

        Console.WriteLine();
        Shuffle(ref numbers, random);

        Console.WriteLine("\nМассив после рандома:\n");

        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i]);
        }
    }

    static void Shuffle(ref int[] numbers, Random random)
    {
        for (int i = 0; i < numbers.Length; i++)
        {
            int randomIndex = random.Next(0, numbers.Length - 1);

            int tempElement = numbers[randomIndex];
            numbers[randomIndex] = numbers[i];
            numbers[i] = tempElement;
        }
    }
}