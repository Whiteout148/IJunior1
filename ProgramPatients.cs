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

            battleField.Fight();
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

        public BattleField()
        {
            _firstPlatoon.AddFighter(new Fighter("Х1", 100, 20, 30));
            _firstPlatoon.AddFighter(new Sniper(2, "Х2", 90, 30, 60));
            _firstPlatoon.AddFighter(new GrenadeLauncher(4, "Х3", 110, 20, 30));
            _firstPlatoon.AddFighter(new MachineGunner(3, "Х4", 100, 20, 50));
            _firstPlatoon.AddFighter(new GrenadeLauncher(5, "Х5", 100, 30, 30));

            _secondPlatoon.AddFighter(new Fighter("Д1", 100, 20, 40));
            _secondPlatoon.AddFighter(new Sniper(3, "Д2", 100, 20, 50));
            _secondPlatoon.AddFighter(new GrenadeLauncher(2, "Д3", 120, 20, 50));
            _secondPlatoon.AddFighter(new MachineGunner(3, "Д4", 110, 30, 50));
            _secondPlatoon.AddFighter(new Sniper(4, "Д5", 110, 40, 60));
        }

        public void Fight()
        {
            _firstPlatoon.ShowFighters();
            Console.WriteLine();
            _secondPlatoon.ShowFighters();
            Console.WriteLine("Нажмите любую клавишу чтобы начать бой.");
            Console.ReadKey();

            _firstPlatoon.AddFightersToDamage();
            _secondPlatoon.AddFightersToDamage();

            int secondWithMs = 1000;

            while (_firstPlatoon.GetFightersHealth() > 0 && _secondPlatoon.GetFightersHealth() > 0)
            {
                _firstPlatoon.Attack(_secondPlatoon.FightersToDamage);
                _secondPlatoon.Attack(_firstPlatoon.FightersToDamage);
            }

            ShowResults();
        }

        private void ShowResults()
        {
            if (_firstPlatoon.GetFightersHealth() > 0 && _secondPlatoon.GetFightersHealth() <= 0)
            {
                Console.WriteLine("Первый взвод победил.");
            }
            else if (_secondPlatoon.GetFightersHealth() > 0 && _firstPlatoon.GetFightersHealth() <= 0)
            {
                Console.WriteLine("Второй взвод победил.");
            }
            else
            {
                Console.WriteLine("Ничья");
            }
        }
    }

    class Platoon
    {
        private List<Fighter> _fighters = new List<Fighter>();
        public List<IDamageable> FightersToDamage = new List<IDamageable>();

        public void Attack(List<IDamageable> targets)
        {
            int halfSecondMs = 500;

            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].Attack(targets);
                Thread.Sleep(halfSecondMs);
            }
        }

        public void ShowFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                _fighters[i].ShowInfo();
                Console.WriteLine();
            }
        }

        public void AddFightersToDamage()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                FightersToDamage.Add(_fighters[i].GetClone());
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

    class Fighter : IDamageable
    {
        protected int Damage;
        protected int Armor;

        public Fighter(string name, int health, int damage, int armor)
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

        public virtual void TakeDamage(int damage)
        {
            int resultDamage = damage * (100 / (100 + Armor));

            Health -= resultDamage;
        }

        public virtual void Attack(List<IDamageable> targets)
        {
            Console.WriteLine($"Боец {Name} атакует.");
            ShowType();

            for (int i = 0; i < targets.Count; i++)
            {
                targets[i].TakeDamage(Damage);
            }
        }

        public virtual void ShowType()
        {
            Console.WriteLine("Обычный");
        }

        public virtual Fighter GetClone()
        {
            return new Fighter(Name, Health, Damage, Armor);
        }
    }

    class Sniper : Fighter
    {
        private int _damageFactor;

        public Sniper(int damageCounter, string name, int health, int damage, int armor) : base(name, health, damage, armor)
        {
            _damageFactor = damageCounter;

            damage *= _damageFactor;
        }

        public override void ShowType()
        {
            Console.WriteLine($"Снайпер, множитель урона: {_damageFactor}");
        }

        public override Fighter GetClone()
        {
            return new Sniper(_damageFactor, Name, Health, Damage, Armor);
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

        public override Fighter GetClone()
        {
            return new GrenadeLauncher(_targets, Name, Health, Damage, Armor);
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

        public override Fighter GetClone()
        {
            return new MachineGunner(_targets, Name, Health, Damage, Armor);
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