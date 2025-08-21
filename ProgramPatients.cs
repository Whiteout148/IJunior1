using System;

class Program
{
    static void Main()
    {
        string input = "(()(()))";

        char openBracket = '(';
        char closeBracket = ')';

        int currentDepth = 0;
        int maxDepth = 0;
        bool isCorrect = true;

        int length = input.Length;

        for (int index = 0; index < length; index = index + 1)
        {
            char currentChar = input[index];

            if (currentChar == openBracket)
            {
                currentDepth = currentDepth + 1;

                if (currentDepth > maxDepth)
                {
                    maxDepth = currentDepth;
                }
            }

            if (currentChar == closeBracket)
            {
                currentDepth = currentDepth - 1;

                if (currentDepth < 0)
                {
                    isCorrect = false;
                }
            }
        }

        if (currentDepth != 0)
        {
            isCorrect = false;
        }

        if (isCorrect)
        {
            Console.WriteLine("Строка корректна.");
            Console.WriteLine("Максимальная глубина вложенности: " + maxDepth);
        }
        else
        {
            Console.WriteLine("Строка некорректна.");
        }
    }
}
