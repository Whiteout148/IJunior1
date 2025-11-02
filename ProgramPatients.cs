using System;
using System.Collections.Generic;
using System.Threading;

namespace XDproject
{
    class Program
    {
        static void Main()
        {
            BattleField battleField = new BattleField();
        }
    }

    interface IDamageable
    {
        void TakeDamage(int damage);
    }

    class BattleField
    {
        private Platoon _firstPlatoon = new Platoon();
        private Platoon _secondPlatoon = new Platoon();

        private void AddFightersToPlatoon()
        {
            _firstPlatoon.AddFighter(new Fighter("Х1", 100, 20, 30));
            _firstPlatoon.AddFighter(new Sniper(2 ,"Х2", 90, 30, 60));
            _firstPlatoon.AddFighter(new GrenadeLauncher(4 ,"Х3", 110, 20, 30));
            _firstPlatoon.AddFighter(new MachineGunner(3, "Х4", 100, 20, 50));
            _firstPlatoon.AddFighter(new GrenadeLauncher(5, "Х5", 100, 30, 30));

            _secondPlatoon.AddFighter(new Fighter("Д1", 100, 20, 40));
            _secondPlatoon.AddFighter(new Sniper(3 ,"Д2", 100, 20, 50));
            _secondPlatoon.AddFighter(new GrenadeLauncher(2,"Д3", 120, 20, 50));
            _secondPlatoon.AddFighter(new MachineGunner(3,"Д4", 110, 30, 50));
            _secondPlatoon.AddFighter(new Sniper(4,"Д5", 110, 40, 60));
        }
    }

    class Platoon
    {
        private List<Fighter> _fighters = new List<Fighter>();
        public int FighterCount { get { return _fighters.Count; } }

        public void ShowFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].ShowInfo();
                Console.WriteLine();
            }
        }

        public void AddFighter(Fighter fighter)
        {
            _fighters.Add(fighter);
        }

        public int GetFightersHealth()
        {
            int resultHealth = 0;

            for (int i = 0; i < _fighters.Count; i++)
            {
                resultHealth += _fighters[i].Health;
            }

            return resultHealth;
        }
    }

    class Fighter 
    {
        protected int Damage;
        protected int Armor;

        public Fighter(string name ,int health, int damage, int armor)
        {
            Name = name;
            Health = health;
            Damage = damage;
            Armor = armor;
        }

        public string Name { get; protected set; }
        public int Health { get; protected set; }

        public void ShowInfo()
        {
            Console.WriteLine($"Имя {Name} Здоровье {Health} Урон {Damage} Броня {Armor}");
            Console.WriteLine("Тип:");
            ShowType();
        }

        public virtual void ShowType()
        {
            Console.WriteLine("Обычный");
        }
    }

    class Sniper : Fighter
    {
        private int _damageCounter;

        public Sniper(int damageCounter, string name, int health, int damage, int armor) : base(name ,health, damage, armor)
        {
            _damageCounter = damageCounter;

            damage *= _damageCounter;
        }

        public override void ShowType()
        {
            Console.WriteLine($"Снайпер, множитель урона: {_damageCounter}");
        }
    }

    class GrenadeLauncher : Fighter
    {
        private int _targets;

        public GrenadeLauncher(int targets, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _targets = targets;
        }

        public override void ShowType()
        {
            Console.WriteLine("Гранатометчик");
            Console.WriteLine($"Количество противников на атаку: {_targets}");
        }
    }

    class MachineGunner : Fighter
    {
        private int _targets;

        public MachineGunner(int targets, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _targets = targets;
        }

        public override void ShowType()
        {
            Console.WriteLine("Пулеметчик");
            Console.WriteLine($"Количество противников на атаку: {_targets}");
        }
    }

    static class UserUtils
    {
        private static Random s_random = new Random();
        public static int MaxPercent { get; private set; } = 100;

        public static int GetRandomNumber(int min, int max)
        {
            return s_random.Next(min, max + 1);
        }
    }
}