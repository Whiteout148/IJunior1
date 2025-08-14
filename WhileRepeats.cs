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

            int firstElement = 0;
            int lastElement = array.Length - 1;

            int leftNeighbourIndex = array[lastElement - 1];
            int rightNeighbourIndex = array[firstElement + 1];

            Random random = new Random();


            for(int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRandomNumber, maxRandomNumber + 1);
            }

            if (array[firstElement] > array[rightNeighbourIndex])
            {
                Console.WriteLine(array[firstElement]);
            }
            
            for (int i = 1; i < array.Length - 1; i++)
            {
                rightNeighbourIndex = i + 1;
                leftNeighbourIndex = i - 1;

                if (array[i] > array[leftNeighbourIndex] && array[i] > array[rightNeighbourIndex])
                {
                    Console.WriteLine(array[i]);
                }
            }

            if (array[lastElement] > array[leftNeighbourIndex])
            {
                Console.WriteLine(array[lastElement]);
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
