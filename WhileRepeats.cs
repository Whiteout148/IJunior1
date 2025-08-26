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

            float percentWithHealth = 0.5f;

            int posX = 1;
            int posY = 1;

            DrawBar(percentWithHealth, maxHealth, posX, posY, ConsoleColor.Red);
        }

        static void DrawBar(float percentWithMaxValue, float maxValue, int positionX, int positionY, ConsoleColor color)
        {
            ConsoleColor defaultColor = Console.BackgroundColor;
            char openBracket = '[';
            char closeBracket = ']';

            float maxProcent = 1f;

            if (percentWithMaxValue > 1)
            {
                percentWithMaxValue = maxProcent;
            }

            float activeBarLenght = percentWithMaxValue * maxValue;

            string bar = " ";

            Console.SetCursorPosition(positionX, positionY);

            Console.Write(openBracket);

            bar = CreateBarPart(activeBarLenght, bar, ' ');

            SetColorAndDraw(bar, color);

            CreateBarPart(maxValue, bar, ' ', activeBarLenght);

            SetColorAndDraw(bar, defaultColor);

            Console.Write(closeBracket);
        }

        static string CreateBarPart(float maxValue, string bar, char symbol, float startValue = 0)
        {
            for (float i = startValue; i < maxValue; i++)
            {
                bar += symbol;
            }

            return bar;
        }

        static void SetColorAndDraw(string bar, ConsoleColor color)
        {
            Console.BackgroundColor = color;
            Console.Write(bar);
        }
    }
}
