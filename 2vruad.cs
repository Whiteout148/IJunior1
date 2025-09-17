using System;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            Player fighter = new Player(3, 2, '@');
            Player businessMan = new Player(5, 6, '$');

            Renderer render = new Renderer();

            render.DrawPlayer(fighter);
            render.DrawPlayer(businessMan);
        }
    }

    class Player
    {
        public Player(int positionX, int positionY, char skin)
        {
            PositionX = positionX;
            PositionY = positionY;
            Skin = skin;
        }

        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public char Skin { get; private set; }
    }

    class Renderer
    {
        public void DrawPlayer(Player player)
        {
            Console.SetCursorPosition(player.PositionX, player.PositionY);
            Console.Write(player.Skin);
        }
    }
}
