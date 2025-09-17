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
        public int PositionX { get; private set; }
        public int PositionY { get; private set; }
        public char Skin { get; private set; }

        public Player(int positionX, int positionY, char skin)
        {
            PositionX = positionX;
            PositionY = positionY;
            Skin = skin;
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
            Console.SetCursorPosition(_player.PositionX, _player.PositionY);
            Console.Write(_player.Skin);
        }
    }
}
