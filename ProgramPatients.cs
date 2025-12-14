using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            Server server = new Server();

            server.Work();
        }
    }

    class Server
    {
        private const int LiderCount = 3;
        private List<Player> _players = new List<Player>();

        public Server()
        {
            _players.Add(new Player("Ivanzolo2004", 50, 34));
            _players.Add(new Player("ZxProshnik_1", 60, 90));
            _players.Add(new Player("XXXnoname", 60, 80));
            _players.Add(new Player("Pivnoye_Payzuri", 70, 50));
            _players.Add(new Player("LOX123", 80, 50));
            _players.Add(new Player("S1mple", 46, 30));
            _players.Add(new Player("4u4elo", 96, 10));
            _players.Add(new Player("Kisselka04", 67, 80));
            _players.Add(new Player("RaveBoy1999", 93, 30));
            _players.Add(new Player("Niga2", 24, 56));
        }

        public void Work()
        {
            const string SearchTopForceCommand = "1";
            const string SearchTopLevelCommand = "2";
            const string ExitCommand = "3";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Игроки сервера: ");
                UserUtils.ShowCollection(_players);
                Console.WriteLine($"Узнать топ 3 игроков по силе: {SearchTopForceCommand}");
                Console.WriteLine($"Узнать топ 3 игроков по уровню: {SearchTopLevelCommand}");
                Console.WriteLine($"Выход: {ExitCommand}");

                switch (Console.ReadLine())
                {
                    case SearchTopLevelCommand:
                        ShowWithLevel();
                        break;

                    case SearchTopForceCommand:
                        ShowWithForce();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Нету такой команды.");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void ShowWithForce()
        {
            var filteredPlayers = _players.OrderByDescending(player => player.Force).ToList();

            Console.WriteLine("Топ 3 игроков по силе:");
            Console.WriteLine();
            UserUtils.ShowWithCount(filteredPlayers, LiderCount);
        }

        private void ShowWithLevel()
        {
            var filteredPlayers = _players.OrderByDescending(player => player.Level).ToList();

            Console.WriteLine("Топ 3 игроков по уровню:");
            Console.WriteLine();
            UserUtils.ShowWithCount(filteredPlayers, LiderCount);
        }
    }

    class Player
    {
        public Player(string name, int level, int force)
        {
            Name = name;
            Level = level;
            Force = force;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Force { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя: {Name} Уровень: {Level} Сила: {Force}");
        }

        public Player GetClone()
        {
            return new Player(Name, Level, Force);
        }
    }

    static class UserUtils
    {
        public static void ShowWithCount(List<Player> collection, int count)
        {
            for (int i = 0; i < count; i++)
            {
                collection[i].ShowInfo();
            }
        }

        public static void ShowCollection(List<Player> collection)
        {
            Console.WriteLine();

            for (int i = 0; i < collection.Count; i++)
            {
                collection[i].ShowInfo();
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public static List<Player> GetCloneCollection(List<Player> collection)
        {
            List<Player> newCollection = new List<Player>();

            for (int i = 0; i < collection.Count; i++)
            {
                newCollection.Add(collection[i].GetClone());
            }

            return newCollection;
        }
    }
}