using System;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            Player fighter = new Player(3, 2, '@');

            Renderer render = new Renderer(fighter);

            render.DrawPlayer();
        }
    }

    class Player
    {
        public int PosX { get; private set; }
        public int PosY { get; private set; }

        public char PlayerSkin { get; private set; }

        public Player(int posX, int posY, char playerSkin)
        {
            PosX = posX;
            PosY = posY;
            PlayerSkin = playerSkin;
        }
    }

    class Renderer
    {
        public Player Player { get; private set; }

        public Renderer(Player player)
        {
            Player = player;
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(Player.PosX, Player.PosY);
            Console.Write(Player.PlayerSkin);
        }
    }
}
