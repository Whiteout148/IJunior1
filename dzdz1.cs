using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace dzdzdz
{
    internal class dzdz1
    {
        static void Main(string[] args)
        {
            Player soldier = new Player("Василий", 30, 100, 150);

            soldier.ShowInfo();
        }
    }

    class Player
    {
        private string _name;
        private int _age;
        private int _level;
        private int _power;

        public Player(string name, int age, int level, int power)
        {
            _name = name;
            _age = age;
            _level = level;
            _power = power;
        }

        public void ShowInfo()
        {
            Console.WriteLine("Игрок: " + _name);
            Console.WriteLine($"Возраст: {_age} Уровень: {_level} Сила: {_power}");
        }
    }
}
