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
            int maxRandomNumber = 100;
            int neighbourIndex = 1;
            int minRandomNumber = -100;

            Random random = new Random();

            for(int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(minRandomNumber, maxRandomNumber);
            }

            for (int i = 0; i < array.Length; i++)
            {
                if (i == 0)
                {
                    if (array[i] > array[i + neighbourIndex])
                    {
                        Console.WriteLine(array[i]);
                    }
                }
                else if(i == array.Length - 1)
                {
                    if (array[i] > array[i - neighbourIndex])
                    {
                        Console.WriteLine(array[i]);
                    }
                }
                else
                {
                    if (array[i] > array[i - neighbourIndex] && array[i] > array[i + neighbourIndex])
                    {
                        Console.WriteLine(array[i]);
                    }
                }
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
