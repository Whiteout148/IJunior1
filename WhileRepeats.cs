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
            Table playTable = new Table();

            playTable.Work();
        }
    }

    class Table
    {
        private Deck _deck = new Deck(8);
        private Player _player = new Player();

        public void Work()
        {
            bool isWork = true;

            while (isWork)
            {
                Console.WriteLine("Карты в колоде:");
                _deck.ShowCards();
                Console.WriteLine();
                Console.WriteLine("Карты у игрока: ");
                _player.ShowCards();
                Console.WriteLine();
                Console.WriteLine("Введите количество карт которые хотите передать игроку:");
                PassCards();

                Console.ReadKey();
                Console.Clear();
            }
        }

        private void PassCards()
        {
            int cardQuantity;

            if (TryToGetUserQuantity(out cardQuantity))
            {
                _deck.RemoveCards(cardQuantity);
                _player.GetCards(cardQuantity);
                Console.WriteLine("Игрок получил карты.");
            }
        }

        private bool TryToGetUserQuantity(out int resultQuantity)
        {
            string userInput = GetUserMessage("Введите число карт:");

            if (int.TryParse(userInput, out resultQuantity))
            {   
                if (resultQuantity <= 0 || resultQuantity > _deck.Cards.Count)
                {
                    Console.WriteLine("Число выходит за границы количества карт!");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                Console.WriteLine("Введите целое число!");
            }

            return false;
        }

        private string GetUserMessage(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }

    class Player
    {
        private List<card> _cards = new List<card>();

        public void ShowCards()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                _cards[i].ShowSymbol();
            }
        }

        public void GetCards(int cardQuantity)
        {
            for (int i = 0; i < cardQuantity; i++)
            {
                _cards.Add(new card());
            }
        }
    }

    class Deck
    {
        private int _deckLength;

        public Deck(int deckLenght)
        {
            _deckLength = deckLenght;
            AddCards();
        }

        public List<card> Cards { get; private set; } = new List<card>();

        public void AddCards()
        {
            for (int i = 0; i < _deckLength; i++)
            {
                Cards.Add(new card());
            }
        }

        public void ShowCards()
        {
            for (int i = 0; i < Cards.Count; i++)
            {
                Cards[i].ShowSymbol();
            }
        }

        public void RemoveCards(int cardQuantity)
        {
            for (int i = 0; i < cardQuantity; i++)
            {
                Cards.RemoveAt(0);
            }
        }
    }

    class card
    {
        public char Symbol { get; private set; } = '♠';

        public void ShowSymbol()
        {
            Console.Write(Symbol + " ");
        }
    }
}
