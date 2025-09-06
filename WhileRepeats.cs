using System;
using System.Data;
using System.Linq;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            char[,] map =
            {
                { '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#', },
                { '#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                {'#',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                {'#',' ',' ',' ',' ',' ','#',' ',' ',' ','c',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                {'#',' ',' ','c',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','a',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','c',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ','a',' ',' ',' ',' ',' ',' ','a',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ','a',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#','#','#',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', } ,
                { '#',' ',' ',' ',' ',' ',' ','c',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ','c',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ','a',' ',' ','#', },
                { '#',' ',' ',' ',' ','c',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ','#', },
                { '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#','#', },
            };

            Console.CursorVisible = false;

            bool isPlaying = true;
            char player = '@';

            int posX = 1, posY = 1;
            int points = 0;

            ConsoleKeyInfo pressedKey = new ConsoleKeyInfo('w', ConsoleKey.W, false, false, false);

            while (isPlaying)
            {
                DrawMap(map);

                HandleInput(pressedKey, ref posX, ref posY, map);
                Console.SetCursorPosition(posY, posX);
                Console.Write(player);
                points = GetPointsWithGatherObjects(posX, posY, ref map, points);

                pressedKey = Console.ReadKey();
                Console.Clear();
            }
        }

        static void HandleInput(ConsoleKeyInfo pressedKey, ref int posX, ref int posY, char[,] map)
        {
            int[] direction = GetDirection(pressedKey);

            int nextPositionX = posX + direction[0];
            int nextPositionY = posY + direction[1];

            if (map[nextPositionX, nextPositionY] != '#')
            {
                posX = nextPositionX;
                posY = nextPositionY;
            }
        }

        static int GetPointsWithGatherObjects(int posX, int posY, ref char[,] map, int points)
        {
            const char Fruit = 'a';
            const char Candy = 'c';

            int priceToCandy = 5;
            int priceToFruit = 3;

            int[] pointsCordinate = { 25, 0 };

            switch (map[posX, posY])
            {
                case Fruit:
                    points = AddPoint(points, priceToFruit, posX, posY, ref map);
                    break;
                
                case Candy:
                    points = AddPoint(points, priceToCandy, posX, posY, ref map);
                    break;
            }

            Console.SetCursorPosition(pointsCordinate[0], pointsCordinate[1]);
            Console.Write("Points: " + points);

            return points;
        }

        static int AddPoint(int points, int priceToAdd, int posX, int posY, ref char[,] map)
        {
            points += priceToAdd;
            map[posX, posY] = ' ';

            return points;
        }

        static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            if (pressedKey.Key == ConsoleKey.W)
                direction[0] -= 1;
            if (pressedKey.Key == ConsoleKey.S)
                direction[0] += 1;
            if (pressedKey.Key == ConsoleKey.D)
                direction[1] += 1;
            if (pressedKey.Key == ConsoleKey.A)
                direction[1] -= 1;

            return direction;
        }

        static void DrawMap(char[,] map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }

                Console.WriteLine();
            }
        }

    }
}
