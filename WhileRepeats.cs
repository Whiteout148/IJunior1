using System;
using System.Data;
using System.Linq;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            int maxHealth = 50;

            float procentWithHealth = 0.5f;

            int posX = 1;
            int posY = 1;

            DrawBar(procentWithHealth, maxHealth, posX, posY, ConsoleColor.Red);
        }

        static void DrawBar(float procentWithMaxValue, int maxValue, int positionX, int positionY, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            char openBracket = '[';
            char closeBracket = ']';

            float activeBarLenght = procentWithMaxValue * maxValue;

            string bar = " ";

            Console.SetCursorPosition(positionX, positionY);

            for (int i = 0; i < activeBarLenght; i++)
            {
                bar += " ";
            }

            Console.Write(openBracket);
            Console.BackgroundColor = color;
            Console.Write(bar);

            bar = null;

            for (float i = activeBarLenght; i < maxValue; i++)
            {
                bar += " ";
            }

            Console.BackgroundColor = defaultColor;
            Console.Write(bar);
            Console.Write(closeBracket);
        }
    }
}
