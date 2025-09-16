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
        private Player _player;

        public Renderer(Player player)
        {
            _player = player;
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(_player.PosX, _player.PosY);
            Console.Write(_player.PlayerSkin);
        }
    }
}
