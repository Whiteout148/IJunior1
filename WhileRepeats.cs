using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            int initialNumber = 5;
            int lastNumber = 789;

            Console.WriteLine(initialNumber);

            while (initialNumber < lastNumber)
            {
                initialNumber += 7;

                Console.WriteLine(initialNumber);
            }
        }
    }
}
