using System;
using System.Data;
using System.Linq;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            float maxHealth = 50;

            float procentWithHealth = 0.5f;

            int posX = 1;
            int posY = 1;

            DrawBar(procentWithHealth, maxHealth, posX, posY, ConsoleColor.Red);
        }

        static void DrawBar(float procentWithMaxValue, float maxValue, int positionX, int positionY, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            char openBracket = '[';
            char closeBracket = ']';

            float maxProcent = 1f;

            if (procentWithMaxValue > 1)
            {
                procentWithMaxValue = maxProcent;
            }

            float activeBarLenght = procentWithMaxValue * maxValue;

            string bar = " ";

            Console.SetCursorPosition(positionX, positionY);

            Console.Write(openBracket);

            CreateBarPart(activeBarLenght, bar, ' ', ConsoleColor.Red);

            CreateBarPart(maxValue, bar, ' ', ConsoleColor.Black, activeBarLenght);

            Console.Write(closeBracket);
        }

        static string CreateBarPart(float maxValue, string bar, char symbol, ConsoleColor color, float startValue = 0)
        {
            for (float i = startValue; i < maxValue; i++)
            {
                bar += symbol;
            }

            Console.BackgroundColor = color;
            Console.Write(bar);

            return bar;
        }
    }
}
