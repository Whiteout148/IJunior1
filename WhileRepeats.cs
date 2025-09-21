using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            DataBases databases = new DataBases();

            databases.Work();
        }
    }

    class DataBases
    {
        private List<Player> _players = new List<Player>();

        public void Work()
        {
            const string AddPlayerCommand = "1";
            const string BanPlayerCommand = "2";
            const string UnBanPlayerCommand = "3";
            const string RemovePlayerCommand = "4";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("База данных");
                Console.WriteLine();
                ShowPlayersInfo();
                Console.WriteLine();
                Console.WriteLine("Варианты команд:");
                Console.WriteLine($"Добавить игрока: {AddPlayerCommand}");
                Console.WriteLine($"Забанить игрока: {BanPlayerCommand}");
                Console.WriteLine($"Разбанить игрока: {UnBanPlayerCommand}");
                Console.WriteLine($"Удалить игрока: {RemovePlayerCommand}");
                string userInput = GetUserMessage("Введите команду:");

                switch (userInput)
                {
                    case AddPlayerCommand:
                        AddPlayer();
                        break;

                    case BanPlayerCommand:
                        BanPlayer();
                        break;

                    case UnBanPlayerCommand:
                        UnBanPlayer();
                        break;

                    case RemovePlayerCommand:
                        RemovePlayer();
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void AddPlayer()
        {
            string userName = GetUserMessage("Введите ник игрока:");

            if (IsContainsName(userName))
            {
                Console.WriteLine("Игрок с таким ником уже есть!");
            }
            else
            {
                string userId = GetUserMessage("Введите номер игрока:");

                if (int.TryParse(userId, out int resultId))
                {
                    bool isGetId;
                    GetIndexWithId(resultId, out isGetId);

                    if (isGetId)
                    {
                        Console.WriteLine("Игрок с таким номером уже есть!");
                    }
                    else
                    {
                        _players.Add(new Player(userName, resultId));
                    }
                }
                else
                {
                    Console.WriteLine("Введите целое число!");
                }
            }
        }

        private void UnBanPlayer()
        {
            int playerIndex;

            if (TryGetPlayerIndex(out playerIndex))
            {
                _players[playerIndex].UnBan();
            }
            else
            {
                Console.WriteLine("Не получилось разбанить игрока.");
            }
        }

        private void BanPlayer()
        {
            int playerIndex;
            
            if (TryGetPlayerIndex(out playerIndex))
            {
                _players[playerIndex].Ban();
            }
            else
            {
                Console.WriteLine("Не получилось забанить игрока.");
            }
        }
        private void RemovePlayer()
        {
            int playerIndex;

            if (TryGetPlayerIndex(out playerIndex))
            {
                _players.RemoveAt(playerIndex);
                Console.WriteLine("Игрок удален.");
            }
            else
            {
                Console.WriteLine("Не удалось удалить игрока.");
            }
        }

        private bool TryGetPlayerIndex(out int resultIndex)
        {
            string userInput = GetUserMessage("Введите номер игрока:");

            int resultId;
            bool isGetId;

            if (int.TryParse(userInput, out resultId))
            {
                resultIndex = GetIndexWithId(resultId, out isGetId);

                if (isGetId)
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Введите целое число!");
            }

            resultIndex = 0;
            return false;
        }

        private int GetIndexWithId(int userId, out bool isGetId)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == userId)
                {
                    isGetId = true;
                    return i;
                }
            }

            isGetId = false;
            return 0;
        }

        private bool IsContainsName(string userName)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (userName == _players[i].Name)
                    return true;
            }

            return false;
        }

        private void ShowPlayersInfo()
        {
            for (int i = 0; i < _players.Count; i++)
            {
                _players[i].ShowInfo();
            }
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }

    class Player
    {
        public string Name { get; private set; }
        public int Id { get; private set; }
        private bool _isBanned;

        public Player(string name, int id)
        {
            Id = id;
            Name = name;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Игрок: {Name} номер: {Id} забанен ли: {GetWordWithBanValue()}");
        }

        public void Ban()
        {
            if (_isBanned)
                Console.WriteLine("Игрок уже забанен!");
            else
                _isBanned = true;
        }

        public void UnBan()
        {
            if (_isBanned)
                _isBanned = false;
            else
                Console.WriteLine("Игрок не забанен!");
        }

        private string GetWordWithBanValue()
        {
            return (_isBanned) ? "Да" : "Нет";
        }
    }
}
