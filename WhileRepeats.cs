using System;
using System.Data;
using System.Linq;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            int[] array = new int[30];

            int minRandomNumber = -100;
            int maxRandomNumber = 100;

            int firstElementIndex = 0;
            int lastElementIndex = array.Length - 1;

            Random random = new Random();

            for(int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRandomNumber, maxRandomNumber + 1);
            }

            if (array[firstElementIndex] > array[firstElementIndex + 1])
            {
                Console.WriteLine(array[firstElementIndex]);
            }
            
            for (int i = 1; i < array.Length - 1; i++)
            {
                int rightNeighbourIndex = i + 1;
                int leftNeighbourIndex = i - 1;

                if (array[i] > array[leftNeighbourIndex] && array[i] > array[rightNeighbourIndex])
                {
                    Console.WriteLine(array[i]);
                }
            }

            if (array[lastElementIndex] > array[lastElementIndex - 1])
            {
                Console.WriteLine(array[lastElementIndex]);
            }

            Console.WriteLine();
            Console.WriteLine("Весь массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " | ");
            }
        }
    }
}
