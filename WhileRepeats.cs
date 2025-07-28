using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            int firstNumber = 5;
            int lastNumber = 789;

            Console.WriteLine(firstNumber);

            while (firstNumber < lastNumber)
            {
                firstNumber += 7;

                Console.WriteLine(firstNumber);
            }
        }
    }
}
