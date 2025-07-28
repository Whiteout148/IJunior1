using System;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            int initialNumber = 5;
            int lastNumber = 789;

            for(int i = initialNumber; i <= lastNumber; i += 7)
            {
                Console.WriteLine(i);
            }
        }
    }
}
