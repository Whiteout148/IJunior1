using System;
using System.Collections.Generic;

namespace WhilesPractice1
{
    internal class WhileRepeats
    {
        static void Main()
        {
            Table playTable = new Table();

            playTable.Work();
        }
    }

    class Table
    {
        private Deck _deck = new Deck();
        private Player _player = new Player();

        public void Work()
        {
            const string GetCardsCommand = "1";
            const string ExitCommand = "2";

            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Колода карт:");
                _deck.ShowCards();
                Console.WriteLine();
                Console.WriteLine("Карты игрока:");
                _player.ShowCards();
                Console.WriteLine($"Команда для получение карт: {GetCardsCommand}");
                Console.WriteLine($"Команда для выхода: {ExitCommand}");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case GetCardsCommand:
                        PassCards();
                        break;

                    case ExitCommand:
                        isWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        public void PassCards()
        {
            Console.WriteLine("Введите количество карт: ");
            string userInput = Console.ReadLine();

            if (int.TryParse(userInput, out int result))
            {
                if (result > _deck.GetCardsLenght() - 1 || result < 0)
                {
                    Console.WriteLine("Число за больше чем карт в колоде или меньше нуля!");
                }
                else
                {
                    List<Card> newCards = new List<Card>();
                    newCards = _deck.GetCards(result);
                    _player.AddCards(newCards);
                }
            }
            else
            {
                Console.WriteLine("Введите целое число!");
            }
        }
    }

    class Player
    {
        private List<Card> _cards = new List<Card>();

        public void AddCards(List<Card> gettedCards)
        {
            _cards.AddRange(gettedCards);
        }

        public void ShowCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].ShowInfo();
            }

            Console.WriteLine();
        }
    }

    class Deck
    {
        private List<Card> _cards = new List<Card>();

        public Deck()
        {
            int minCardDenomination = 6;
            int maxCardDenomination = 14;

            AddCardRange(minCardDenomination, maxCardDenomination, '♠');
            AddCardRange(minCardDenomination, maxCardDenomination, '♥');
            AddCardRange(minCardDenomination, maxCardDenomination, '♦');
            AddCardRange(minCardDenomination, maxCardDenomination, '♣');
        }

        public int GetCardsLenght()
        {
            return _cards.Count + 1;
        }

        public void ShowCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                int suitQuantity = 9;

                if (i % suitQuantity == 0)
                {
                    Console.WriteLine();
                }

                _cards[i].ShowInfo();
            }

            Console.WriteLine();
        }

        public List<Card> GetCards(int cardQuantity)
        {
            List<Card> cardsToGet = new List<Card>();

            for (int i = 0; i < cardQuantity; i++)
            {
                cardsToGet.Add(_cards[0]);
                _cards.RemoveAt(0);
            }

            return cardsToGet;
        }

        private void AddCardRange(int minDenomination, int maxDenomination, char suit)
        {
            for (int i = minDenomination; i < maxDenomination + 1; i++)
            {
                _cards.Add(new Card(suit, i));
            }
        }
    }

    class Card
    {
        private char _suit;
        private int _denomination;

        public Card(char suit, int denomination)
        {
            _suit = suit;
            _denomination = denomination;
        }

        public void ShowInfo()
        {
            Console.Write($" {_suit}{_denomination}");
        }
    }
}
