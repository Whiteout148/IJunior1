using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            int initialNumber = 5;
            int lastNumber = 789;
            int subsequenceCount = 7;

            for (int i = initialNumber; i <= lastNumber; i += subsequenceCount)
            {
                Console.WriteLine(i);
            }
        }
    }
}
